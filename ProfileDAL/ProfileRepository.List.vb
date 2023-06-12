Imports System.Data.Objects
Imports System.IO
Imports System.Reflection
Imports System.Web
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Ionic.Zip

Partial Class ProfileRepository

#Region "salary group"

    ''' <summary>
    ''' Lay data vao combo cho bang luong
    ''' </summary>
    ''' <param name="dateValue">Ma bang luong</param>
    ''' <param name="isBlank">0: Khong lay dong empty; 1: Co lay dong empty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryRankCombo(ByVal SalaryLevel As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_RANK",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP = SalaryLevel,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryLevelCombo(ByVal SalaryGroup As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_LEVEL",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP = SalaryGroup,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryLevelComboNotByGroup(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_LEVEL",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP = 0,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "contract procedure"
    Public Function GetContractProcedure(ByVal _filter As ContractProcedureDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractProcedureDTO)

        Try
            Dim query = From p In Context.HU_CONTRACT_PROCEDURE
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From h1 In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.HD_1).DefaultIfEmpty
                        From h2 In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.HD_2).DefaultIfEmpty
                        From h3 In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.HD_3).DefaultIfEmpty
                        From h4 In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.HD_4).DefaultIfEmpty
                        From h5 In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.HD_5).DefaultIfEmpty
                        From h6 In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.HD_6).DefaultIfEmpty
                        Select New ContractProcedureDTO With {
                                   .ID = p.ID,
                                   .ORG_NAME = o.NAME_VN,
                                   .HD_1 = p.HD_1,
                                   .HD_1_NAME = h1.NAME,
                                   .HD_2 = p.HD_2,
                                   .HD_2_NAME = h2.NAME,
                                   .HD_3 = p.HD_3,
                                   .HD_3_NAME = h3.NAME,
                                   .HD_4 = p.HD_4,
                                   .HD_4_NAME = h4.NAME,
                                   .HD_5 = p.HD_5,
                                   .HD_5_NAME = h5.NAME,
                                   .HD_6 = p.HD_6,
                                   .HD_6_NAME = h6.NAME,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.HD_1_NAME) Then
                lst = lst.Where(Function(p) p.HD_1_NAME.ToUpper.Contains(_filter.HD_1_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.HD_2_NAME) Then
                lst = lst.Where(Function(p) p.HD_2_NAME.ToUpper.Contains(_filter.HD_2_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.HD_3_NAME) Then
                lst = lst.Where(Function(p) p.HD_3_NAME.ToUpper.Contains(_filter.HD_3_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.HD_4_NAME) Then
                lst = lst.Where(Function(p) p.HD_4_NAME.ToUpper.Contains(_filter.HD_4_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.HD_5_NAME) Then
                lst = lst.Where(Function(p) p.HD_5_NAME.ToUpper.Contains(_filter.HD_5_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.HD_6_NAME) Then
                lst = lst.Where(Function(p) p.HD_6_NAME.ToUpper.Contains(_filter.HD_6_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertContractProcedure(ByVal objContractProcedure As ContractProcedureDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractProcedureData As New HU_CONTRACT_PROCEDURE
        Dim iCount As Integer = 0
        Try
            If objContractProcedure.ID = Nothing Then
                objContractProcedureData.ID = Utilities.GetNextSequence(Context, Context.HU_CONTRACT_PROCEDURE.EntitySet.Name)
                objContractProcedureData.ACTFLG = "A"
                objContractProcedureData.HD_1 = objContractProcedure.HD_1
                objContractProcedureData.HD_2 = objContractProcedure.HD_2
                objContractProcedureData.HD_3 = objContractProcedure.HD_3
                objContractProcedureData.HD_4 = objContractProcedure.HD_4
                objContractProcedureData.HD_5 = objContractProcedure.HD_5
                objContractProcedureData.HD_6 = objContractProcedure.HD_6
                objContractProcedureData.EFFECT_DATE = objContractProcedure.EFFECT_DATE
                objContractProcedureData.NOTE = objContractProcedure.NOTE
                objContractProcedureData.ORG_ID = objContractProcedure.ORG_ID

                Context.HU_CONTRACT_PROCEDURE.AddObject(objContractProcedureData)

                Context.SaveChanges(log)
                gID = objContractProcedureData.ID
            Else
                objContractProcedureData = (From p In Context.HU_CONTRACT_PROCEDURE Where p.ID = objContractProcedure.ID).FirstOrDefault
                objContractProcedureData.ID = objContractProcedure.ID
                objContractProcedureData.ACTFLG = "A"
                objContractProcedureData.HD_1 = objContractProcedure.HD_1
                objContractProcedureData.HD_2 = objContractProcedure.HD_2
                objContractProcedureData.HD_3 = objContractProcedure.HD_3
                objContractProcedureData.HD_4 = objContractProcedure.HD_4
                objContractProcedureData.HD_5 = objContractProcedure.HD_5
                objContractProcedureData.HD_6 = objContractProcedure.HD_6
                objContractProcedureData.EFFECT_DATE = objContractProcedure.EFFECT_DATE
                objContractProcedureData.NOTE = objContractProcedure.NOTE
                If objContractProcedure.ORG_ID <> 0 Then
                    objContractProcedureData.ORG_ID = objContractProcedure.ORG_ID
                End If

                Context.SaveChanges(log)
                gID = objContractProcedureData.ID
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteContractProcedure(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_CONTRACT_PROCEDURE)
        Try

            lstTitleData = (From p In Context.HU_CONTRACT_PROCEDURE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_CONTRACT_PROCEDURE.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function Delete_List_Contract(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_CONTRACT)
        Try

            lstTitleData = (From p In Context.HU_CONTRACT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_CONTRACT.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function checkContractProcedure(ByVal objContractProcedure As ContractProcedureDTO) As Decimal
        Dim re = 0
        If objContractProcedure.ID = Nothing Then
            re = (From p In Context.HU_CONTRACT_PROCEDURE.Where(Function(f) f.ORG_ID = objContractProcedure.ORG_ID _
                                                           And f.EFFECT_DATE = objContractProcedure.EFFECT_DATE
                                                           )
                  Select New ContractProcedureDTO With {.ID = p.ID}).ToList.Count
        Else
            re = (From p In Context.HU_CONTRACT_PROCEDURE.Where(Function(f) f.ID <> objContractProcedure.ID And f.ORG_ID = objContractProcedure.ORG_ID _
                                                           And f.EFFECT_DATE = objContractProcedure.EFFECT_DATE
                                                           )
                  Select New ContractProcedureDTO With {.ID = p.ID}).ToList.Count
        End If

        Return re
    End Function
#End Region


#Region "org_brand"
    Public Function GetOrgLevel(ByVal org_id As Decimal) As OrganizationDTO
        Try
            Dim org = (From p In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = org_id) Select New OrganizationDTO With {
                      .ID = p.ID,
                      .NAME_VN = p.NAME_VN,
                      .CODE = p.CODE,
                      .INFOR_5 = p.ORG_NAME5,
                      .INFOR_4 = p.ORG_NAME4,
                      .INFOR_3 = p.ORG_NAME3,
                      .INFOR_2 = p.ORG_NAME2
                      }).FirstOrDefault
            Return org
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOrgBrand(ByVal _filter As OrgBrandDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgBrandDTO)

        Try
            Dim query = From p In Context.HU_ORG_BRAND
                        From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From oo In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STORE_TYPE_ID).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MODELSHOP).DefaultIfEmpty
                        From ot3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGION_ID And f.TYPE_CODE = "REGION_CH").DefaultIfEmpty
                        Select New OrgBrandDTO With {
                                   .ID = p.ID,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_CODE = o2.CODE,
                                   .ORG_NAME_1 = o2.NAME_VN,
                                   .ORG_NAME_2 = oo.ORG_NAME2,
                                   .ORG_NAME_3 = oo.ORG_NAME3,
                                   .ORG_NAME_4 = oo.ORG_NAME4,
                                   .ORG_NAME_5 = oo.ORG_NAME5,
                                   .BRAND_ID = p.BRAND_ID,
                                   .BRAND_NAME = ot.NAME_VN,
                                   .STORE_TYPE_ID = p.STORE_TYPE_ID,
                                   .STORE_TYPE_NAME = ot1.NAME_VN,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .MODELSHOP_ID = p.MODELSHOP,
                                   .MODELSHOP_NAME = ot2.NAME_VN,
                                   .REGION_ID = p.REGION_ID,
                                   .REGION_NAME = ot3.NAME_VN}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.ORG_CODE) Then
                lst = lst.Where(Function(p) p.ORG_CODE.ToUpper.Contains(_filter.ORG_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME_1) Then
                lst = lst.Where(Function(p) p.ORG_NAME_1.ToUpper.Contains(_filter.ORG_NAME_1.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME_2) Then
                lst = lst.Where(Function(p) p.ORG_NAME_2.ToUpper.Contains(_filter.ORG_NAME_2.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME_3) Then
                lst = lst.Where(Function(p) p.ORG_NAME_3.ToUpper.Contains(_filter.ORG_NAME_3.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.BRAND_NAME) Then
                lst = lst.Where(Function(p) p.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.STORE_TYPE_NAME) Then
                lst = lst.Where(Function(p) p.STORE_TYPE_NAME.ToUpper.Contains(_filter.STORE_TYPE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.MODELSHOP_NAME) Then
                lst = lst.Where(Function(p) p.MODELSHOP_NAME.ToUpper.Contains(_filter.MODELSHOP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REGION_NAME) Then
                lst = lst.Where(Function(p) p.REGION_NAME.ToUpper.Contains(_filter.REGION_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteOrgBrand(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_ORG_BRAND)
        Try

            lstTitleData = (From p In Context.HU_ORG_BRAND Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_ORG_BRAND.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function checkOrgBrand(ByVal objOrgBrand As OrgBrandDTO) As Decimal
        Dim re = 0
        If objOrgBrand.ID = Nothing Then
            re = (From p In Context.HU_ORG_BRAND.Where(Function(f) f.ORG_ID = objOrgBrand.ORG_ID _
                                                           And f.EFFECT_DATE = objOrgBrand.EFFECT_DATE _
                                                           And f.BRAND_ID = objOrgBrand.BRAND_ID _
                                                           And f.STORE_TYPE_ID = objOrgBrand.STORE_TYPE_ID
                                                           )
                  Select New OrgBrandDTO With {.ID = p.ID}).ToList.Count
        Else
            re = (From p In Context.HU_ORG_BRAND.Where(Function(f) f.ID <> objOrgBrand.ID And f.ORG_ID = objOrgBrand.ORG_ID _
                                                           And f.EFFECT_DATE = objOrgBrand.EFFECT_DATE _
                                                           And f.BRAND_ID = objOrgBrand.BRAND_ID _
                                                           And f.STORE_TYPE_ID = objOrgBrand.STORE_TYPE_ID
                                                           )
                  Select New OrgBrandDTO With {.ID = p.ID}).ToList.Count
        End If

        Return re
    End Function
    Public Function InsertOrgBrand(ByVal objOrgBrand As OrgBrandDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrgBrandData As New HU_ORG_BRAND
        Dim iCount As Integer = 0
        Try
            If objOrgBrand.ID = Nothing Then
                objOrgBrandData.ID = Utilities.GetNextSequence(Context, Context.HU_ORG_BRAND.EntitySet.Name)
                objOrgBrandData.ACTFLG = "A"
                objOrgBrandData.BRAND_ID = objOrgBrand.BRAND_ID
                objOrgBrandData.STORE_TYPE_ID = objOrgBrand.STORE_TYPE_ID
                objOrgBrandData.MODELSHOP = objOrgBrand.MODELSHOP_ID
                objOrgBrandData.REGION_ID = objOrgBrand.REGION_ID
                objOrgBrandData.EFFECT_DATE = objOrgBrand.EFFECT_DATE
                objOrgBrandData.NOTE = objOrgBrand.NOTE
                objOrgBrandData.ORG_ID = objOrgBrand.ORG_ID

                Context.HU_ORG_BRAND.AddObject(objOrgBrandData)
                Context.SaveChanges(log)
                gID = objOrgBrandData.ID
            Else
                objOrgBrandData = (From p In Context.HU_ORG_BRAND Where p.ID = objOrgBrand.ID).FirstOrDefault
                objOrgBrandData.ID = objOrgBrand.ID
                objOrgBrandData.ACTFLG = "A"
                objOrgBrandData.BRAND_ID = objOrgBrand.BRAND_ID
                objOrgBrandData.STORE_TYPE_ID = objOrgBrand.STORE_TYPE_ID
                objOrgBrandData.MODELSHOP = objOrgBrand.MODELSHOP_ID
                objOrgBrandData.REGION_ID = objOrgBrand.REGION_ID
                objOrgBrandData.EFFECT_DATE = objOrgBrand.EFFECT_DATE
                objOrgBrandData.NOTE = objOrgBrand.NOTE
                If objOrgBrand.ORG_ID <> 0 Then
                    objOrgBrandData.ORG_ID = objOrgBrand.ORG_ID
                End If

                Context.SaveChanges(log)
                gID = objOrgBrandData.ID
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "org_pause"
    Public Function GetOrgLevel_Pause(ByVal org_id As Decimal) As OrganizationDTO
        Try
            Dim org = (From p In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = org_id) Select New OrganizationDTO With {
                      .ID = p.ID,
                      .NAME_VN = p.NAME_VN,
                      .CODE = p.CODE
                      }).FirstOrDefault
            Return org
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOrgPause(ByVal _filter As ORG_PAUSEDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ORG_PAUSEDTO)

        Try
            Dim query = From p In Context.HU_ORG_PAUSE
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Select New ORG_PAUSEDTO With {
                                        .ID = p.ID,
                                        .ORG_ID = p.ORG_ID,
                                        .ORG_CODE = o.CODE,
                                        .ORG_NAME = o.NAME_VN,
                                        .DAY_NUM = p.DAY_NUM,
                                        .EFFECT_FROM = p.EFFECT_FROM,
                                        .EFFECT_TO = p.EFFECT_TO,
                                        .IS_OT = p.IS_OT,
                                        .IS_PAUSE = p.IS_PAUSE,
                                        .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.ORG_CODE) Then
                lst = lst.Where(Function(p) p.ORG_CODE.ToUpper.Contains(_filter.ORG_CODE.ToUpper))
            End If
            If _filter.EFFECT_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_FROM = _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_TO IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_TO = _filter.EFFECT_TO)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteOrgPause(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_ORG_PAUSE)
        Try

            lstTitleData = (From p In Context.HU_ORG_PAUSE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_ORG_PAUSE.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function checkOrgPause(ByVal objOrg As ORG_PAUSEDTO) As Decimal
        Dim re = 0

        re = (From p In Context.HU_ORG_PAUSE.Where(Function(f) f.ID <> objOrg.ID And f.ORG_ID = objOrg.ORG_ID _
                                                           And f.EFFECT_FROM = objOrg.EFFECT_FROM)
              Select New ORG_PAUSEDTO With {.ID = p.ID}).ToList.Count

        Return re
    End Function
    Public Function InsertOrgPause(ByVal objOrgPause As ORG_PAUSEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrgPauseData As New HU_ORG_PAUSE
        Dim iCount As Integer = 0
        Try
            If objOrgPause.ID = 0 Then
                objOrgPauseData.ID = Utilities.GetNextSequence(Context, Context.HU_ORG_PAUSE.EntitySet.Name)
                objOrgPauseData.ORG_ID = objOrgPause.ORG_ID
                objOrgPauseData.EFFECT_FROM = objOrgPause.EFFECT_FROM
                objOrgPauseData.EFFECT_TO = objOrgPause.EFFECT_TO
                objOrgPauseData.DAY_NUM = objOrgPause.DAY_NUM
                objOrgPauseData.IS_PAUSE = objOrgPause.IS_PAUSE
                objOrgPauseData.IS_OT = objOrgPause.IS_OT
                Context.HU_ORG_PAUSE.AddObject(objOrgPauseData)
                Context.SaveChanges(log)
                gID = objOrgPauseData.ID
            Else
                objOrgPauseData = (From p In Context.HU_ORG_PAUSE Where p.ID = objOrgPause.ID).FirstOrDefault
                objOrgPauseData.ORG_ID = objOrgPause.ORG_ID
                objOrgPauseData.EFFECT_FROM = objOrgPause.EFFECT_FROM
                objOrgPauseData.EFFECT_TO = objOrgPause.EFFECT_TO
                objOrgPauseData.DAY_NUM = objOrgPause.DAY_NUM
                objOrgPauseData.IS_PAUSE = objOrgPause.IS_PAUSE
                objOrgPauseData.IS_OT = objOrgPause.IS_OT

                Context.SaveChanges(log)
                gID = objOrgPauseData.ID
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Title"
    Public Function CheckPermissionByPosition(Optional ByVal lstID As List(Of Decimal) = Nothing,
                                              Optional ByVal Org_ID As Decimal = 0) As List(Of Decimal)
        '// Check quyen du lieu
        'Dim cls As New DataAccess.QueryData
        'Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_POSITION_RIGHT",
        '                                 New With {.P_EMPLOYEE_ID = Log.EMPLOYEE_ID,
        '                                           .P_CUR = cls.OUT_CURSOR})
        'Dim lst As List(Of Decimal?) = (From p In dtData.ToList(Of PositionRightDTO)()
        '                                Where (p.ORG_ID = Org_ID Or Org_ID = 0)
        '                                Select p.ID).ToList
        'Dim objChk As List(Of Decimal)
        'If (lstID IsNot Nothing) Then
        '    objChk = (From p In Context.HU_TITLE
        '              Where (lstID.Contains(p.ID) And lst.Contains(p.ID))
        '              Select p.ID).ToList()
        'Else
        '    objChk = (From p In Context.HU_TITLE
        '              Where (lst.Contains(p.ID))
        '              Select p.ID).ToList()
        'End If
        'Return objChk
    End Function
    Public Function SwapMasterInterim(ByVal _Id As Decimal, ByVal log As UserLog, ByVal type As String,
                                    Optional ByVal OrgIDDefault As Decimal = 1,
                                    Optional ByVal IsDissolveDefault As Decimal = 0) As Boolean
        Dim objTitleData As New HU_TITLE
        Try

            If (type = "APP") Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                     New With {.P_USERNAME = log.Username.ToUpper(),
                                               .P_ORGID = OrgIDDefault,
                                               .P_ISDISSOLVE = IsDissolveDefault})
                End Using
                '// Check quyen du lieu
                Dim objChk As List(Of Decimal) = (From p In Context.HU_TITLE
                                                  From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                                                  Where p.ID = _Id
                                                  Select p.ID).ToList()
                If (objChk.Count < 1) Then
                    Return False
                End If
            Else
                '// Check quyen du lieu
                Dim lst As List(Of Decimal) = New List(Of Decimal)
                lst.Add(_Id)
                Dim objChk As List(Of Decimal) = CheckPermissionByPosition(lst)
                If (objChk.Count < 1) Then
                    Return False
                End If
            End If


            Dim master As Decimal
            Dim interim As Decimal
            Dim dateNow = Date.Now
            objTitleData = (From p In Context.HU_TITLE Where p.ID = _Id).FirstOrDefault
            If objTitleData.INTERIM IsNot Nothing Then
                interim = objTitleData.INTERIM
            End If
            If objTitleData.MASTER IsNot Nothing Then
                master = objTitleData.MASTER
                objTitleData.INTERIM = objTitleData.MASTER
                objTitleData.CONCURRENT = 0
            Else
                objTitleData.INTERIM = Nothing
            End If
            'Từ Interim -> Master, check Interim đã từng kiêm nhiệm còn hiệu lực tính đến hiện tại
            'And p.STATUS = 447 _
            Dim concurent = (From p In Context.HU_TITLE_CONCURRENT
                             Where p.EMPLOYEE_ID = interim _
                            And p.TITLE_ID = _Id _
                             And p.EFFECT_DATE.HasValue And p.EFFECT_DATE <= dateNow _
                             And ((p.EXPIRE_DATE.HasValue And p.EXPIRE_DATE >= dateNow) Or (p.EXPIRE_DATE Is Nothing))
                             Order By p.EFFECT_DATE Descending).FirstOrDefault
            If concurent IsNot Nothing Then
                objTitleData.CONCURRENT = -1
            End If
            If interim <> 0 Then
                objTitleData.MASTER = interim
            Else
                objTitleData.MASTER = Nothing
            End If

            Context.SaveChanges(log)
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_OMS_BUSINESS.JOB_AUTO_UPDATE_TITLE_TEMP")
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "SwapMasterInterim")
            Throw ex
        End Try

    End Function
    Public Function GetTitle(ByVal _filter As TitleDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        Try
            Dim query = From p In Context.HU_TITLE
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From bld In Context.HU_TITLE_BLD.Where(Function(f) f.ID = p.TITLE_BLD_ID).DefaultIfEmpty
                        From tbl In Context.HU_TITLE_TBL.Where(Function(f) f.ID = p.TITLE_TBL_ID).DefaultIfEmpty
                        From orgLv In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From orgType In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CSM_1).DefaultIfEmpty
                        From groupEmp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_EMPLOYEE_ID).DefaultIfEmpty
                        Select New TitleDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .NAME_SUM = p.NAME_SUM,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                   .TITLE_GROUP_NAME = group.NAME_VN,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_ID_NAME = orgLv.NAME_VN,
                                   .ORG_TYPE = p.CSM_1,
                                   .ORG_TYPE_NAME = orgType.NAME_VN}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            If _filter.TN_COEF IsNot Nothing Then
                lst = lst.Where(Function(p) p.TN_COEF = _filter.TN_COEF)
            End If
            If _filter.CV_COEF IsNot Nothing Then
                lst = lst.Where(Function(p) p.CV_COEF = _filter.CV_COEF)
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_BLD_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_BLD_NAME.ToUpper.Contains(_filter.TITLE_BLD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_TBL_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_TBL_NAME.ToUpper.Contains(_filter.TITLE_TBL_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetTitleID(ByVal ID As Decimal) As TitleDTO

        Try
            Dim query = (From p In Context.HU_TITLE
                         From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                         From work In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.WORK_INVOLVE_ID).DefaultIfEmpty
                         From title In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                         Where p.ID = ID
                         Select New TitleDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .NAME_SUM = p.NAME_SUM,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                    .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                    .TITLE_GROUP_NAME = group.NAME_VN,
                                    .WORK_INVOLVE_NAME = work.NAME_VN,
                                    .LEVEL_TITLE_NAME = title.NAME_VN,
                                    .DRIVE_INFOR = p.DRIVE_INFOR,
                                    .WORK_INVOLVE_ID = p.WORK_INVOLVE_ID,
                                    .OVT = p.OVT,
                                    .LIST_PC_ID = p.LIST_PC_ID,
                                    .OVT_CHECK = If(p.OVT = "-1", True, False),
                                    .UPLOAD_FILE = p.UPLOAD_FILE,
                                    .LEVEL_ID = p.LEVEL_ID}).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertTitle(ByVal objTitle As TitleDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_TITLE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_SUM = objTitle.NAME_SUM
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            'objTitleData.HURTFUL = objTitle.HURTFUL
            'objTitleData.SPEC_HURFUL = objTitle.SPEC_HURFUL
            objTitleData.OVT = objTitle.OVT
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.CSM_1 = objTitle.ORG_TYPE
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.WORK_INVOLVE_ID = objTitle.WORK_INVOLVE_ID
            objTitleData.DRIVE_INFOR = objTitle.DRIVE_INFOR
            objTitleData.LIST_PC_ID = objTitle.LIST_PC_ID
            objTitleData.GROUP_EMPLOYEE_ID = objTitle.GROUP_EMPLOYEE_ID
            objTitleData.TITLE_BLD_ID = objTitle.TITLE_BLD_ID
            objTitleData.TITLE_TBL_ID = objTitle.TITLE_TBL_ID
            objTitleData.TN_COEF = objTitle.TN_COEF
            objTitleData.CV_COEF = objTitle.CV_COEF
            objTitleData.IS_PERCENT = objTitle.IS_PERCENT
            objTitleData.IS_CBCD = objTitle.IS_CBCD

            Context.HU_TITLE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateTitle(ByVal _validate As TitleDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_TITLE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyTitle(ByVal objTitle As TitleDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_TITLE
        Try
            objTitleData = (From p In Context.HU_TITLE Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_SUM = objTitle.NAME_SUM
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID

            objTitleData.OVT = objTitle.OVT
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.CSM_1 = objTitle.ORG_TYPE
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.WORK_INVOLVE_ID = objTitle.WORK_INVOLVE_ID
            objTitleData.DRIVE_INFOR = objTitle.DRIVE_INFOR
            objTitleData.LIST_PC_ID = objTitle.LIST_PC_ID
            objTitleData.GROUP_EMPLOYEE_ID = objTitle.GROUP_EMPLOYEE_ID
            objTitleData.TITLE_BLD_ID = objTitle.TITLE_BLD_ID
            objTitleData.TITLE_TBL_ID = objTitle.TITLE_TBL_ID
            objTitleData.TN_COEF = objTitle.TN_COEF
            objTitleData.CV_COEF = objTitle.CV_COEF
            objTitleData.IS_PERCENT = objTitle.IS_PERCENT
            objTitleData.IS_CBCD = objTitle.IS_CBCD

            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateManager(ByVal lstID As List(Of Decimal), ByVal objTitle As TitleDTO, ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE)
        Try
            lstTitleData = (From p In Context.HU_TITLE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                If objTitle.LM IsNot Nothing Then
                    lstTitleData(index).LM = objTitle.LM
                End If
                If objTitle.CSM IsNot Nothing Then
                    lstTitleData(index).CSM = objTitle.CSM
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ActiveTitle(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE)
        Dim lstOrgTitle As List(Of HU_ORG_TITLE)
        Try
            lstTitleData = (From p In Context.HU_TITLE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                lstTitleData(index).ACTFLG = sActive
                Dim id As Decimal = lstTitleData(index).ID
                lstOrgTitle = (From p In Context.HU_ORG_TITLE Where p.TITLE_ID = id And p.ACTFLG <> sActive).ToList
                For i = 0 To lstOrgTitle.Count - 1
                    lstOrgTitle(i).ACTFLG = sActive
                Next
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteTitle(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE)
        Try

            lstTitleData = (From p In Context.HU_TITLE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_TITLE.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Get title by ID
    ''' </summary>
    ''' <param name="sID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTitleByID(ByVal sID As Decimal) As List(Of TitleDTO)

        Try
            Dim query = (From p In Context.HU_TITLE
                         From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_EMPLOYEE_ID).DefaultIfEmpty
                         Where p.ID = sID
                         Select New TitleDTO With {.ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .GROUP_EMPLOYEE_NAME = ot.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try



    End Function
#End Region

#Region "Chuc vu"

    Public Function GetChucvu(ByVal _filter As ChucvuDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)

        Try
            Dim query = From p In Context.HU_CHUCVU
                        Select New ChucvuDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetChucvuID(ByVal ID As Decimal) As ChucvuDTO

        Try
            Dim query = (From p In Context.HU_CHUCVU
                         Where p.ID = ID
                         Select New ChucvuDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                }).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertChucvu(ByVal objTitle As ChucvuDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_CHUCVU
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_CHUCVU.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = "A"
            objTitleData.REMARK = objTitle.REMARK

            Context.HU_CHUCVU.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateChucvuCode(ByVal param As ChucvuDTO)
        Try
            If param.ID <> 0 Then
                Dim count = (From p In Context.HU_CHUCVU Where p.ID <> param.ID And p.CODE = param.CODE Select p).Count
                If count > 0 Then
                    Return False
                End If
            Else
                Dim count = (From p In Context.HU_CHUCVU Where p.CODE = param.CODE Select p).Count
                If count > 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyChucvu(ByVal objTitle As ChucvuDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_CHUCVU
        Try
            objTitleData = (From p In Context.HU_CHUCVU Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveChucvu(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_CHUCVU)
        Try
            lstTitleData = (From p In Context.HU_CHUCVU Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                lstTitleData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteChucvu(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_CHUCVU)
        Try

            lstTitleData = (From p In Context.HU_CHUCVU Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_CHUCVU.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetChucvuByID(ByVal sID As Decimal) As List(Of ChucvuDTO)

        Try
            Dim query = (From p In Context.HU_CHUCVU.ToList Where p.ID = sID
                         Select New ChucvuDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try



    End Function
#End Region

#Region "Chuc vu bld"

    Public Function GetChucvuBld(ByVal _filter As ChucvuDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)

        Try
            Dim query = From p In Context.HU_TITLE_BLD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        Select New ChucvuDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = e.EMPLOYEE_CODE + " - " + e.FULLNAME_VN,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetChucvuBldID(ByVal ID As Decimal) As ChucvuDTO

        Try
            Dim query = (From p In Context.HU_TITLE_BLD
                         Where p.ID = ID
                         Select New ChucvuDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                }).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertChucvuBld(ByVal objTitle As ChucvuDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_TITLE_BLD
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_CHUCVU.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = "A"
            objTitleData.REMARK = objTitle.REMARK

            Context.HU_TITLE_BLD.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateChucvuBldCode(ByVal param As ChucvuDTO)
        Try
            If param.ID <> 0 Then
                Dim count = (From p In Context.HU_TITLE_BLD Where p.ID <> param.ID And p.CODE = param.CODE Select p).Count
                If count > 0 Then
                    Return False
                End If
            Else
                Dim count = (From p In Context.HU_TITLE_BLD Where p.CODE = param.CODE Select p).Count
                If count > 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyChucvuBld(ByVal objTitle As ChucvuDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_TITLE_BLD
        Try
            objTitleData = (From p In Context.HU_TITLE_BLD Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveChucvuBld(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE_BLD)
        Try
            lstTitleData = (From p In Context.HU_TITLE_BLD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                lstTitleData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteChucvuBld(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE_BLD)
        Try

            lstTitleData = (From p In Context.HU_TITLE_BLD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_TITLE_BLD.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetChucvuBldByID(ByVal sID As Decimal) As List(Of ChucvuDTO)

        Try
            Dim query = (From p In Context.HU_TITLE_BLD.ToList Where p.ID = sID
                         Select New ChucvuDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try



    End Function

    Public Function GetTitleBLDsByOrg(ByVal org_id As Decimal, ByVal emp_id As Decimal?) As List(Of ChucvuDTO)

        Try
            Dim orgObj = (From p In Context.HU_ORGANIZATION Where p.ID = org_id).FirstOrDefault
            If orgObj Is Nothing Then
                Return New List(Of ChucvuDTO)
            Else
                Dim query = From p In Context.HU_TITLE_BLD
                            Where (p.EMPLOYEE_ID Is Nothing Or (emp_id IsNot Nothing AndAlso p.EMPLOYEE_ID = emp_id)) AndAlso p.REMARK.ToUpper.Equals(orgObj.SHORT_NAME.ToUpper)
                            Select New ChucvuDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN}

                Return query.ToList
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try



    End Function

#End Region

#Region "Chuc vu tbl"

    Public Function GetChucvuTbl(ByVal _filter As ChucvuDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)

        Try
            Dim query = From p In Context.HU_TITLE_TBL
                        Select New ChucvuDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetChucvuTblID(ByVal ID As Decimal) As ChucvuDTO

        Try
            Dim query = (From p In Context.HU_TITLE_TBL
                         Where p.ID = ID
                         Select New ChucvuDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                }).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertChucvuTbl(ByVal objTitle As ChucvuDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_TITLE_TBL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_CHUCVU.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = "A"
            objTitleData.REMARK = objTitle.REMARK

            Context.HU_TITLE_TBL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateChucvuTblCode(ByVal param As ChucvuDTO)
        Try
            If param.ID <> 0 Then
                Dim count = (From p In Context.HU_TITLE_TBL Where p.ID <> param.ID And p.CODE = param.CODE Select p).Count
                If count > 0 Then
                    Return False
                End If
            Else
                Dim count = (From p In Context.HU_TITLE_TBL Where p.CODE = param.CODE Select p).Count
                If count > 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyChucvuTbl(ByVal objTitle As ChucvuDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_TITLE_TBL
        Try
            objTitleData = (From p In Context.HU_TITLE_TBL Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveChucvuTbl(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE_TBL)
        Try
            lstTitleData = (From p In Context.HU_TITLE_TBL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                lstTitleData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteChucvuTbl(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE_TBL)
        Try

            lstTitleData = (From p In Context.HU_TITLE_TBL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_TITLE_TBL.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetChucvuTblByID(ByVal sID As Decimal) As List(Of ChucvuDTO)

        Try
            Dim query = (From p In Context.HU_TITLE_TBL.ToList Where p.ID = sID
                         Select New ChucvuDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try



    End Function
#End Region

#Region "TitleConcurrent"
    Public Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleConcurrentDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_TITLE_CONCURRENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New TitleConcurrentDTO With {
                                   .ID = p.ID,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_NAME = org.NAME_VN,
                                   .TITLE_ID = p.TITLE_ID,
                                   .NAME = title.NAME_VN,
                                   .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                   .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                   .EMPLOYEE_NAME = e.FULLNAME_VN,
                                   .DECISION_NO = p.DECISION_NO,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}
            Dim lst = query
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)

        Try

            Dim query = From p In Context.HU_TITLE_CONCURRENT
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        Select New TitleConcurrentDTO With {
                                   .ID = p.ID,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_NAME = org.NAME_VN,
                                   .TITLE_ID = p.TITLE_ID,
                                   .NAME = p.NAME,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleConcurrentData As New HU_TITLE_CONCURRENT
        Dim iCount As Integer = 0
        Try
            objTitleConcurrentData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE_CONCURRENT.EntitySet.Name)
            objTitleConcurrentData.ORG_ID = objTitleConcurrent.ORG_ID
            objTitleConcurrentData.TITLE_ID = objTitleConcurrent.TITLE_ID
            objTitleConcurrentData.NAME = objTitleConcurrent.NAME
            objTitleConcurrentData.EFFECT_DATE = objTitleConcurrent.EFFECT_DATE
            objTitleConcurrentData.EXPIRE_DATE = objTitleConcurrent.EXPIRE_DATE
            objTitleConcurrentData.EMPLOYEE_ID = objTitleConcurrent.EMPLOYEE_ID
            objTitleConcurrentData.NOTE = objTitleConcurrent.NOTE
            objTitleConcurrentData.DECISION_NO = objTitleConcurrent.DECISION_NO
            Context.HU_TITLE_CONCURRENT.AddObject(objTitleConcurrentData)
            Context.SaveChanges(log)
            gID = objTitleConcurrentData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleConcurrentData As HU_TITLE_CONCURRENT
        Try
            objTitleConcurrentData = (From p In Context.HU_TITLE_CONCURRENT Where p.ID = objTitleConcurrent.ID).FirstOrDefault
            objTitleConcurrentData.ORG_ID = objTitleConcurrent.ORG_ID
            objTitleConcurrentData.TITLE_ID = objTitleConcurrent.TITLE_ID
            objTitleConcurrentData.NAME = objTitleConcurrent.NAME
            objTitleConcurrentData.EFFECT_DATE = objTitleConcurrent.EFFECT_DATE
            objTitleConcurrentData.EXPIRE_DATE = objTitleConcurrent.EXPIRE_DATE
            objTitleConcurrentData.EMPLOYEE_ID = objTitleConcurrent.EMPLOYEE_ID
            objTitleConcurrentData.NOTE = objTitleConcurrent.NOTE
            objTitleConcurrentData.DECISION_NO = objTitleConcurrent.DECISION_NO
            Context.SaveChanges(log)
            gID = objTitleConcurrentData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteTitleConcurrent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleConcurrentData As List(Of HU_TITLE_CONCURRENT)
        Try
            lstTitleConcurrentData = (From p In Context.HU_TITLE_CONCURRENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleConcurrentData.Count - 1
                Context.HU_TITLE_CONCURRENT.DeleteObject(lstTitleConcurrentData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Document"
    Public Function GetAll_Document(ByVal _filter As DocumentDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DocumentDTO)

        Try
            Dim query = From p In Context.HU_DOCUMENT
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_DOCUMENT_ID).DefaultIfEmpty
                        Select New DocumentDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .MUST_HAVE = If(p.MUST_HAVE = -1, True, False),
                                   .ALLOW_UPLOAD_FILE = If(p.ALLOW_UPLOAD_FILE = -1, True, False),
                                  .TYPE_DOCUMENT_ID = p.TYPE_DOCUMENT_ID,
                                   .TYPE_DOCUMENT_NAME = ot.NAME_VN}
            Dim lst = query
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveDocument(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstDocumentData As List(Of HU_DOCUMENT)
        Try
            lstDocumentData = (From p In Context.HU_DOCUMENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstDocumentData.Count - 1
                lstDocumentData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function DeleteDocument(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstDocumentData As List(Of HU_DOCUMENT)
        Try

            lstDocumentData = (From p In Context.HU_DOCUMENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstDocumentData.Count - 1
                Context.HU_DOCUMENT.DeleteObject(lstDocumentData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function Check_Exit_Document(ByVal objDocument As DocumentDTO) As Decimal
        Dim count As Integer
        Try
            count = (From p In Context.HU_DOCUMENT
                     Where p.CODE = objDocument.CODE And (p.ID <> objDocument.ID Or p.ID = Nothing)).ToList.Count
            If count > 0 Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ModifyDocument(ByVal objDocument As DocumentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDocumentData As HU_DOCUMENT
        Try
            objDocumentData = (From p In Context.HU_DOCUMENT Where p.ID = objDocument.ID).FirstOrDefault
            objDocumentData.ID = objDocument.ID
            objDocumentData.CODE = objDocument.CODE
            objDocumentData.NAME_EN = objDocument.NAME_EN
            objDocumentData.NAME_VN = objDocument.NAME_VN
            objDocumentData.ACTFLG = objDocument.ACTFLG
            objDocumentData.TYPE_DOCUMENT_ID = objDocument.TYPE_DOCUMENT_ID
            objDocumentData.MUST_HAVE = objDocument.CHK_MUST_HAVE
            objDocumentData.ALLOW_UPLOAD_FILE = objDocument.ALLOW_UPLOAD_FILE
            Context.SaveChanges(log)
            gID = objDocumentData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function InsertDocument(ByVal objDocument As DocumentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDocumentData As New HU_DOCUMENT
        Dim iCount As Integer = 0
        Try
            objDocumentData.ID = Utilities.GetNextSequence(Context, Context.HU_DOCUMENT.EntitySet.Name)
            objDocumentData.CODE = objDocument.CODE
            objDocumentData.NAME_EN = objDocument.NAME_EN
            objDocumentData.NAME_VN = objDocument.NAME_VN
            objDocumentData.ACTFLG = objDocument.ACTFLG
            objDocumentData.TYPE_DOCUMENT_ID = objDocument.TYPE_DOCUMENT_ID
            objDocumentData.MUST_HAVE = objDocument.CHK_MUST_HAVE
            objDocumentData.ALLOW_UPLOAD_FILE = objDocument.ALLOW_UPLOAD_FILE
            Context.HU_DOCUMENT.AddObject(objDocumentData)
            Context.SaveChanges(log)
            gID = objDocumentData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region
#Region "bhld item"

    Public Function GetBHLDItem(ByVal _filter As BHLDItemDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BHLDItemDTO)

        Try

            Dim query = From p In Context.HU_BHLD_ITEM

            Dim lst = query.Select(Function(f) New BHLDItemDTO With {
                                       .ID = f.ID,
                                       .CODE = f.CODE,
                                       .NAME_VN = f.NAME_VN,
                                       .UNIT = f.UNIT,
                                       .MONEY = f.MONEY,
                                       .ORDER_NUM = f.ORDER_NUM,
                                       .REMARK = f.REMARK,
                                       .AUTOGEN = If(f.AUTOGEN = -1, True, False),
                                       .HIDE = If(f.HIDE = -1, True, False),
                                       .ACTFLG = If(f.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = f.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If

            If _filter.UNIT IsNot Nothing Then
                lst = lst.Where(Function(p) p.UNIT = _filter.UNIT)
            End If

            If _filter.MONEY IsNot Nothing Then
                lst = lst.Where(Function(p) p.MONEY = _filter.MONEY)
            End If
            If _filter.ORDER_NUM IsNot Nothing Then
                lst = lst.Where(Function(p) p.ORDER_NUM = _filter.ORDER_NUM)
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function checkOrderNum(ByVal id As Decimal, ByVal num As Decimal) As Decimal
        Dim re = 0
        If id <> 0 Then
            re = (From p In Context.HU_BHLD_ITEM Where p.ID <> id And p.ORDER_NUM = num Select p).Count
        Else
            re = (From p In Context.HU_BHLD_ITEM Where p.ORDER_NUM = num Select p).Count
        End If
        Return re
    End Function

    Public Function InsertBHLDItem(ByVal objBHLDItem As BHLDItemDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objBHLDItemData As New HU_BHLD_ITEM
        Try
            objBHLDItemData.ID = Utilities.GetNextSequence(Context, Context.HU_BHLD_ITEM.EntitySet.Name)
            objBHLDItemData.CODE = objBHLDItem.CODE.Trim
            objBHLDItemData.NAME_VN = objBHLDItem.NAME_VN
            objBHLDItemData.REMARK = objBHLDItem.REMARK
            objBHLDItemData.UNIT = objBHLDItem.UNIT
            objBHLDItemData.MONEY = objBHLDItem.MONEY
            objBHLDItemData.ORDER_NUM = objBHLDItem.ORDER_NUM

            objBHLDItemData.ACTFLG = "A"
            objBHLDItemData.AUTOGEN = objBHLDItem.AUTOGEN
            objBHLDItemData.HIDE = objBHLDItem.HIDE
            Context.HU_BHLD_ITEM.AddObject(objBHLDItemData)
            Context.SaveChanges(log)
            gID = objBHLDItemData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateBHLDItem(ByVal _validate As BHLDItemDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_BHLD_ITEM
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_BHLD_ITEM
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_BHLD_ITEM
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_BHLD_ITEM
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyBHLDItem(ByVal objBHLDItem As BHLDItemDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objBHLDItemData As New HU_BHLD_ITEM With {.ID = objBHLDItem.ID}
        Try
            objBHLDItemData = (From p In Context.HU_BHLD_ITEM Where p.ID = objBHLDItem.ID).FirstOrDefault
            objBHLDItemData.ID = objBHLDItem.ID
            objBHLDItemData.CODE = objBHLDItem.CODE.Trim
            objBHLDItemData.NAME_VN = objBHLDItem.NAME_VN
            objBHLDItemData.REMARK = objBHLDItem.REMARK
            objBHLDItemData.UNIT = objBHLDItem.UNIT
            objBHLDItemData.MONEY = objBHLDItem.MONEY
            objBHLDItemData.ORDER_NUM = objBHLDItem.ORDER_NUM
            objBHLDItemData.ACTFLG = "A"
            objBHLDItemData.AUTOGEN = objBHLDItem.AUTOGEN
            objBHLDItemData.HIDE = objBHLDItem.HIDE
            Context.SaveChanges(log)
            gID = objBHLDItemData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function ActiveBHLDItem(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstContractTypeData As List(Of HU_BHLD_ITEM)
        Try
            lstContractTypeData = (From p In Context.HU_BHLD_ITEM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstContractTypeData.Count - 1
                lstContractTypeData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteBHLDItem(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstContractTypeData As List(Of HU_BHLD_ITEM)
        Try
            lstContractTypeData = (From p In Context.HU_BHLD_ITEM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstContractTypeData.Count - 1
                Context.HU_BHLD_ITEM.DeleteObject(lstContractTypeData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "ContractType"

    Public Function GetContractType(ByVal _filter As ContractTypeDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO)

        Try

            Dim query = From p In Context.HU_CONTRACT_TYPE
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                        From ot8 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FLOWING_MD).DefaultIfEmpty
                        From ot9 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CODE_GET_ENDDATE).DefaultIfEmpty

            Dim lst = query.Select(Function(f) New ContractTypeDTO With {
                                       .ID = f.p.ID,
                                       .CODE = f.p.CODE,
                                       .PERIOD = f.p.PERIOD,
                                       .REMARK = f.p.REMARK,
                                       .NAME = f.p.NAME,
                                       .NAME_VISIBLE_ONFORM = f.p.NAME_VISIBLE_ONFORM,
                                       .CODE_GET_ENDDATE_ID = f.p.CODE_GET_ENDDATE,
                                       .CODE_GET_ENDDATE = f.ot9.NAME_VN,
                                       .FLOWING_MD_ID = f.p.FLOWING_MD,
                                       .FLOWING_MD = f.ot8.NAME_VN,
                                       .TYPE_ID = f.p.TYPE_ID,
                                       .TYPE_NAME = f.ot.NAME_VN,
                                       .IS_HOCVIEC = f.p.IS_HOCVIEC,
                                       .IS_REQUIREMENT = f.p.IS_REQUIREMENT,
                                       .IS_HSL = f.p.IS_HSL,
                                       .ACTFLG = If(f.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = f.p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TYPE_NAME.ToUpper.Contains(_filter.TYPE_NAME.ToUpper))
            End If

            If _filter.NAME_VISIBLE_ONFORM <> "" Then
                lst = lst.Where(Function(p) p.NAME_VISIBLE_ONFORM.ToUpper.Contains(_filter.NAME_VISIBLE_ONFORM.ToUpper))
            End If

            If _filter.CODE_GET_ENDDATE <> "" Then
                lst = lst.Where(Function(p) p.CODE_GET_ENDDATE.ToUpper.Contains(_filter.CODE_GET_ENDDATE.ToUpper))
            End If

            If _filter.FLOWING_MD <> "" Then
                lst = lst.Where(Function(p) p.FLOWING_MD.ToUpper.Contains(_filter.FLOWING_MD.ToUpper))
            End If

            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.PERIOD <> 0 Then
                lst = lst.Where(Function(p) p.PERIOD = _filter.PERIOD)
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertContractType(ByVal objContractType As ContractTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objContractTypeData As New HU_CONTRACT_TYPE
        Try
            objContractTypeData.ID = Utilities.GetNextSequence(Context, Context.HU_CONTRACT_TYPE.EntitySet.Name)
            objContractTypeData.CODE = objContractType.CODE.Trim
            objContractTypeData.NAME = objContractType.NAME.Trim
            objContractTypeData.REMARK = objContractType.REMARK.Trim
            objContractTypeData.PERIOD = objContractType.PERIOD
            objContractTypeData.TYPE_ID = objContractType.TYPE_ID
            objContractTypeData.ACTFLG = objContractType.ACTFLG
            objContractTypeData.NAME_VISIBLE_ONFORM = objContractType.NAME_VISIBLE_ONFORM
            objContractTypeData.FLOWING_MD = objContractType.FLOWING_MD_ID
            objContractTypeData.CODE_GET_ENDDATE = objContractType.CODE_GET_ENDDATE_ID
            objContractTypeData.IS_HOCVIEC = objContractType.IS_HOCVIEC
            objContractTypeData.IS_HSL = objContractType.IS_HSL
            objContractTypeData.IS_REQUIREMENT = objContractType.IS_REQUIREMENT
            Context.HU_CONTRACT_TYPE.AddObject(objContractTypeData)
            Context.SaveChanges(log)
            gID = objContractTypeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateContractType(ByVal _validate As ContractTypeDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_CONTRACT_TYPE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyContractType(ByVal objContractType As ContractTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractTypeData As New HU_CONTRACT_TYPE With {.ID = objContractType.ID}
        Try
            objContractTypeData = (From p In Context.HU_CONTRACT_TYPE Where p.ID = objContractType.ID).FirstOrDefault
            objContractTypeData.ID = objContractType.ID
            objContractTypeData.CODE = objContractType.CODE.Trim
            objContractTypeData.NAME = objContractType.NAME.Trim
            objContractTypeData.PERIOD = objContractType.PERIOD
            objContractTypeData.REMARK = objContractType.REMARK.Trim
            objContractTypeData.TYPE_ID = objContractType.TYPE_ID
            objContractTypeData.NAME_VISIBLE_ONFORM = objContractType.NAME_VISIBLE_ONFORM
            objContractTypeData.FLOWING_MD = objContractType.FLOWING_MD_ID
            objContractTypeData.CODE_GET_ENDDATE = objContractType.CODE_GET_ENDDATE_ID
            objContractTypeData.IS_HOCVIEC = objContractType.IS_HOCVIEC
            objContractTypeData.IS_HSL = objContractType.IS_HSL
            objContractTypeData.IS_REQUIREMENT = objContractType.IS_REQUIREMENT
            Context.SaveChanges(log)
            gID = objContractTypeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function ActiveContractType(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstContractTypeData As List(Of HU_CONTRACT_TYPE)
        Try
            lstContractTypeData = (From p In Context.HU_CONTRACT_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstContractTypeData.Count - 1
                lstContractTypeData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteContractType(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstContractTypeData As List(Of HU_CONTRACT_TYPE)
        Try
            lstContractTypeData = (From p In Context.HU_CONTRACT_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstContractTypeData.Count - 1
                Context.HU_CONTRACT_TYPE.DeleteObject(lstContractTypeData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "WelfareList"

    Public Function GetWelfareList(ByVal _filter As WelfareListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_WELFARE_LIST
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = p.ORG_ID)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(org) org.ORG_ID = o.ID And
                                                                    org.USERNAME = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New WelfareListDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME = p.p.NAME,
                                       .CONTRACT_TYPE = p.p.CONTRACT_TYPE,
                                       .CONTRACT_TYPE_NAME = p.p.CONTRACT_TYPE_NAME,
                                       .GENDER = p.p.GENDER,
                                       .ORG_ID = p.p.ORG_ID,
                                       .TITLE_GROUP_ID = p.p.TITLE_GROUP_ID,
                                       .TITLE_GROUP_NAME = p.ot.NAME_VN,
                                       .GENDER_NAME = p.p.GENDER_NAME,
                                       .SENIORITY = p.p.SENIORITY,
                                       .CHILD_OLD_FROM = p.p.CHILD_OLD_FROM,
                                       .CHILD_OLD_TO = p.p.CHILD_OLD_TO,
                                       .MONEY = p.p.MONEY,
                                       .START_DATE = p.p.START_DATE,
                                       .END_DATE = p.p.END_DATE,
                                       .IS_AUTO = p.p.IS_AUTO,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .ID_NAME = p.p.ID_NAME,
                                       .SENIORITY_FROM = p.p.SENIORITY_FROM})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.CONTRACT_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
            End If
            If _filter.GENDER_NAME <> "" Then
                lst = lst.Where(Function(p) p.GENDER_NAME.ToUpper.Contains(_filter.GENDER_NAME.ToUpper))
            End If
            If _filter.SENIORITY <> 0 Then
                lst = lst.Where(Function(p) p.SENIORITY = _filter.SENIORITY)
            End If
            If _filter.MONEY <> 0 Then
                lst = lst.Where(Function(p) p.MONEY = _filter.MONEY)
            End If
            If _filter.SENIORITY <> 0 Then
                lst = lst.Where(Function(p) p.SENIORITY = _filter.SENIORITY)
            End If
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If
            If _filter.IS_AUTO IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_AUTO = _filter.IS_AUTO)
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertWelfareList(ByVal objWelfareList As WelfareListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objWelfareListData As New HU_WELFARE_LIST
        Try
            objWelfareListData.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
            objWelfareListData.CODE = objWelfareList.CODE.Trim
            objWelfareListData.NAME = objWelfareList.NAME.Trim
            objWelfareListData.CONTRACT_TYPE = objWelfareList.CONTRACT_TYPE
            objWelfareListData.CONTRACT_TYPE_NAME = objWelfareList.CONTRACT_TYPE_NAME
            objWelfareListData.GENDER = objWelfareList.GENDER
            objWelfareListData.TITLE_GROUP_ID = objWelfareList.TITLE_GROUP_ID
            objWelfareListData.GENDER_NAME = objWelfareList.GENDER_NAME
            objWelfareListData.SENIORITY = objWelfareList.SENIORITY
            objWelfareListData.SENIORITY_FROM = objWelfareList.SENIORITY_FROM
            objWelfareListData.CHILD_OLD_FROM = objWelfareList.CHILD_OLD_FROM
            objWelfareListData.CHILD_OLD_TO = objWelfareList.CHILD_OLD_TO
            objWelfareListData.MONEY = objWelfareList.MONEY
            objWelfareListData.START_DATE = objWelfareList.START_DATE
            objWelfareListData.END_DATE = objWelfareList.END_DATE
            objWelfareListData.IS_AUTO = objWelfareList.IS_AUTO
            objWelfareListData.ACTFLG = objWelfareList.ACTFLG
            objWelfareListData.ORG_ID = objWelfareList.ORG_ID
            objWelfareListData.ID_NAME = objWelfareList.ID_NAME
            Context.HU_WELFARE_LIST.AddObject(objWelfareListData)
            Context.SaveChanges(log)
            gID = objWelfareListData.ID
            If objWelfareList.CONTRACT_TYPE IsNot Nothing Then
                Dim lstT = objWelfareList.CONTRACT_TYPE.Split(",")
                If lstT IsNot Nothing Then
                    For Each s As String In lstT
                        Dim obj As New HU_WELFARE_LIST_GW
                        obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                        obj.HU_WELFARE_LIST_ID = gID
                        obj.CONTRACT_TYPE_ID = Utilities.Obj2Decima(s)
                        Context.HU_WELFARE_LIST_GW.AddObject(obj)
                    Next
                End If
            End If

            If objWelfareList.GENDER IsNot Nothing Then
                Dim lstG = objWelfareList.GENDER.Split(",")
                For Each s As String In lstG
                    Dim obj As New HU_WELFARE_LIST_GW
                    obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                    obj.HU_WELFARE_LIST_ID = gID
                    obj.GENDER_ID = Utilities.Obj2Decima(s)
                    Context.HU_WELFARE_LIST_GW.AddObject(obj)
                Next
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateWelfareList(ByVal _validate As WelfareListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_WELFARE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_WELFARE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function ModifyWelfareList(ByVal objWelfareList As WelfareListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWelfareListData As New HU_WELFARE_LIST With {.ID = objWelfareList.ID}
        Try
            objWelfareListData = (From p In Context.HU_WELFARE_LIST Where p.ID = objWelfareList.ID).FirstOrDefault
            objWelfareListData.ID = objWelfareList.ID
            objWelfareListData.CODE = objWelfareList.CODE.Trim
            objWelfareListData.NAME = objWelfareList.NAME.Trim
            objWelfareListData.CONTRACT_TYPE = objWelfareList.CONTRACT_TYPE
            objWelfareListData.CONTRACT_TYPE_NAME = objWelfareList.CONTRACT_TYPE_NAME
            objWelfareListData.GENDER = objWelfareList.GENDER
            objWelfareListData.GENDER_NAME = objWelfareList.GENDER_NAME
            objWelfareListData.SENIORITY = objWelfareList.SENIORITY
            objWelfareListData.SENIORITY_FROM = objWelfareList.SENIORITY_FROM
            objWelfareListData.CHILD_OLD_FROM = objWelfareList.CHILD_OLD_FROM
            objWelfareListData.CHILD_OLD_TO = objWelfareList.CHILD_OLD_TO
            objWelfareListData.MONEY = objWelfareList.MONEY
            objWelfareListData.TITLE_GROUP_ID = objWelfareList.TITLE_GROUP_ID
            objWelfareListData.START_DATE = objWelfareList.START_DATE
            objWelfareListData.END_DATE = objWelfareList.END_DATE
            objWelfareListData.IS_AUTO = objWelfareList.IS_AUTO
            objWelfareListData.ID_NAME = objWelfareList.ID_NAME
            Context.SaveChanges()
            Context.SaveChanges(log)
            gID = objWelfareListData.ID

            Dim lstWelfareListData As List(Of HU_WELFARE_LIST_GW)
            lstWelfareListData = (From p In Context.HU_WELFARE_LIST_GW Where p.HU_WELFARE_LIST_ID = objWelfareListData.ID).ToList
            For index = 0 To lstWelfareListData.Count - 1
                Context.HU_WELFARE_LIST_GW.DeleteObject(lstWelfareListData(index))
            Next
            Context.SaveChanges(log)
            If objWelfareList.CONTRACT_TYPE IsNot Nothing Then
                Dim lstT = objWelfareList.CONTRACT_TYPE.Split(",")
                If lstT IsNot Nothing Then
                    For Each s As String In lstT
                        Dim obj As New HU_WELFARE_LIST_GW
                        obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                        obj.HU_WELFARE_LIST_ID = gID
                        obj.CONTRACT_TYPE_ID = Utilities.Obj2Decima(s)
                        Context.HU_WELFARE_LIST_GW.AddObject(obj)
                    Next
                End If
            End If

            If objWelfareList.GENDER IsNot Nothing Then
                Dim lstG = objWelfareList.GENDER.Split(",")
                If lstG IsNot Nothing Then
                    For Each s As String In lstG
                        Dim obj As New HU_WELFARE_LIST_GW
                        obj.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_LIST.EntitySet.Name)
                        obj.HU_WELFARE_LIST_ID = gID
                        obj.GENDER_ID = Utilities.Obj2Decima(s)
                        Context.HU_WELFARE_LIST_GW.AddObject(obj)
                    Next
                End If
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveWelfareList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstWelfareListData As List(Of HU_WELFARE_LIST)
        lstWelfareListData = (From p In Context.HU_WELFARE_LIST Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstWelfareListData.Count - 1
            lstWelfareListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteWelfareList(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstWelfareListData As List(Of HU_WELFARE_LIST)
        Try
            'Check ID da su dung trong phuc loi ca nhan hay tap the chua? HU_WELFARE_MNG
            Dim lstWelfareListUse As List(Of HU_WELFARE_MNG)
            lstWelfareListUse = (From mng In Context.HU_WELFARE_MNG Where lstID.Contains(mng.WELFARE_ID)).ToList
            If (lstWelfareListUse.Count > 0) Then

                For Each l In lstWelfareListUse
                    If (l.WELFARE_ID IsNot Nothing) Then
                        lstID.Remove(l.WELFARE_ID)
                    End If
                Next
            End If
            If (lstID.Count > 0) Then
                lstWelfareListData = (From Wel In Context.HU_WELFARE_LIST Where lstID.Contains(Wel.ID)).ToList
                For index = 0 To lstWelfareListData.Count - 1
                    Context.HU_WELFARE_LIST.DeleteObject(lstWelfareListData(index))
                Next
                Context.SaveChanges(log)
                Return True
            Else
                Context.SaveChanges(log)
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "AllowanceList"

    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)

        Try
            Dim query = From p In Context.HU_ALLOWANCE_LIST

            Dim lst = query.Select(Function(p) New AllowanceListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .IS_INSURANCE = p.IS_INSURANCE,
                                       .IS_DEDUCT = p.IS_DEDUCT,
                                       .ORDERS = p.ORDERS,
                                       .ALLOW_TYPE = p.ALLOWANCE_TYPE,
                                       .ALLOW_TYPE_NAME = If(p.ALLOWANCE_TYPE = 1, "Theo tháng",
                                                        If(p.ALLOWANCE_TYPE = 2, "Theo công hưởng lương",
                                                           If(p.ALLOWANCE_TYPE = 3, "Theo công làm việc", ""))),
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .IS_CONTRACT = p.IS_CONTRACT})
            If _filter.ID > 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ALLOW_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.ALLOW_TYPE_NAME.ToUpper.Contains(_filter.ALLOW_TYPE_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertAllowanceList(ByVal objAllowanceList As AllowanceListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objAllowanceListData As New HU_ALLOWANCE_LIST
        Try
            objAllowanceListData.ID = Utilities.GetNextSequence(Context, Context.HU_ALLOWANCE_LIST.EntitySet.Name)
            objAllowanceListData.CODE = objAllowanceList.CODE.Trim
            objAllowanceListData.NAME = objAllowanceList.NAME.Trim
            objAllowanceListData.ACTFLG = objAllowanceList.ACTFLG
            objAllowanceListData.ALLOWANCE_TYPE = objAllowanceList.ALLOW_TYPE
            objAllowanceListData.REMARK = objAllowanceList.REMARK
            objAllowanceListData.IS_INSURANCE = objAllowanceList.IS_INSURANCE
            objAllowanceListData.IS_DEDUCT = objAllowanceList.IS_DEDUCT
            objAllowanceListData.IS_CONTRACT = objAllowanceList.IS_CONTRACT
            objAllowanceListData.ORDERS = objAllowanceList.ORDERS
            Context.HU_ALLOWANCE_LIST.AddObject(objAllowanceListData)
            Context.SaveChanges(log)
            gID = objAllowanceListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateAllowanceList(ByVal _validate As AllowanceListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_ALLOWANCE_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyAllowanceList(ByVal objAllowanceList As AllowanceListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAllowanceListData As New HU_ALLOWANCE_LIST With {.ID = objAllowanceList.ID}
        Try
            objAllowanceListData = (From p In Context.HU_ALLOWANCE_LIST Where p.ID = objAllowanceList.ID).FirstOrDefault
            objAllowanceListData.ID = objAllowanceList.ID
            objAllowanceListData.CODE = objAllowanceList.CODE
            objAllowanceListData.NAME = objAllowanceList.NAME
            objAllowanceListData.ALLOWANCE_TYPE = objAllowanceList.ALLOW_TYPE
            objAllowanceListData.REMARK = objAllowanceList.REMARK
            objAllowanceListData.IS_INSURANCE = objAllowanceList.IS_INSURANCE
            objAllowanceListData.IS_DEDUCT = objAllowanceList.IS_DEDUCT
            objAllowanceListData.IS_CONTRACT = objAllowanceList.IS_CONTRACT
            objAllowanceListData.ORDERS = objAllowanceList.ORDERS
            Context.SaveChanges(log)
            gID = objAllowanceListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveAllowanceList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstAllowanceListData As List(Of HU_ALLOWANCE_LIST)
        lstAllowanceListData = (From p In Context.HU_ALLOWANCE_LIST Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstAllowanceListData.Count - 1
            lstAllowanceListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteAllowanceList(ByVal lstAllowanceList() As AllowanceListDTO, ByVal log As UserLog) As Boolean
        Dim lstAllowanceListData As List(Of HU_ALLOWANCE_LIST)
        Dim lstIDAllowanceList As List(Of Decimal) = (From p In lstAllowanceList.ToList Select p.ID).ToList
        Try
            lstAllowanceListData = (From p In Context.HU_ALLOWANCE_LIST Where lstIDAllowanceList.Contains(p.ID)).ToList
            For index = 0 To lstAllowanceListData.Count - 1
                Context.HU_ALLOWANCE_LIST.DeleteObject(lstAllowanceListData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "RelationshipList"

    Public Function GetRelationshipGroupList() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_REL_GROUP_LIST",
                                           New With {.P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetRelationshipList(ByVal _filter As RelationshipListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO)

        Try
            Dim query = From p In Context.HU_RELATIONSHIP_LIST
                        From g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REL_GROUP_ID).DefaultIfEmpty
                        Select New RelationshipListDTO With {
                                       .ID = p.ID,
                                       .REL_GROUP_ID = p.REL_GROUP_ID,
                                       .REL_GROUP_NAME = g.NAME_VN,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.ID > 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.REL_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.REL_GROUP_NAME.ToUpper.Contains(_filter.REL_GROUP_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertRelationshipList(ByVal objRelationshipList As RelationshipListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objRelationshipListData As New HU_RELATIONSHIP_LIST
        Try
            objRelationshipListData.ID = Utilities.GetNextSequence(Context, Context.HU_RELATIONSHIP_LIST.EntitySet.Name)
            objRelationshipListData.REL_GROUP_ID = objRelationshipList.REL_GROUP_ID
            objRelationshipListData.CODE = objRelationshipList.CODE.Trim
            objRelationshipListData.NAME = objRelationshipList.NAME.Trim
            objRelationshipListData.ACTFLG = objRelationshipList.ACTFLG
            objRelationshipListData.REMARK = objRelationshipList.REMARK
            Context.HU_RELATIONSHIP_LIST.AddObject(objRelationshipListData)
            Context.SaveChanges(log)
            gID = objRelationshipListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateRelationshipList(ByVal _validate As RelationshipListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_RELATIONSHIP_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyRelationshipList(ByVal objRelationshipList As RelationshipListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRelationshipListData As New HU_RELATIONSHIP_LIST With {.ID = objRelationshipList.ID}
        Try
            objRelationshipListData = (From p In Context.HU_RELATIONSHIP_LIST Where p.ID = objRelationshipList.ID).FirstOrDefault
            objRelationshipListData.ID = objRelationshipList.ID
            objRelationshipListData.REL_GROUP_ID = objRelationshipList.REL_GROUP_ID
            objRelationshipListData.CODE = objRelationshipList.CODE
            objRelationshipListData.NAME = objRelationshipList.NAME
            objRelationshipListData.REMARK = objRelationshipList.REMARK
            Context.SaveChanges(log)
            gID = objRelationshipListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveRelationshipList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstRelationshipListData As List(Of HU_RELATIONSHIP_LIST)
        lstRelationshipListData = (From p In Context.HU_RELATIONSHIP_LIST Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstRelationshipListData.Count - 1
            lstRelationshipListData(index).ACTFLG = sActive
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function DeleteRelationshipList(ByVal lstRelationshipList() As RelationshipListDTO, ByVal log As UserLog) As Boolean
        Dim lstRelationshipListData As List(Of HU_RELATIONSHIP_LIST)
        Dim lstIDRelationshipList As List(Of Decimal) = (From p In lstRelationshipList.ToList Select p.ID).ToList
        Try
            lstRelationshipListData = (From p In Context.HU_RELATIONSHIP_LIST Where lstIDRelationshipList.Contains(p.ID)).ToList
            For index = 0 To lstRelationshipListData.Count - 1
                Context.HU_RELATIONSHIP_LIST.DeleteObject(lstRelationshipListData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Organization"
    Public Function GetTreeOrgByID(ByVal ID As Decimal) As OrganizationTreeDTO
        Dim orgTree As OrganizationTreeDTO
        Try
            orgTree = (From p In Context.HUV_ORGANIZATION
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID5).DefaultIfEmpty
                       From E In Context.HU_EMPLOYEE.Where(Function(F) F.ID = o.REPRESENTATIVE_ID).DefaultIfEmpty
                       Where p.ID = ID
                       Select New OrganizationTreeDTO With {.ID = p.ID,
                                                            .NAME_VN = p.NAME_VN,
                                                            .CODE = p.CODE,
                                                            .ORG_CODE1 = p.ORG_CODE1,
                                                            .ORG_CODE2 = p.ORG_CODE2,
                                                            .ORG_CODE3 = p.ORG_CODE3,
                                                            .ORG_CODE4 = p.ORG_CODE4,
                                                            .ORG_CODE5 = p.ORG_CODE5,
                                                            .ORG_CODE6 = p.ORG_CODE6,
                                                            .ORG_CODE7 = p.ORG_CODE7,
                                                            .ORG_CODE8 = p.ORG_CODE8,
                                                            .ORG_CODE9 = p.ORG_CODE9,
                                                            .ORG_ID1 = p.ORG_ID1,
                                                            .ORG_ID2 = p.ORG_ID2,
                                                            .ORG_ID3 = p.ORG_ID3,
                                                            .ORG_ID4 = p.ORG_ID4,
                                                            .ORG_ID5 = p.ORG_ID5,
                                                            .ORG_ID6 = p.ORG_ID6,
                                                            .ORG_ID7 = p.ORG_ID7,
                                                            .ORG_ID8 = p.ORG_ID8,
                                                            .ORG_ID9 = p.ORG_ID9,
                                                            .ORG_NAME = p.ORG_NAME,
                                                            .ORG_NAME1 = p.ORG_NAME1,
                                                            .ORG_NAME2 = p.ORG_NAME2,
                                                            .ORG_NAME3 = p.ORG_NAME3,
                                                            .ORG_NAME4 = p.ORG_NAME4,
                                                            .ORG_NAME5 = p.ORG_NAME5,
                                                            .ORG_NAME6 = p.ORG_NAME6,
                                                            .ORG_NAME7 = p.ORG_NAME7,
                                                            .ORG_NAME8 = p.ORG_NAME8,
                                                            .ORG_NAME9 = p.ORG_NAME9,
                                                            .ORG_PATH = p.ORG_PATH,
                                                            .PARENT_ID = p.PARENT_ID,
                                                            .REPRESENTATIVE_ID = o.REPRESENTATIVE_ID,
                                                            .REPRESENTATIVE_NAME = E.FULLNAME_VN
                           }).SingleOrDefault
            Return orgTree
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO
        Dim query As OrganizationDTO
        Try
            query = (From p In Context.HU_ORGANIZATION
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty
                     From ov In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ID)
                     Where p.ID = ID
                     Select New OrganizationDTO With {.ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .NAME_VN = p.NAME_VN,
                                                      .NAME_EN = p.NAME_EN,
                                                      .PARENT_ID = p.PARENT_ID,
                                                      .EFFECT_DATE = p.EFFECT_DATE,
                                                      .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                      .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                      .REMARK = p.REMARK,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                      .ADDRESS = p.ADDRESS,
                                                      .FAX = p.FAX,
                                                      .NUMBER_BUSINESS = p.NUMBER_BUSINESS,
                                                      .DATE_BUSINESS = p.DATE_BUSINESS,
                                                      .PIT_NO = p.PIT_NO,
                                                      .MOBILE = p.MOBILE,
                                                      .REPRESENTATIVE_ID = p.REPRESENTATIVE_ID,
                                                      .REPRESENTATIVE_CODE = e.EMPLOYEE_CODE,
                                                      .REPRESENTATIVE_NAME = e.FULLNAME_VN,
                                                      .U_INSURANCE = p.U_INSURANCE,
                                                      .INFOR_4 = p.INFOR_4,
                                                      .INFOR_5 = p.INFOR_5,
                                                      .ORG_ID2 = ov.ORG_ID2,
                                                      .ORG_NAME2 = ov.ORG_NAME2,
                                                    .ORG_LEVEL = p.ORG_LEVEL,
                                                    .REGION_ID = p.REGION_ID}).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOrganization(ByVal sACT As String) As List(Of OrganizationDTO)
        Dim query As ObjectQuery(Of OrganizationDTO)
        Try
            If sACT = "" Then
                query = (From p In Context.HU_ORGANIZATION
                         From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty()
                         From v In Context.HU_EMPLOYEE_CV.Where(Function(b) b.EMPLOYEE_ID = e.ID).DefaultIfEmpty()
                         From t In Context.HU_TITLE.Where(Function(a) a.ID = e.TITLE_ID).DefaultIfEmpty()
                         From PO In Context.HU_TITLE.Where(Function(a) a.ID = p.POSITION_ID).DefaultIfEmpty()
                         From o In Context.OT_OTHER_LIST.Where(Function(a) a.ID = p.ORG_LEVEL).DefaultIfEmpty()
                         From ov In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ID).DefaultIfEmpty
                         Order By p.ORD_NO, p.NAME_VN.ToUpper
                         Select New OrganizationDTO With {.ID = p.ID,
                                                    .CODE = p.CODE,
                                                    .NAME_VN = p.NAME_VN,
                                                    .NAME_EN = p.NAME_EN,
                                                    .PARENT_ID = p.PARENT_ID,
                                                    .PARENT_NAME = parent.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                    .END_DATE = p.END_DATE,
                                                    .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                    .REMARK = p.REMARK,
                                                    .ACTFLG = p.ACTFLG,
                                                    .ADDRESS = p.ADDRESS,
                                                    .FAX = p.FAX,
                                                    .EMAIL = p.EMAIL,
                                                    .MOBILE = p.MOBILE,
                                                    .PROVINCE_NAME = p.PROVINCE_NAME,
                                                    .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                    .ORD_NO = p.ORD_NO,
                                                    .NUMBER_BUSINESS = p.NUMBER_BUSINESS,
                                                    .DATE_BUSINESS = p.DATE_BUSINESS,
                                                    .PIT_NO = p.PIT_NO,
                                                    .UNIT_LEVEL = p.UNIT_LEVEL,
                                                    .REPRESENTATIVE_ID = p.REPRESENTATIVE_ID,
                                                    .REPRESENTATIVE_CODE = e.EMPLOYEE_CODE,
                                                    .REPRESENTATIVE_NAME = e.FULLNAME_VN,
                                                    .REPRESENTATIVE_PHONE = p.REPRESENTATIVE_PHONE,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .IMAGE = v.IMAGE,
                                                    .U_INSURANCE = p.U_INSURANCE,
                                                    .ORG_LEVEL = p.ORG_LEVEL,
                                                    .ORG_LEVEL_NAME = o.NAME_VN,
                                                    .REGION_ID = p.REGION_ID,
                                                    .TYPE_DECISION = p.TYPE_DECISION,
                                                    .NUMBER_DECISION = p.NUMBER_DECISION,
                                                    .CHK_ORGCHART = p.CHK_ORGCHART,
                                                    .LOCATION_WORK = p.LOCATION_WORK,
                                                    .SHORT_NAME = p.SHORT_NAME,
                                                    .INFOR_1 = p.INFOR_1,
                                                    .INFOR_2 = p.INFOR_2,
                                                    .INFOR_3 = p.INFOR_3,
                                                    .INFOR_4 = p.INFOR_4,
                                                    .INFOR_5 = p.INFOR_5,
                                                    .IPAY_GET_DTTD_DTPB = p.IPAY_GET_DTTD_DTPB,
                                                    .FILES = p.FILES,
                                                    .ORG_REG_ID = p.ORG_REG_ID,
                                                    .WEBSITE = p.WEBSITE,
                                                    .UY_BAN = p.UY_BAN,
                                                    .POSITION_ID = p.POSITION_ID,
                                                    .POSITION_NAME = PO.NAME_VN,
                                                    .ATTACH_FILE = p.ATTACH_FILE,
                                                    .FILENAME = p.FILENAME,
                                                    .ORG_ID2 = ov.ORG_ID2
                                                    })
                '.AutoGenTimeSheet = p.AUTOGENTIMESHEET
            Else
                query = (From p In Context.HU_ORGANIZATION
                         From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty
                         Where p.ACTFLG = sACT
                         Order By p.ORD_NO, p.NAME_VN.ToUpper
                         Select New OrganizationDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .PARENT_NAME = parent.NAME_VN,
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                          .END_DATE = p.END_DATE,
                                                          .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                          .REMARK = p.REMARK,
                                                          .ACTFLG = p.ACTFLG,
                                                          .ADDRESS = p.ADDRESS,
                                                          .FAX = p.FAX,
                                                          .PIT_NO = p.PIT_NO,
                                                          .EMAIL = p.EMAIL,
                                                          .MOBILE = p.MOBILE,
                                                          .PROVINCE_NAME = p.PROVINCE_NAME,
                                                          .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                          .ORD_NO = p.ORD_NO,
                                                          .REPRESENTATIVE_ID = p.REPRESENTATIVE_ID,
                                                          .REPRESENTATIVE_CODE = e.EMPLOYEE_CODE,
                                                          .REPRESENTATIVE_NAME = e.FULLNAME_VN,
                                                          .REPRESENTATIVE_PHONE = p.REPRESENTATIVE_PHONE,
                                                          .U_INSURANCE = p.U_INSURANCE,
                                                          .ORG_LEVEL = p.ORG_LEVEL,
                                                          .REGION_ID = p.REGION_ID,
                                                          .TYPE_DECISION = p.TYPE_DECISION,
                                                          .NUMBER_DECISION = p.NUMBER_DECISION,
                                                          .CHK_ORGCHART = p.CHK_ORGCHART,
                                                          .LOCATION_WORK = p.LOCATION_WORK,
                                                          .SHORT_NAME = p.SHORT_NAME,
                                                          .INFOR_1 = p.INFOR_1,
                                                          .INFOR_2 = p.INFOR_2,
                                                          .INFOR_3 = p.INFOR_3,
                                                          .INFOR_4 = p.INFOR_4,
                                                          .INFOR_5 = p.INFOR_5,
                                                          .FILES = p.FILES,
                                                          .WEBSITE = p.WEBSITE
                                                         })
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetMaxId() As Decimal
        Try
            Dim chuoi As Decimal = (From p In Context.HU_ORGANIZATION
                                    Select p.ID Order By ID Descending).FirstOrDefault
            Return chuoi
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetNameOrg(ByVal org_id As Decimal) As String
        Dim str As String = ""
        Try
            Dim chuoi = Context.HU_ORGANIZATION.Where(Function(f) f.ID = org_id).FirstOrDefault
            str = chuoi.NAME_VN
            Return str
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function InsertOrganization(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrganizationData As New HU_ORGANIZATION
        Try
            objOrganizationData.ID = Utilities.GetNextSequence(Context, Context.HU_ORGANIZATION.EntitySet.Name)
            objOrganizationData.CODE = objOrganization.CODE
            objOrganizationData.NAME_VN = objOrganization.NAME_VN.Trim
            objOrganizationData.NAME_EN = objOrganization.NAME_EN.Trim
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.EFFECT_DATE = objOrganization.EFFECT_DATE
            objOrganizationData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrganizationData.END_DATE = objOrganization.END_DATE
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.PARENT_ID = objOrganization.PARENT_ID
            objOrganizationData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objOrganizationData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            objOrganizationData.ACTFLG = objOrganization.ACTFLG
            objOrganizationData.REMARK = objOrganization.REMARK
            objOrganizationData.ADDRESS = objOrganization.ADDRESS
            objOrganizationData.FAX = objOrganization.FAX
            objOrganizationData.MOBILE = objOrganization.MOBILE
            objOrganizationData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrganizationData.EMAIL = objOrganization.EMAIL
            objOrganizationData.ORD_NO = objOrganization.ORD_NO
            objOrganizationData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID
            objOrganizationData.REPRESENTATIVE_PHONE = objOrganization.REPRESENTATIVE_PHONE
            objOrganizationData.NUMBER_BUSINESS = objOrganization.NUMBER_BUSINESS
            objOrganizationData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrganizationData.PIT_NO = objOrganization.PIT_NO
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.NUMBER_DECISION = objOrganization.NUMBER_DECISION
            objOrganizationData.TYPE_DECISION = objOrganization.TYPE_DECISION
            objOrganizationData.LOCATION_WORK = objOrganization.LOCATION_WORK
            objOrganizationData.CHK_ORGCHART = objOrganization.CHK_ORGCHART
            objOrganizationData.FILES = objOrganization.FILES

            objOrganizationData.SHORT_NAME = objOrganization.SHORT_NAME
            objOrganizationData.INFOR_1 = objOrganization.INFOR_1
            objOrganizationData.INFOR_2 = objOrganization.INFOR_2
            objOrganizationData.INFOR_3 = objOrganization.INFOR_3
            objOrganizationData.INFOR_4 = objOrganization.INFOR_4
            objOrganizationData.INFOR_5 = objOrganization.INFOR_5
            objOrganizationData.IPAY_GET_DTTD_DTPB = objOrganization.IPAY_GET_DTTD_DTPB
            objOrganizationData.ORG_REG_ID = objOrganization.ORG_REG_ID
            objOrganizationData.WEBSITE = objOrganization.WEBSITE

            objOrganizationData.UY_BAN = objOrganization.UY_BAN
            objOrganizationData.POSITION_ID = objOrganization.POSITION_ID
            objOrganizationData.FILENAME = objOrganization.FILENAME
            objOrganizationData.ATTACH_FILE = objOrganization.ATTACH_FILE

            'EDIT BY: CHIENNV
            'EDIT DATE:11/10/2017
            'ADD FIELD U_INSURANCE IN A Context HU_ORGANIZATION
            objOrganizationData.U_INSURANCE = objOrganization.U_INSURANCE
            objOrganizationData.REGION_ID = objOrganization.REGION_ID
            objOrganizationData.ORG_LEVEL = objOrganization.ORG_LEVEL
            objOrganizationData.UNIT_LEVEL = objOrganization.UNIT_LEVEL
            'objOrganizationData.AUTOGENTIMESHEET = objOrganization.AutoGenTimeSheet
            'END EDIT;
            Context.HU_ORGANIZATION.AddObject(objOrganizationData)

            'tự phân quyền cho user ko phải admin thấy org vừa tạo
            AuthorUserOrganization(log, objOrganizationData.ID)

            Context.SaveChanges(log)
            gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Sub AuthorUserOrganization(ByVal log As UserLog, ByVal org_id As Decimal)
        Try
            Dim user = Context.SE_USER.Where(Function(f) f.USERNAME.ToLower = log.Username.ToLower).FirstOrDefault
            Dim _item As New SE_USER_ORG_ACCESS
            _item.ID = Utilities.GetNextSequence(Context, Context.SE_USER_ORG_ACCESS.EntitySet.Name)
            _item.USER_ID = user.ID
            _item.ORG_ID = org_id
            Context.SE_USER_ORG_ACCESS.AddObject(_item)
            For Each group In user.SE_GROUPS
                If group.IS_ORG_PERMISSION Then
                    Dim lstUser = group.SE_USERS.ToList
                    For Each item_user In lstUser
                        Dim objUser As New SE_USER_ORG_ACCESS
                        objUser.ID = Utilities.GetNextSequence(Context, Context.SE_USER_ORG_ACCESS.EntitySet.Name)
                        objUser.USER_ID = item_user.ID
                        objUser.ORG_ID = org_id
                        objUser.GROUP_ID = group.ID
                        Context.SE_USER_ORG_ACCESS.AddObject(objUser)

                        Dim objGroup As New SE_GROUP_ORG_ACCESS
                        objGroup.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_ORG_ACCESS.EntitySet.Name)
                        objGroup.ORG_ID = org_id
                        objGroup.GROUP_ID = group.ID
                        Context.SE_GROUP_ORG_ACCESS.AddObject(objGroup)
                    Next
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ValidateOrganization(ByVal _validate As OrganizationDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If (_validate.NAME_VN IsNot Nothing And _validate.NAME_EN IsNot Nothing) Then
                    query = (From p In Context.HU_ORGANIZATION
                             Where (p.NAME_VN.ToUpper = _validate.NAME_VN.ToUpper _
                             Or p.NAME_EN.ToUpper = _validate.NAME_VN.ToUpper) _
                         And p.ACTFLG = _validate.ACTFLG).FirstOrDefault
                End If
                Return (query IsNot Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateCostCenterCode(ByVal _validate As OrganizationDTO)
        Dim query
        Try
            If _validate.COST_CENTER_CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.COST_CENTER_CODE.ToUpper = _validate.COST_CENTER_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_ORGANIZATION
                             Where p.COST_CENTER_CODE.ToUpper = _validate.COST_CENTER_CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim i As Integer = (From p In Context.HU_EMPLOYEE
                                Where lstID.Contains(p.ORG_ID) And
                                (Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                        ((p.WORK_STATUS <> terID) Or
                                            (p.WORK_STATUS = terID And
                                                p.TER_EFFECT_DATE > dateNow))))).Count
            If i > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyOrganization(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrganizationData As New HU_ORGANIZATION With {.ID = objOrganization.ID}

        Try
            objOrganizationData = (From p In Context.HU_ORGANIZATION Where p.ID = objOrganization.ID).FirstOrDefault
            objOrganizationData.ID = objOrganization.ID
            If objOrganization.PARENT_ID IsNot Nothing Then
                objOrganizationData.PARENT_ID = objOrganization.PARENT_ID
            End If
            objOrganizationData.CODE = objOrganization.CODE.Trim
            objOrganizationData.NAME_VN = objOrganization.NAME_VN.Trim
            objOrganizationData.NAME_EN = objOrganization.NAME_EN.Trim
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.EFFECT_DATE = objOrganization.EFFECT_DATE
            objOrganizationData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrganizationData.END_DATE = objOrganization.END_DATE
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objOrganizationData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            objOrganizationData.REMARK = objOrganization.REMARK
            objOrganizationData.ADDRESS = objOrganization.ADDRESS
            objOrganizationData.FAX = objOrganization.FAX
            objOrganizationData.MOBILE = objOrganization.MOBILE
            objOrganizationData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrganizationData.EMAIL = objOrganization.EMAIL
            objOrganizationData.NUMBER_BUSINESS = objOrganization.NUMBER_BUSINESS
            objOrganizationData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrganizationData.PIT_NO = objOrganization.PIT_NO
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.NUMBER_DECISION = objOrganization.NUMBER_DECISION
            objOrganizationData.TYPE_DECISION = objOrganization.TYPE_DECISION
            objOrganizationData.LOCATION_WORK = objOrganization.LOCATION_WORK
            objOrganizationData.CHK_ORGCHART = objOrganization.CHK_ORGCHART
            objOrganizationData.FILES = objOrganization.FILES
            objOrganizationData.ORD_NO = objOrganization.ORD_NO
            objOrganizationData.IPAY_GET_DTTD_DTPB = objOrganization.IPAY_GET_DTTD_DTPB


            objOrganizationData.SHORT_NAME = objOrganization.SHORT_NAME
            objOrganizationData.INFOR_1 = objOrganization.INFOR_1
            objOrganizationData.INFOR_2 = objOrganization.INFOR_2
            objOrganizationData.INFOR_3 = objOrganization.INFOR_3
            objOrganizationData.INFOR_4 = objOrganization.INFOR_4
            objOrganizationData.INFOR_5 = objOrganization.INFOR_5
            'EDIT BY: CHIENNV
            'EDIT DATE:11/10/2017
            'ADD FIELD U_INSURANCE IN A Context HU_ORGANIZATION
            objOrganizationData.U_INSURANCE = objOrganization.U_INSURANCE
            objOrganizationData.REGION_ID = objOrganization.REGION_ID
            objOrganizationData.ORG_LEVEL = objOrganization.ORG_LEVEL
            objOrganizationData.UNIT_LEVEL = objOrganization.UNIT_LEVEL
            'END EDIT;
            objOrganizationData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID
            objOrganizationData.REPRESENTATIVE_PHONE = objOrganization.REPRESENTATIVE_PHONE
            objOrganizationData.ORG_REG_ID = objOrganization.ORG_REG_ID
            objOrganizationData.WEBSITE = objOrganization.WEBSITE
            objOrganizationData.UY_BAN = objOrganization.UY_BAN
            objOrganizationData.POSITION_ID = objOrganization.POSITION_ID
            objOrganizationData.FILENAME = objOrganization.FILENAME
            objOrganizationData.ATTACH_FILE = objOrganization.ATTACH_FILE
            Context.SaveChanges(log)
            gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean
        Try

            For Each item As OrganizationPathDTO In lstPath
                Dim objOrganizationData As New HU_ORGANIZATION With {.ID = item.ID}
                Context.HU_ORGANIZATION.Attach(objOrganizationData)
                objOrganizationData.DESCRIPTION_PATH = item.DESCRIPTION_PATH
                objOrganizationData.HIERARCHICAL_PATH = item.HIERARCHICAL_PATH
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrganization(ByVal lstOrganization() As OrganizationDTO, ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstOrganizationData As List(Of HU_ORGANIZATION)
        Dim lstIDOrganization As List(Of Decimal) = (From p In lstOrganization.ToList Select p.ID).ToList
        lstOrganizationData = (From p In Context.HU_ORGANIZATION Where lstIDOrganization.Contains(p.ID)).ToList
        For index = 0 To lstOrganizationData.Count - 1
            lstOrganizationData(index).ACTFLG = sActive
            If sActive = "A" Then
                lstOrganizationData(index).DISSOLVE_DATE = Nothing
            End If
            lstOrganizationData(index).MODIFIED_DATE = DateTime.Now
            lstOrganizationData(index).MODIFIED_BY = log.Username
            lstOrganizationData(index).MODIFIED_LOG = log.ComputerName
            If sActive = "I" Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_PROFILE.UPDATE_ORG_CHILDREN",
                                     New With {.P_ORGID = lstOrganizationData(index).ID,
                                               .P_USER = log.Username})
                End Using
            End If
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function UpdateUy_Ban_Organization(ByVal lstOrganization() As OrganizationDTO, ByVal sValue As Decimal, ByVal log As UserLog) As Boolean
        Dim lstOrganizationData As List(Of HU_ORGANIZATION)
        Dim lstIDOrganization As List(Of Decimal) = (From p In lstOrganization.ToList Select p.ID).ToList
        lstOrganizationData = (From p In Context.HU_ORGANIZATION Where lstIDOrganization.Contains(p.ID)).ToList
        For index = 0 To lstOrganizationData.Count - 1
            lstOrganizationData(index).UY_BAN = sValue
            lstOrganizationData(index).MODIFIED_DATE = DateTime.Now
            lstOrganizationData(index).MODIFIED_BY = log.Username
            lstOrganizationData(index).MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
        Next
        Context.SaveChanges(log)
        Return True
    End Function
#End Region

#Region "OrgTitle"

    Public Function GetOrgTitle(ByVal filter As OrgTitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO)
        Try
            Dim query = From p In Context.HU_ORG_TITLE
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID And f.ID = filter.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From t2 In Context.HU_TITLE.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID).DefaultIfEmpty
                        Order By t.CODE


            Dim org = query.Select(Function(p) New OrgTitleDTO With {.ID = p.p.ID,
                                                                     .ORG_ID = p.p.ORG_ID,
                                                                     .TITLE_ID = p.p.TITLE_ID,
                                                                     .CODE = p.t.CODE,
                                                                     .NAME_EN = p.t.NAME_EN,
                                                                     .NAME_VN = p.t.NAME_VN,
                                                                     .TITLE_GROUP_ID = p.group.ID,
                                                                     .TITLE_GROUP_NAME = p.group.NAME_VN,
                                                                     .REMARK = p.t.REMARK,
                                                                     .PARENT_NAME = p.t2.NAME_VN,
                                                                     .PARENT_ID = p.p.PARENT_ID,
                                                                     .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            If filter.TITLE_ID <> 0 Then
                org = org.Where(Function(p) p.TITLE_ID = filter.TITLE_ID)
            End If
            If filter.NAME_EN <> "" Then
                org = org.Where(Function(p) p.NAME_EN.ToUpper.Contains(filter.NAME_EN.ToUpper))
            End If
            If filter.NAME_VN <> "" Then
                org = org.Where(Function(p) p.NAME_VN.ToUpper.Contains(filter.NAME_VN.ToUpper))
            End If
            If filter.PARENT_NAME <> "" Then
                org = org.Where(Function(p) p.PARENT_NAME.ToUpper.Contains(filter.PARENT_NAME.ToUpper))
            End If
            If filter.ACTFLG <> "" Then
                org = org.Where(Function(p) p.ACTFLG.ToUpper.Contains(filter.ACTFLG.ToUpper))
            End If

            Total = org.Count
            org = org.Skip(PageIndex * PageSize).Take(PageSize)
            Return org.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertOrgTitle(ByVal lstOrgTitle As List(Of OrgTitleDTO),
                                   ByVal log As UserLog, ByRef gID As Decimal,
                                   Optional ByVal isSave As Boolean = True) As Boolean
        Dim litOrgID As List(Of Decimal) = (From p In lstOrgTitle Select p.ORG_ID).Distinct().ToList
        Try
            Dim lstOrgTitleExist = (From p In Context.HU_ORG_TITLE
                                    Where litOrgID.Contains(p.ORG_ID)
                                    Select p.ORG_ID, p.TITLE_ID).ToList

            lstOrgTitle = lstOrgTitle.Where(Function(w) Not lstOrgTitleExist.Any(Function(f) f.TITLE_ID = w.TITLE_ID)).ToList

            For Each obj As OrgTitleDTO In lstOrgTitle
                Dim objData As New HU_ORG_TITLE
                objData.ID = Utilities.GetNextSequence(Context, Context.HU_ORG_TITLE.EntitySet.Name)
                objData.ORG_ID = obj.ORG_ID
                objData.TITLE_ID = obj.TITLE_ID
                objData.ACTFLG = obj.ACTFLG
                objData.PARENT_ID = obj.PARENT_ID
                Context.HU_ORG_TITLE.AddObject(objData)
            Next
            If isSave AndAlso lstOrgTitle.Any Then
                Context.SaveChanges(log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean
        Try
            Dim lstTitleID = (From p In Context.HU_ORG_TITLE Where lstID.Contains(p.ID) Select p.TITLE_ID).ToList
            Dim i As Integer = (From p In Context.HU_EMPLOYEE
                                Where lstTitleID.Contains(p.TITLE_ID) And p.ORG_ID = orgID).Count
            If i > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrgTitle(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As String,
                               ByVal log As UserLog) As Boolean
        Dim lstOrgTitleData As List(Of HU_ORG_TITLE)
        Try
            lstOrgTitleData = (From p In Context.HU_ORG_TITLE Where lstOrgTitle.Contains(p.ID)).ToList
            For index = 0 To lstOrgTitleData.Count - 1
                lstOrgTitleData(index).ACTFLG = sActive
                lstOrgTitleData(index).MODIFIED_DATE = DateTime.Now
                lstOrgTitleData(index).MODIFIED_BY = log.Username
                lstOrgTitleData(index).MODIFIED_LOG = log.ComputerName
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteOrgTitle(ByVal lstOrgTitle As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Dim lstOrgTitleData As List(Of HU_ORG_TITLE)
        Try
            lstOrgTitleData = (From p In Context.HU_ORG_TITLE Where lstOrgTitle.Contains(p.ID)).ToList

            For idx = 0 To lstOrgTitleData.Count - 1
                Context.HU_ORG_TITLE.DeleteObject(lstOrgTitleData(idx))
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

    ''' <summary>
    ''' Kiểm tra dữ liệu đã được sử dụng hay chưa?
    ''' </summary>
    ''' <param name="table">Enum Table_Name</param>
    ''' <returns>true:chưa có/false:có rồi</returns>
    ''' <remarks></remarks>
    ''' 

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try

            Select Case table
                Case ProfileCommon.TABLE_NAME.HU_CONTRACT_TYPE
                    isExist = Execute_ExistInDatabase("HU_CONTRACT", "CONTRACT_TYPE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_TITLE
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "TITLE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_ORG_TITLE", "TITLE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "TITLE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    'isExist = Execute_ExistInDatabase("HU_TITLE_CONCURRENT", "TITLE_ID", strListID)
                    'If Not isExist Then
                    '    Return isExist
                    'End If
                Case ProfileCommon.TABLE_NAME.HU_WORK_PLACE
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "WORK_PLACE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "WORK_PLACE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_ALLOWANCE_LIST
                    isExist = Execute_ExistInDatabase("HU_WORKING_ALLOW", "ALLOWANCE_LIST_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_NATION
                    isExist = Execute_ExistInDatabase("HU_PROVINCE", "NATION_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NATIONALITY", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_PROVINCE
                    isExist = Execute_ExistInDatabase("HU_DISTRICT", "PROVINCE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "PER_PROVINCE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NAV_PROVINCE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("INS_WHEREHEALTH", "ID_PROVINCE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_BANK
                    isExist = Execute_ExistInDatabase("HU_BANK_BRANCH", "BANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "BANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_BANK_BRANCH
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "BANK_BRANCH_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_ASSET
                    isExist = Execute_ExistInDatabase("HU_ASSET_MNG", "ASSET_DECLARE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_DISTRICT
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "PER_DISTRICT", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NAV_DISTRICT", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_WARD
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "PER_WARD", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NAV_WARD", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_STAFF_RANK
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "STAFF_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "STAFF_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("AT_SETUP_SPECIAL", "STAFF_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case ProfileCommon.TABLE_NAME.HU_COMMENDLEVER
                    isExist = Execute_ExistInDatabase("HU_COMMEND", "COMMEND_LEVEL", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Private Function Execute_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ") and ACTFLG = 'A'"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "Danh muc tham so he thong"
    Public Function ValidateOtherList(ByVal _validate As OtherListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    If _validate.ACTFLG <> "" Then
                        query = (From p In Context.OT_OTHER_LIST
                                 Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                                 Where q.CODE.ToUpper = _validate.CODE.ToUpper _
                                 And p.ID = _validate.ID _
                                 And p.ACTFLG = _validate.ACTFLG
                             ).FirstOrDefault
                        Return (Not query Is Nothing)
                    Else
                        query = (From p In Context.OT_OTHER_LIST
                                 Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                                 And p.ID <> _validate.ID
                             ).SingleOrDefault
                        Return (query Is Nothing)
                    End If
                Else
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper
                            ).FirstOrDefault
                    Return (query Is Nothing)
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    'And p.TYPE_ID = _validate.TYPE_ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    ' And p.TYPE_ID = _validate.TYPE_ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
                If (_validate.NAME_VN <> "") Then
                    If (_validate.ID <> 0) Then
                        If (_validate.TYPE_CODE <> "") Then
                            query = (From p In Context.OT_OTHER_LIST
                                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = p.TYPE_ID)
                                     Where p.ACTFLG.ToUpper = "A" _
                                          And p.ID = _validate.ID _
                                          And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim _
                                          And t.CODE = _validate.TYPE_CODE).FirstOrDefault
                            Return (Not query Is Nothing)
                        Else
                            query = (From p In Context.OT_OTHER_LIST
                                     Where p.ACTFLG.ToUpper = "A" And p.ID = _validate.ID And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim).FirstOrDefault
                            Return (Not query Is Nothing)
                        End If

                    Else
                        If (_validate.TYPE_CODE <> "") Then
                            query = (From p In Context.OT_OTHER_LIST
                                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = p.TYPE_ID)
                                     Where p.ACTFLG.ToUpper = "A" _
                                           And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim _
                                           And t.CODE = _validate.TYPE_CODE).FirstOrDefault
                            Return (Not query Is Nothing)
                        Else
                            query = (From p In Context.OT_OTHER_LIST
                                     Where p.ACTFLG.ToUpper = "A" And p.NAME_VN.ToUpper.Trim = _validate.NAME_VN.ToUpper.Trim).FirstOrDefault
                            Return (Not query Is Nothing)
                        End If

                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
#Region "Nation -Danh muc quoc gia"
    Public Function GetNation(ByVal _filter As NationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO)
        Try
            Dim query = From p In Context.HU_NATION
            Dim lst = query.Select(Function(p) New NationDTO With {.ID = p.ID,
                                                            .NAME_EN = p.NAME_EN,
                                                            .NAME_VN = p.NAME_VN,
                                                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                            .CODE = p.CODE,
                                                            .CREATED_DATE = p.CREATED_DATE
                                                             })
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            '''''Logger.LogError(ex)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, "Profile.GetNation")
            Throw ex
        End Try
    End Function

    Public Function InsertNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objNationData As New HU_NATION
            objNationData.ID = Utilities.GetNextSequence(Context, Context.HU_NATION.EntitySet.Name)
            'objNationData.NAME_EN = objNation.NAME_EN
            objNationData.NAME_VN = objNation.NAME_VN
            objNationData.ACTFLG = objNation.ACTFLG
            objNationData.CODE = objNation.CODE
            Context.HU_NATION.AddObject(objNationData)
            Context.SaveChanges(log)
            gID = objNationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objNationData As New HU_NATION With {.ID = objNation.ID}

            objNationData = (From p In Context.HU_NATION Where p.ID = objNation.ID).FirstOrDefault
            objNationData.ID = objNation.ID
            objNationData.CODE = objNation.CODE
            'objNationData.NAME_EN = objNation.NAME_EN
            objNationData.NAME_VN = objNation.NAME_VN
            Context.SaveChanges(log)
            gID = objNation.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateNation(ByVal _validate As NationDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_NATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_NATION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_NATION
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_NATION
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveNation(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstNationData As List(Of HU_NATION)
            lstNationData = (From p In Context.HU_NATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstNationData.Count - 1
                lstNationData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteNation(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_NATION)
            lstData = (From p In Context.HU_NATION Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_NATION.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "Province -Danh muc tinh thanh"
    Public Function GetProvinceByNationID(ByVal sNationID As Decimal, ByVal sACTFLG As String) As List(Of ProvinceDTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.ProvinceDTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_PROVINCE Where p.NATION_ID = sNationID And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN
                                                      })
            Else
                query = (From p In Context.HU_PROVINCE Where p.NATION_ID = sNationID Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN
                                                      })
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvinceByNationCode(ByVal sNationCode As String, ByVal sACTFLG As String) As List(Of ProvinceDTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.ProvinceDTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_PROVINCE Where p.HU_NATION.CODE = sNationCode And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN,
                                                      .NAME_EN = p.NAME_EN
                                                      })
            Else
                query = (From p In Context.HU_PROVINCE Where p.HU_NATION.CODE = sNationCode Order By p.NAME_VN.ToUpper
                         Select New ProvinceDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN,
                                                       .NAME_EN = p.NAME_EN
                                                      })
            End If
            ''''Logger.LogError(ex)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvince(ByVal _filter As ProvinceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO)
        Try
            Dim query = From p In Context.HU_PROVINCE
            Dim lst = query.Select(Function(p) New ProvinceDTO With {.ID = p.ID,
                                                   .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                   .CODE = p.CODE,
                                                   .NATION_ID = p.NATION_ID,
                                                   .NATION_NAME = p.HU_NATION.NAME_VN,
                                                   .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                lst = lst.Where(Function(p) p.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objProvinceData As New HU_PROVINCE

            objProvinceData.ID = Utilities.GetNextSequence(Context, Context.HU_PROVINCE.EntitySet.Name)
            'objProvinceData.NAME_EN = objProvince.NAME_EN
            objProvinceData.NAME_VN = objProvince.NAME_VN
            objProvinceData.ACTFLG = objProvince.ACTFLG
            objProvinceData.CODE = objProvince.CODE
            objProvinceData.NATION_ID = objProvince.NATION_ID
            Context.HU_PROVINCE.AddObject(objProvinceData)
            Context.SaveChanges(log)
            gID = objProvinceData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateProvince(ByVal _validate As ProvinceDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_PROVINCE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_PROVINCE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_PROVINCE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_PROVINCE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objProvinceData As New HU_PROVINCE With {.ID = objProvince.ID}

            objProvinceData = (From p In Context.HU_PROVINCE Where p.ID = objProvince.ID).FirstOrDefault
            objProvinceData.ID = objProvince.ID
            objProvinceData.CODE = objProvince.CODE
            objProvinceData.NATION_ID = objProvince.NATION_ID
            'objProvinceData.NAME_EN = objProvince.NAME_EN
            objProvinceData.NAME_VN = objProvince.NAME_VN
            Context.SaveChanges(log)
            gID = objProvinceData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveProvince(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstProvinceData As List(Of HU_PROVINCE)
            lstProvinceData = (From p In Context.HU_PROVINCE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstProvinceData.Count - 1
                lstProvinceData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteProvince(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_PROVINCE)
            lstData = (From p In Context.HU_PROVINCE Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_PROVINCE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "District -Danh muc quan huyen"
    Public Function GetDistrictByProvinceID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of DistrictDTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.DistrictDTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_DISTRICT Where p.PROVINCE_ID = sProvinceID And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New DistrictDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN})
            Else
                query = (From p In Context.HU_DISTRICT Where p.PROVINCE_ID = sProvinceID Order By p.NAME_VN.ToUpper
                         Select New DistrictDTO With {.ID = p.ID,
                                                      .NAME_VN = p.NAME_VN})
            End If
            ''''Logger.LogError(ex)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetDistrict(ByVal _filter As DistrictDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO)
        Try
            Dim query = From p In Context.HU_DISTRICT
            Dim lst = query.Select(Function(p) New DistrictDTO With {.ID = p.ID,
                                                            .NAME_EN = p.NAME_EN,
                                                            .NAME_VN = p.NAME_VN,
                                                            .CODE = p.CODE,
                                                            .NATION_NAME = p.HU_PROVINCE.HU_NATION.NAME_VN,
                                                            .NATION_ID = p.HU_PROVINCE.NATION_ID,
                                                            .PROVINCE_NAME = p.HU_PROVINCE.NAME_VN,
                                                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                            .PROVINCE_ID = p.PROVINCE_ID,
                                                            .CREATED_DATE = p.CREATED_DATE})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                lst = lst.Where(Function(p) p.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDistrict(ByVal _validate As DistrictDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_DISTRICT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_DISTRICT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_DISTRICT
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_DISTRICT
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objDistrictData As New HU_DISTRICT
            objDistrictData.ID = Utilities.GetNextSequence(Context, Context.HU_DISTRICT.EntitySet.Name)
            objDistrictData.CODE = objDistrict.CODE
            objDistrictData.NAME_VN = objDistrict.NAME_VN
            objDistrictData.ACTFLG = objDistrict.ACTFLG
            objDistrictData.PROVINCE_ID = objDistrict.PROVINCE_ID
            Context.HU_DISTRICT.AddObject(objDistrictData)
            Context.SaveChanges(log)
            gID = objDistrictData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objDistrictData As New HU_DISTRICT With {.ID = objDistrict.ID}
            objDistrictData = (From p In Context.HU_DISTRICT Where p.ID = objDistrict.ID).FirstOrDefault
            objDistrictData.ID = objDistrict.ID
            objDistrictData.CODE = objDistrict.CODE
            objDistrictData.PROVINCE_ID = objDistrict.PROVINCE_ID
            objDistrictData.NAME_VN = objDistrict.NAME_VN
            Context.SaveChanges(log)
            gID = objDistrictData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveDistrict(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstDistrictData As List(Of HU_DISTRICT)
            lstDistrictData = (From p In Context.HU_DISTRICT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstDistrictData.Count - 1
                lstDistrictData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteDistrict(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_DISTRICT)
            lstData = (From p In Context.HU_DISTRICT Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_DISTRICT.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "HU_WARD danh mục xã phường"
    Public Function GetWardByDistrictID(ByVal sDistrictID As Decimal, ByVal sACTFLG As String) As List(Of Ward_DTO)
        Try
            Dim query As System.Linq.IQueryable(Of ProfileDAL.Ward_DTO)
            If sACTFLG <> "" Then
                query = (From p In Context.HU_WARD Where p.DISTRICT_ID = sDistrictID And p.ACTFLG = sACTFLG Order By p.NAME_VN.ToUpper
                         Select New Ward_DTO With {.ID = p.ID,
                                                   .NAME_VN = p.NAME_VN})
            Else
                query = (From p In Context.HU_WARD Where p.DISTRICT_ID = sDistrictID Order By p.NAME_VN.ToUpper
                         Select New Ward_DTO With {.ID = p.ID,
                                                   .NAME_VN = p.NAME_VN})
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDistrictByProvinceID")
            Throw ex
        End Try


    End Function

    Public Function GetWard(ByVal _filter As Ward_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO)
        Try
            Dim query = From p In Context.HU_WARD
                        From d In Context.HU_DISTRICT.Where(Function(F) F.ID = p.DISTRICT_ID)
                        From pr In Context.HU_PROVINCE.Where(Function(F) F.ID = d.PROVINCE_ID)
                        From n In Context.HU_NATION.Where(Function(F) F.ID = pr.NATION_ID)
            Dim lst = query.Select(Function(p) New Ward_DTO With {.ID = p.p.ID,
                                                            .CODE = p.p.CODE,
                                                            .NAME_EN = p.p.NAME_EN,
                                                            .NAME_VN = p.p.NAME_VN,
                                                            .DISTRICT_ID = p.p.DISTRICT_ID,
                                                            .DISTRICT_NAME = p.d.NAME_VN,
                                                            .PROVINCE_ID = p.d.PROVINCE_ID,
                                                            .PROVINCE_NAME = p.pr.NAME_VN,
                                                            .NATION_ID = p.pr.NATION_ID,
                                                            .NATION_NAME = p.n.NAME_VN,
                                                            .NOTE = p.p.NOTE,
                                                            .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                            .CREATED_DATE = p.p.CREATED_DATE})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.DISTRICT_NAME <> "" Then
                lst = lst.Where(Function(p) p.DISTRICT_NAME.ToUpper.Contains(_filter.DISTRICT_NAME.ToUpper))
            End If
            If _filter.PROVINCE_NAME <> "" Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                lst = lst.Where(Function(p) p.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetWard")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateWard(ByVal _validate As Ward_DTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_WARD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_WARD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_WARD
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_WARD
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateWard")
            Throw ex
        End Try
    End Function

    Public Function InsertWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objWardData As New HU_WARD
            objWardData.ID = Utilities.GetNextSequence(Context, Context.HU_WARD.EntitySet.Name)
            objWardData.CODE = objWard.CODE
            objWardData.NAME_VN = objWard.NAME_VN
            objWardData.DISTRICT_ID = objWard.DISTRICT_ID
            objWardData.ACTFLG = objWard.ACTFLG
            objWardData.NOTE = objWard.NOTE
            Context.HU_WARD.AddObject(objWardData)
            Context.SaveChanges(log)
            gID = objWardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertWard")
            Throw ex
        End Try

    End Function

    Public Function ModifyWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objWardData As New HU_WARD With {.ID = objWard.ID}
            objWardData = (From p In Context.HU_WARD Where p.ID = objWard.ID).FirstOrDefault
            objWardData.ID = objWard.ID
            objWardData.CODE = objWard.CODE
            objWardData.DISTRICT_ID = objWard.DISTRICT_ID
            objWardData.NAME_VN = objWard.NAME_VN
            objWardData.NOTE = objWard.NOTE
            Context.SaveChanges(log)
            gID = objWardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyWard")
            Throw ex
        End Try

    End Function

    Public Function ActiveWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of HU_WARD)
        Try
            lstData = (From p In Context.HU_WARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ActiveWard")
            Throw ex
        End Try
    End Function

    Public Function DeleteWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of HU_WARD)
        Try
            lstData = (From p In Context.HU_WARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_WARD.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteWard")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bank -Danh muc Ngan hang"


    Public Function GetBank(ByVal _filter As BankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO)
        Try
            Dim query = From p In Context.HU_BANK
            Dim lst = query.Select(Function(p) New BankDTO With {.ID = p.ID,
                                                             .NAME = p.NAME,
                                                             .SHORT_NAME = p.SHORT_NAME,
                                                             .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                             .CODE = p.CODE,
                                                             .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.SHORT_NAME <> "" Then
                lst = lst.Where(Function(p) p.SHORT_NAME.ToUpper.Contains(_filter.SHORT_NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objBankData As New HU_BANK
            objBankData.ID = Utilities.GetNextSequence(Context, Context.HU_BANK.EntitySet.Name)
            objBankData.NAME = objBank.NAME
            objBankData.SHORT_NAME = objBank.SHORT_NAME
            objBankData.ACTFLG = objBank.ACTFLG
            objBankData.CODE = objBank.CODE
            Context.HU_BANK.AddObject(objBankData)
            Context.SaveChanges(log)
            gID = objBankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objBankData As New HU_BANK With {.ID = objBank.ID}
            objBankData = (From p In Context.HU_BANK Where p.ID = objBank.ID).FirstOrDefault
            objBankData.ID = objBank.ID
            objBankData.NAME = objBank.NAME
            objBankData.SHORT_NAME = objBank.SHORT_NAME
            objBankData.CODE = objBank.CODE
            Context.SaveChanges(log)
            gID = objBankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBank(ByVal _validate As BankDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_BANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_BANK
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveBank(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstBankData As List(Of HU_BANK)
            lstBankData = (From p In Context.HU_BANK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstBankData.Count - 1
                lstBankData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteBank(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_BANK)
            lstData = (From p In Context.HU_BANK Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_BANK.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "BankBranch -Danh muc chi nhanh ngan hang"

    Public Function GetBankBranchByBankID(ByVal sBank_ID As Decimal) As List(Of BankBranchDTO)
        Try
            Dim query = (From p In Context.HU_BANK_BRANCH
                         From bank In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID)
                         Where p.BANK_ID = sBank_ID AndAlso p.ACTFLG = "A"
                         Select New BankBranchDTO With {.ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .NAME = p.NAME,
                                                        .BANK_CODE = bank.CODE,
                                                        .BANK_NAME = bank.NAME,
                                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                        .REMARK = p.REMARK})
            ''''Logger.LogError(ex)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function GetBankBranch(ByVal _filter As BankBranchDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO)
        Try
            Dim queryProvince = From p In Context.HU_PROVINCE Select p
            Dim query = From p In Context.HU_BANK_BRANCH
                        From bank In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID).DefaultIfEmpty
                        Select New BankBranchDTO With {.ID = p.ID,
                                                       .CODE = p.CODE,
                                                       .NAME = p.NAME,
                                                       .BANK_CODE = bank.CODE,
                                                       .BANK_NAME = bank.NAME,
                                                       .BANK_ID = p.BANK_ID,
                                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                       .REMARK = p.REMARK,
                                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.BANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.BANK_NAME.ToUpper.Contains(_filter.BANK_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            'Logger.LogError(ex)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objBankBranchData As New HU_BANK_BRANCH
            objBankBranchData.ID = Utilities.GetNextSequence(Context, Context.HU_BANK_BRANCH.EntitySet.Name)
            objBankBranchData.NAME = objBankBranch.NAME
            objBankBranchData.ACTFLG = objBankBranch.ACTFLG
            objBankBranchData.CODE = objBankBranch.CODE
            objBankBranchData.BANK_ID = objBankBranch.BANK_ID
            objBankBranchData.REMARK = objBankBranch.REMARK
            Context.HU_BANK_BRANCH.AddObject(objBankBranchData)
            Context.SaveChanges(log)
            gID = objBankBranchData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objBankBranchData As New HU_BANK_BRANCH With {.ID = objBankBranch.ID}
            objBankBranchData = (From p In Context.HU_BANK_BRANCH Where p.ID = objBankBranch.ID).FirstOrDefault
            objBankBranchData.ID = objBankBranch.ID
            objBankBranchData.NAME = objBankBranch.NAME
            objBankBranchData.CODE = objBankBranch.CODE
            objBankBranchData.BANK_ID = objBankBranch.BANK_ID
            objBankBranchData.REMARK = objBankBranch.REMARK
            Context.SaveChanges(log)
            gID = objBankBranchData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBankBranch(ByVal _validate As BankBranchDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_BANK_BRANCH
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveBankBranch(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstBankBranchData As List(Of HU_BANK_BRANCH)
            lstBankBranchData = (From p In Context.HU_BANK_BRANCH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstBankBranchData.Count - 1
                lstBankBranchData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteBankBranch(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_BANK_BRANCH)
            lstData = (From p In Context.HU_BANK_BRANCH Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_BANK_BRANCH.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Asset -Danh muc tài sản cấp phát"

    Public Function GetAsset(ByVal _filter As AssetDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO)
        Try
            Dim query = From p In Context.HU_ASSET
            Dim lst = query.Select(Function(p) New AssetDTO With {.ID = p.ID,
                                                                  .NAME = p.NAME,
                                                                  .ACTFLG = p.ACTFLG,
                                                                  .ACTFLG2 = p.ACTFLG,
                                                                  .CODE = p.CODE,
                                                                  .GROUP_ID = p.GROUP_ID,
                                                                     .REMARK = p.REMARK,
                                                                  .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_NAME.ToUpper.Contains(_filter.GROUP_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            If _filter.ACTFLG2 <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG2 = _filter.ACTFLG2)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objAssetData As New HU_ASSET
            objAssetData.ID = Utilities.GetNextSequence(Context, Context.HU_ASSET.EntitySet.Name)
            objAssetData.NAME = objAsset.NAME
            objAssetData.ACTFLG = objAsset.ACTFLG
            objAssetData.GROUP_ID = objAsset.GROUP_ID
            objAssetData.REMARK = objAsset.REMARK
            objAssetData.CODE = objAsset.CODE
            Context.HU_ASSET.AddObject(objAssetData)
            Context.SaveChanges(log)
            gID = objAssetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objAssetData As New HU_ASSET With {.ID = objAsset.ID}
            objAssetData = (From p In Context.HU_ASSET Where p.ID = objAsset.ID).FirstOrDefault
            objAssetData.ID = objAsset.ID
            objAssetData.NAME = objAsset.NAME
            objAssetData.CODE = objAsset.CODE
            objAssetData.REMARK = objAsset.REMARK
            objAssetData.GROUP_ID = objAsset.GROUP_ID
            Context.SaveChanges(log)
            gID = objAssetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAsset(ByVal _validate As AssetDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_ASSET
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.HU_ASSET
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_ASSET
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_ASSET
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveAsset(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstAssetData As List(Of HU_ASSET)
            lstAssetData = (From p In Context.HU_ASSET Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAssetData.Count - 1
                lstAssetData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteAsset(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_ASSET)
            lstData = (From p In Context.HU_ASSET Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_ASSET.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

    Public Function GetOrgFromUsername(ByVal username As String) As Decimal?
        Try
            Dim user As SE_USER = Context.SE_USER.FirstOrDefault(Function(p) p.USERNAME.ToUpper = username.ToUpper)

            If user Is Nothing OrElse user.EMPLOYEE_CODE & "" = "" Then
                Return Nothing
            End If

            Dim employee = (From p In Context.HU_EMPLOYEE
                            Where p.EMPLOYEE_CODE.ToUpper = user.EMPLOYEE_CODE.ToUpper
                            Order By p.ID Descending).FirstOrDefault

            If employee Is Nothing Then
                Return Nothing
            End If

            Dim working = Context.HU_WORKING.FirstOrDefault(Function(p) p.EMPLOYEE_ID = employee.ID AndAlso p.EFFECT_DATE <= Date.Now AndAlso p.EXPIRE_DATE >= Date.Now)

            If working Is Nothing Then
                Return Nothing
            End If

            Return working.ORG_ID
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO)
        Try
            Dim rep As New ProfileRepository
            Dim listReturn As New List(Of EmployeeDTO)

            Dim user As SE_USER = Context.SE_USER.FirstOrDefault(Function(p) p.USERNAME.ToUpper = username.ToUpper)

            If user Is Nothing OrElse user.EMPLOYEE_CODE & "" = "" Then
                Return listReturn
            End If

            Dim employee = (From p In Context.HU_EMPLOYEE
                            Join o In Context.HU_ORGANIZATION On p.ORG_ID Equals o.ID
                            Join c In Context.HU_CONTRACT On p.CONTRACT_ID Equals c.ID
                            Join t In Context.HU_TITLE On p.TITLE_ID Equals t.ID
                            Where p.EMPLOYEE_CODE = user.EMPLOYEE_CODE
                            Order By p.ID Descending
                            Select New EmployeeDTO With {
                                .ID = p.ID,
                                .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                .FULLNAME_VN = p.FULLNAME_VN,
                                .ORG_ID = p.ORG_ID,
                                .ORG_NAME = o.NAME_VN,
                                .TITLE_NAME_VN = t.NAME_VN,
                                .JOIN_DATE = p.JOIN_DATE,
                                .CONTRACT_NO = c.CONTRACT_NO,
                                .CONTRACT_EFFECT_DATE = c.START_DATE,
                                .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE}).FirstOrDefault

            If employee Is Nothing Then
                Return listReturn
            End If

            listReturn.Add(employee)

            While Not employee.DIRECT_MANAGER.HasValue
                Dim m_employee = rep.GetEmployeeByID(employee.DIRECT_MANAGER.Value)
                listReturn.Add(m_employee)
            End While

            Return listReturn
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

#Region "StaffRank"

    Public Function GetStaffRank(ByVal _filter As StaffRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO)

        Try
            Dim query = From p In Context.HU_STAFF_RANK
                        Select New StaffRankDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .LEVER = p.LEVEL_STAFF,
                            .LEVEL_STAFF = p.LEVEL_STAFF,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}
            '.LEAVE_COUNT = p.LEAVE_COUNT,
            '.IS_OVT = If(p.IS_OVT = -1, True, False),
            Dim lst = query
            If _filter.LEVER <> "" Then
                lst = lst.Where(Function(p) p.LEVER.ToUpper.Contains(_filter.LEVER.ToUpper))
            End If
            If _filter.LEVEL_STAFF <> 0 Then
                lst = lst.Where(Function(p) p.LEVEL_STAFF = _filter.LEVEL_STAFF)
            End If
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper = _filter.ACTFLG.ToUpper)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetStaffRank")
            Throw ex
        End Try

    End Function

    Public Function InsertStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objStaffRankData As New HU_STAFF_RANK
        Try
            objStaffRankData.ID = Utilities.GetNextSequence(Context, Context.HU_STAFF_RANK.EntitySet.Name)
            objStaffRankData.CODE = objStaffRank.CODE
            objStaffRankData.NAME = objStaffRank.NAME
            objStaffRankData.ACTFLG = objStaffRank.ACTFLG
            objStaffRankData.REMARK = objStaffRank.REMARK
            objStaffRankData.LEVEL_STAFF = objStaffRank.LEVER
            'objStaffRankData.LEAVE_COUNT = objStaffRank.LEAVE_COUNT
            'objStaffRankData.IS_OVT = objStaffRank.IS_OVT
            Context.HU_STAFF_RANK.AddObject(objStaffRankData)
            Context.SaveChanges(log)
            gID = objStaffRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertStaffRank")
            Throw ex
        End Try
    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateStaffRank(ByVal _validate As StaffRankDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_STAFF_RANK
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateStaffRank")
            Throw ex
        End Try
    End Function

    Public Function ModifyStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objStaffRankData As New HU_STAFF_RANK With {.ID = objStaffRank.ID}
        Try
            objStaffRankData = (From p In Context.HU_STAFF_RANK Where p.ID = objStaffRank.ID).FirstOrDefault
            objStaffRankData.CODE = objStaffRank.CODE
            objStaffRankData.NAME = objStaffRank.NAME
            objStaffRankData.REMARK = objStaffRank.REMARK
            objStaffRankData.LEVEL_STAFF = objStaffRank.LEVER
            'objStaffRankData.LEAVE_COUNT = objStaffRank.LEAVE_COUNT
            'objStaffRankData.IS_OVT = objStaffRank.IS_OVT
            Context.SaveChanges(log)
            gID = objStaffRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyStaffRank")
            Throw ex
        End Try

    End Function

    Public Function ActiveStaffRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal sActive As String) As Boolean
        Dim lstData As List(Of HU_STAFF_RANK)
        Try
            lstData = (From p In Context.HU_STAFF_RANK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteStaffRank(ByVal lstStaffRank() As StaffRankDTO, ByVal log As UserLog) As Boolean
        Dim lstStaffRankData As List(Of HU_STAFF_RANK)
        Dim lstIDStaffRank As List(Of Decimal) = (From p In lstStaffRank.ToList Select p.ID).ToList
        Try
            lstStaffRankData = (From p In Context.HU_STAFF_RANK Where lstIDStaffRank.Contains(p.ID)).ToList
            For index = 0 To lstStaffRankData.Count - 1
                Context.HU_STAFF_RANK.DeleteObject(lstStaffRankData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteStaffRank")
            Throw ex
        End Try

    End Function

#End Region


#Region "Danh mục bảo hộ lao động"
    Public Function GetLabourProtection(ByVal _filter As LabourProtectionDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionDTO)
        Try

            Dim query = From p In Context.HU_LABOURPROTECTION

            Dim lst = query.Select(Function(s) New LabourProtectionDTO With {
                                        .ID = s.ID,
                                        .CODE = s.CODE,
                                        .NAME = s.NAME,
                                        .UNIT_PRICE = s.UNIT_PRICE,
                                        .SDESC = s.SDESC,
                                        .ACTFLG = If(s.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = s.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.UNIT_PRICE.HasValue Then
                lst = lst.Where(Function(p) p.UNIT_PRICE = _filter.UNIT_PRICE)
            End If
            If _filter.SDESC <> "" Then
                lst = lst.Where(Function(p) p.SDESC.ToUpper.Contains(_filter.SDESC.ToUpper))
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertLabourProtection(ByVal objTitle As LabourProtectionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_LABOURPROTECTION
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_LABOURPROTECTION.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.UNIT_PRICE = objTitle.UNIT_PRICE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = "A"
            Context.HU_LABOURPROTECTION.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyLabourProtection(ByVal objTitle As LabourProtectionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_LABOURPROTECTION
        Try
            objTitleData = (From p In Context.HU_LABOURPROTECTION Where p.ID = objTitle.ID).SingleOrDefault
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.UNIT_PRICE = objTitle.UNIT_PRICE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateLabourProtection(ByVal _validate As LabourProtectionDTO) As Boolean
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_LABOURPROTECTION
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of HU_LABOURPROTECTION)
        Try
            lstData = (From p In Context.HU_LABOURPROTECTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstLabourProtectionData As List(Of HU_LABOURPROTECTION)
        Try
            lstLabourProtectionData = (From p In Context.HU_LABOURPROTECTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstLabourProtectionData.Count - 1
                Context.HU_LABOURPROTECTION.DeleteObject(lstLabourProtectionData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".LabourProtection")
            Throw ex
        End Try
    End Function

#End Region

#Region "Danh mục thông tin lương "
    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Try
            Dim query = From p In Context.AT_PERIOD Where p.YEAR = year And p.ACTFLG = "A" Order By p.MONTH Ascending, p.START_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .PERIOD_STANDARD = p.PERIOD_STANDARD})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Năng lực"

#Region "CompetencyGroup"

    Public Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_GROUP
                        Select New CompetencyGroupDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyGroupData As New HU_COMPETENCY_GROUP
        Dim iCount As Integer = 0
        Try
            objCompetencyGroupData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_GROUP.EntitySet.Name)
            objCompetencyGroupData.CODE = objCompetencyGroup.CODE
            objCompetencyGroupData.NAME = objCompetencyGroup.NAME
            Context.HU_COMPETENCY_GROUP.AddObject(objCompetencyGroupData)
            Context.SaveChanges(log)
            gID = objCompetencyGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateCompetencyGroup(ByVal _validate As CompetencyGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_COMPETENCY_GROUP
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyGroupData As HU_COMPETENCY_GROUP
        Try
            objCompetencyGroupData = (From p In Context.HU_COMPETENCY_GROUP Where p.ID = objCompetencyGroup.ID).FirstOrDefault
            objCompetencyGroupData.CODE = objCompetencyGroup.CODE
            objCompetencyGroupData.NAME = objCompetencyGroup.NAME
            Context.SaveChanges(log)
            gID = objCompetencyGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstCompetencyGroupData As List(Of HU_COMPETENCY_GROUP)
        Try
            lstCompetencyGroupData = (From p In Context.HU_COMPETENCY_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyGroupData.Count - 1
                lstCompetencyGroupData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyGroupData As List(Of HU_COMPETENCY_GROUP)
        Try

            lstCompetencyGroupData = (From p In Context.HU_COMPETENCY_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyGroupData.Count - 1
                Context.HU_COMPETENCY_GROUP.DeleteObject(lstCompetencyGroupData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Competency"

    Public Function GetCompetency(ByVal _filter As CompetencyDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY
                        From group In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = p.COMPETENCY_GROUP_ID)
                        Select New CompetencyDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .COMPETENCY_GROUP_ID = p.COMPETENCY_GROUP_ID,
                                   .COMPETENCY_GROUP_NAME = group.NAME,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetency(ByVal objCompetency As CompetencyDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyData As New HU_COMPETENCY
        Dim iCount As Integer = 0
        Try
            objCompetencyData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY.EntitySet.Name)
            objCompetencyData.CODE = objCompetency.CODE
            objCompetencyData.NAME = objCompetency.NAME
            objCompetencyData.COMPETENCY_GROUP_ID = objCompetency.COMPETENCY_GROUP_ID
            objCompetencyData.EFFECT_DATE = objCompetency.EFFECT_DATE
            objCompetencyData.EXPIRE_DATE = objCompetency.EXPIRE_DATE
            objCompetencyData.REMARK = objCompetency.REMARK
            Context.HU_COMPETENCY.AddObject(objCompetencyData)
            Context.SaveChanges(log)
            gID = objCompetencyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateCompetency(ByVal _validate As CompetencyDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMPETENCY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMPETENCY
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_COMPETENCY
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyCompetency(ByVal objCompetency As CompetencyDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyData As HU_COMPETENCY
        Try
            objCompetencyData = (From p In Context.HU_COMPETENCY Where p.ID = objCompetency.ID).FirstOrDefault
            objCompetencyData.CODE = objCompetency.CODE
            objCompetencyData.NAME = objCompetency.NAME
            objCompetencyData.COMPETENCY_GROUP_ID = objCompetency.COMPETENCY_GROUP_ID
            objCompetencyData.EFFECT_DATE = objCompetency.EFFECT_DATE
            objCompetencyData.EXPIRE_DATE = objCompetency.EXPIRE_DATE
            objCompetencyData.REMARK = objCompetency.REMARK
            Context.SaveChanges(log)
            gID = objCompetencyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveCompetency(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstCompetencyData As List(Of HU_COMPETENCY)
        Try
            lstCompetencyData = (From p In Context.HU_COMPETENCY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyData.Count - 1
                lstCompetencyData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetency(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyData As List(Of HU_COMPETENCY)
        Try

            lstCompetencyData = (From p In Context.HU_COMPETENCY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyData.Count - 1
                Context.HU_COMPETENCY.DeleteObject(lstCompetencyData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyBuild"

    Public Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_BUILD
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = p.COMPETENCY_GROUP_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = p.COMPETENCY_ID)
                        Select New CompetencyBuildDTO With {
                            .ID = p.ID,
                            .COMPETENCY_GROUP_ID = p.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = p.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER = p.LEVEL_NUMBER,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.LEVEL_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER = _filter.LEVEL_NUMBER)
            End If
            lst = lst.Where(Function(p) p.COMPETENCY_GROUP_ID = _filter.COMPETENCY_GROUP_ID)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyBuildData As New HU_COMPETENCY_BUILD
        Dim iCount As Integer = 0
        Try
            objCompetencyBuildData = (From p In Context.HU_COMPETENCY_BUILD
                                      Where p.COMPETENCY_ID = objCompetencyBuild.COMPETENCY_ID And
                                      p.COMPETENCY_GROUP_ID = objCompetencyBuild.COMPETENCY_GROUP_ID).FirstOrDefault

            If objCompetencyBuildData IsNot Nothing Then
                objCompetencyBuildData.LEVEL_NUMBER = objCompetencyBuild.LEVEL_NUMBER
                objCompetencyBuildData.REMARK = objCompetencyBuild.REMARK
            Else
                objCompetencyBuildData = New HU_COMPETENCY_BUILD
                objCompetencyBuildData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_BUILD.EntitySet.Name)
                objCompetencyBuildData.COMPETENCY_GROUP_ID = objCompetencyBuild.COMPETENCY_GROUP_ID
                objCompetencyBuildData.COMPETENCY_ID = objCompetencyBuild.COMPETENCY_ID
                objCompetencyBuildData.LEVEL_NUMBER = objCompetencyBuild.LEVEL_NUMBER
                objCompetencyBuildData.REMARK = objCompetencyBuild.REMARK
                Context.HU_COMPETENCY_BUILD.AddObject(objCompetencyBuildData)
            End If
            Context.SaveChanges(log)
            gID = objCompetencyBuildData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyBuildData As HU_COMPETENCY_BUILD
        Try
            objCompetencyBuildData = (From p In Context.HU_COMPETENCY_BUILD Where p.ID = objCompetencyBuild.ID).FirstOrDefault
            objCompetencyBuildData.COMPETENCY_GROUP_ID = objCompetencyBuild.COMPETENCY_GROUP_ID
            objCompetencyBuildData.COMPETENCY_ID = objCompetencyBuild.COMPETENCY_ID
            objCompetencyBuildData.LEVEL_NUMBER = objCompetencyBuild.LEVEL_NUMBER
            objCompetencyBuildData.REMARK = objCompetencyBuild.REMARK
            Context.SaveChanges(log)
            gID = objCompetencyBuildData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyBuild(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyBuildData As List(Of HU_COMPETENCY_BUILD)
        Try

            lstCompetencyBuildData = (From p In Context.HU_COMPETENCY_BUILD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyBuildData.Count - 1
                Context.HU_COMPETENCY_BUILD.DeleteObject(lstCompetencyBuildData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyStandard"

    Public Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_STANDARD
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = p.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        Select New CompetencyStandardDTO With {
                            .ID = p.ID,
                            .TITLE_ID = p.TITLE_ID,
                            .TITLE_NAME = title.NAME_VN,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = p.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER = p.LEVEL_NUMBER,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If
            If _filter.LEVEL_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER = _filter.LEVEL_NUMBER)
            End If
            lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstData = lst.ToList
            For Each item In lstData
                item.LEVEL_NUMBER_NAME = item.LEVEL_NUMBER.Value.ToString & "/4"
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyStandardData As New HU_COMPETENCY_STANDARD
        Dim iCount As Integer = 0
        Try
            objCompetencyStandardData = (From p In Context.HU_COMPETENCY_STANDARD
                                         Where p.COMPETENCY_ID = objCompetencyStandard.COMPETENCY_ID And
                                         p.TITLE_ID = objCompetencyStandard.TITLE_ID).FirstOrDefault

            If objCompetencyStandardData IsNot Nothing Then
                objCompetencyStandardData.LEVEL_NUMBER = objCompetencyStandard.LEVEL_NUMBER
            Else
                objCompetencyStandardData = New HU_COMPETENCY_STANDARD
                objCompetencyStandardData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_STANDARD.EntitySet.Name)
                objCompetencyStandardData.TITLE_ID = objCompetencyStandard.TITLE_ID
                objCompetencyStandardData.COMPETENCY_ID = objCompetencyStandard.COMPETENCY_ID
                objCompetencyStandardData.LEVEL_NUMBER = objCompetencyStandard.LEVEL_NUMBER
                Context.HU_COMPETENCY_STANDARD.AddObject(objCompetencyStandardData)
            End If
            Context.SaveChanges(log)
            gID = objCompetencyStandardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyStandardData As HU_COMPETENCY_STANDARD
        Try
            objCompetencyStandardData = (From p In Context.HU_COMPETENCY_STANDARD Where p.ID = objCompetencyStandard.ID).FirstOrDefault
            objCompetencyStandardData.TITLE_ID = objCompetencyStandard.TITLE_ID
            objCompetencyStandardData.COMPETENCY_ID = objCompetencyStandard.COMPETENCY_ID
            objCompetencyStandardData.LEVEL_NUMBER = objCompetencyStandard.LEVEL_NUMBER
            Context.SaveChanges(log)
            gID = objCompetencyStandardData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyStandardData As List(Of HU_COMPETENCY_STANDARD)
        Try

            lstCompetencyStandardData = (From p In Context.HU_COMPETENCY_STANDARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyStandardData.Count - 1
                Context.HU_COMPETENCY_STANDARD.DeleteObject(lstCompetencyStandardData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyAppendix"

    Public Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_APPENDIX
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Select New CompetencyAppendixDTO With {
                                   .ID = p.ID,
                                   .TITLE_ID = p.TITLE_ID,
                                   .TITLE_NAME = title.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)

            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyAppendixData As New HU_COMPETENCY_APPENDIX
        Dim iCount As Integer = 0
        Try
            objCompetencyAppendixData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_APPENDIX.EntitySet.Name)
            objCompetencyAppendixData.TITLE_ID = objCompetencyAppendix.TITLE_ID
            objCompetencyAppendixData.REMARK = objCompetencyAppendix.REMARK
            Context.HU_COMPETENCY_APPENDIX.AddObject(objCompetencyAppendixData)
            Context.SaveChanges(log)
            gID = objCompetencyAppendixData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyAppendixData As HU_COMPETENCY_APPENDIX
        Try
            objCompetencyAppendixData = (From p In Context.HU_COMPETENCY_APPENDIX Where p.ID = objCompetencyAppendix.ID).FirstOrDefault
            objCompetencyAppendixData.TITLE_ID = objCompetencyAppendix.TITLE_ID
            objCompetencyAppendixData.REMARK = objCompetencyAppendix.REMARK
            Context.SaveChanges(log)
            gID = objCompetencyAppendixData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyAppendix(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyAppendixData As List(Of HU_COMPETENCY_APPENDIX)
        Try

            lstCompetencyAppendixData = (From p In Context.HU_COMPETENCY_APPENDIX Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyAppendixData.Count - 1
                Context.HU_COMPETENCY_APPENDIX.DeleteObject(lstCompetencyAppendixData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyEmp"

    Public Function GetCompetencyEmp(ByVal _filter As CompetencyEmpDTO) As List(Of CompetencyEmpDTO)

        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = stand.TITLE_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From p In Context.HU_COMPETENCY_EMP.Where(Function(f) f.TITLE_ID = stand.TITLE_ID And
                                                                      f.EMPLOYEE_ID = _filter.EMPLOYEE_ID And
                                                                      f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID).DefaultIfEmpty
                        Where stand.TITLE_ID = _filter.TITLE_ID
                        Select New CompetencyEmpDTO With {
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = stand.TITLE_ID,
                            .TITLE_NAME = title.NAME_VN,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = stand.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                            .LEVEL_NUMBER_EMP = p.LEVEL_NUMBER,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If

            Dim lstData = lst.ToList
            For Each item In lstData
                If item.LEVEL_NUMBER_EMP IsNot Nothing Then
                    item.LEVEL_NUMBER_EMP_NAME = item.LEVEL_NUMBER_EMP.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_STANDARD IsNot Nothing Then
                    item.LEVEL_NUMBER_STANDARD_NAME = item.LEVEL_NUMBER_STANDARD.Value.ToString & "/4"
                End If
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateCompetencyEmp(ByVal lstCom As List(Of CompetencyEmpDTO), ByVal log As UserLog) As Boolean
        Dim objCompetencyEmpData As New HU_COMPETENCY_EMP
        Dim iCount As Integer = 0
        Try
            For Each obj In lstCom
                objCompetencyEmpData = (From p In Context.HU_COMPETENCY_EMP
                                        Where p.COMPETENCY_ID = obj.COMPETENCY_ID And
                                        p.EMPLOYEE_ID = obj.EMPLOYEE_ID).FirstOrDefault

                If objCompetencyEmpData IsNot Nothing Then
                    objCompetencyEmpData.LEVEL_NUMBER = obj.LEVEL_NUMBER_EMP
                Else
                    objCompetencyEmpData = New HU_COMPETENCY_EMP
                    objCompetencyEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_STANDARD.EntitySet.Name)
                    objCompetencyEmpData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    objCompetencyEmpData.TITLE_ID = obj.TITLE_ID
                    objCompetencyEmpData.COMPETENCY_ID = obj.COMPETENCY_ID
                    objCompetencyEmpData.LEVEL_NUMBER = obj.LEVEL_NUMBER_EMP
                    Context.HU_COMPETENCY_EMP.AddObject(objCompetencyEmpData)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyPeriod"

    Public Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_PERIOD
                        Select New CompetencyPeriodDTO With {
                                   .ID = p.ID,
                                   .YEAR = p.YEAR,
                                   .NAME = p.NAME,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyPeriodData As New HU_COMPETENCY_PERIOD
        Dim iCount As Integer = 0
        Try
            objCompetencyPeriodData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_PERIOD.EntitySet.Name)
            objCompetencyPeriodData.YEAR = objCompetencyPeriod.YEAR
            objCompetencyPeriodData.NAME = objCompetencyPeriod.NAME
            Context.HU_COMPETENCY_PERIOD.AddObject(objCompetencyPeriodData)
            Context.SaveChanges(log)
            gID = objCompetencyPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCompetencyPeriodData As HU_COMPETENCY_PERIOD
        Try
            objCompetencyPeriodData = (From p In Context.HU_COMPETENCY_PERIOD Where p.ID = objCompetencyPeriod.ID).FirstOrDefault
            objCompetencyPeriodData.YEAR = objCompetencyPeriod.YEAR
            objCompetencyPeriodData.NAME = objCompetencyPeriod.NAME
            Context.SaveChanges(log)
            gID = objCompetencyPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyPeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyPeriodData As List(Of HU_COMPETENCY_PERIOD)
        Try

            lstCompetencyPeriodData = (From p In Context.HU_COMPETENCY_PERIOD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyPeriodData.Count - 1
                Context.HU_COMPETENCY_PERIOD.DeleteObject(lstCompetencyPeriodData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CompetencyAssDtl"

    Public Function GetCompetencyAss(ByVal _filter As CompetencyAssDTO) As List(Of CompetencyAssDTO)

        Try
            Dim query = From ass In Context.HU_COMPETENCY_ASS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = ass.TITLE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                        Where ass.COMPETENCY_PERIOD_ID = _filter.COMPETENCY_PERIOD_ID
                        Select New CompetencyAssDTO With {
                            .ID = ass.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = t.ID,
                            .TITLE_NAME = t.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_NAME = o.NAME_VN,
                            .CREATED_DATE = ass.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ID <> 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            Dim lstData = lst.ToList
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCompetencyAssDtl(ByVal _filter As CompetencyAssDtlDTO) As List(Of CompetencyAssDtlDTO)

        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = stand.TITLE_ID)
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID And
                                                                        f.COMPETENCY_PERIOD_ID = _filter.COMPETENCY_PERIOD_ID And
                                                                        stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                        From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                         f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                        Where stand.TITLE_ID = _filter.TITLE_ID
                        Select New CompetencyAssDtlDTO With {
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = stand.TITLE_ID,
                            .TITLE_NAME = title.NAME_VN,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = stand.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                            .LEVEL_NUMBER_EMP = p.LEVEL_NUMBER,
                            .LEVEL_NUMBER_ASS = p.LEVEL_NUMBER,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If

            Dim lstData = lst.ToList
            For Each item In lstData
                If item.LEVEL_NUMBER_EMP IsNot Nothing Then
                    item.LEVEL_NUMBER_EMP_NAME = item.LEVEL_NUMBER_EMP.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_STANDARD IsNot Nothing Then
                    item.LEVEL_NUMBER_STANDARD_NAME = item.LEVEL_NUMBER_STANDARD.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_ASS IsNot Nothing Then
                    item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER_ASS.Value.ToString & "/4"
                End If
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateCompetencyAssDtl(ByVal objAss As CompetencyAssDTO, ByVal lstCom As List(Of CompetencyAssDtlDTO), ByVal log As UserLog) As Boolean
        Dim objCompetencyAssDtlData As HU_COMPETENCY_ASSDTL
        Dim objCompetencyAssData As HU_COMPETENCY_ASS
        Dim iCount As Integer = 0
        Try
            If lstCom.Count > 0 Then
                objCompetencyAssData = (From p In Context.HU_COMPETENCY_ASS
                                        Where p.COMPETENCY_PERIOD_ID = objAss.COMPETENCY_PERIOD_ID And
                                        p.EMPLOYEE_ID = objAss.EMPLOYEE_ID).FirstOrDefault
                If objCompetencyAssData Is Nothing Then
                    objCompetencyAssData = New HU_COMPETENCY_ASS
                    objCompetencyAssData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_ASS.EntitySet.Name)
                    objCompetencyAssData.EMPLOYEE_ID = objAss.EMPLOYEE_ID
                    objCompetencyAssData.COMPETENCY_PERIOD_ID = objAss.COMPETENCY_PERIOD_ID
                    objCompetencyAssData.TITLE_ID = objAss.TITLE_ID
                    Context.HU_COMPETENCY_ASS.AddObject(objCompetencyAssData)
                End If
            End If

            For Each obj In lstCom
                objCompetencyAssDtlData = (From p In Context.HU_COMPETENCY_ASSDTL
                                           Where p.COMPETENCY_ID = obj.COMPETENCY_ID And
                                           p.COMPETENCY_ASS_ID = objCompetencyAssData.ID).FirstOrDefault

                If objCompetencyAssDtlData IsNot Nothing Then
                    objCompetencyAssDtlData.LEVEL_NUMBER = obj.LEVEL_NUMBER_ASS
                    objCompetencyAssDtlData.REMARK = obj.REMARK
                Else
                    objCompetencyAssDtlData = New HU_COMPETENCY_ASSDTL
                    objCompetencyAssDtlData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_ASSDTL.EntitySet.Name)
                    objCompetencyAssDtlData.COMPETENCY_ASS_ID = objCompetencyAssData.ID
                    objCompetencyAssDtlData.COMPETENCY_ID = obj.COMPETENCY_ID
                    objCompetencyAssDtlData.LEVEL_NUMBER = obj.LEVEL_NUMBER_ASS
                    objCompetencyAssDtlData.REMARK = obj.REMARK
                    Context.HU_COMPETENCY_ASSDTL.AddObject(objCompetencyAssDtlData)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyAss(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyAssData As List(Of HU_COMPETENCY_ASS)
        Dim lstCompetencyAssDtlData As List(Of HU_COMPETENCY_ASSDTL)
        Try

            lstCompetencyAssData = (From p In Context.HU_COMPETENCY_ASS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyAssData.Count - 1
                Context.HU_COMPETENCY_ASS.DeleteObject(lstCompetencyAssData(index))
            Next

            lstCompetencyAssDtlData = (From p In Context.HU_COMPETENCY_ASSDTL Where lstID.Contains(p.COMPETENCY_ASS_ID)).ToList
            For index = 0 To lstCompetencyAssDtlData.Count - 1
                Context.HU_COMPETENCY_ASSDTL.DeleteObject(lstCompetencyAssDtlData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#End Region
#Region "DM Khen thưởng"
    Public Function GetCommendList(ByVal _filter As CommendListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO)
        Try
            Dim query = From p In Context.HU_COMMEND_LIST
                        From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                        From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                        Select New CommendListDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .DATATYPE_ID = p.DATATYPE_ID,
                                   .DATATYPE_NAME = da.NAME_VN,
                                   .TYPE_ID = p.TYPE_ID,
                                   .TYPE_NAME = t.NAME_VN,
                                   .NUMBER_ORDER = p.NUMBER_ORDER,
                                   .LEVEL_ID = p.LEVEL_ID,
                                   .LEVEL_NAME = l.NAME,
                                   .OBJECT_ID = p.OBJECT_ID,
                                   .OBJECT_NAME = o.NAME_VN,
                                   .EXCEL = p.EXCEL,
                                   .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

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
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            If _filter.DATATYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.DATATYPE_NAME.ToUpper.Contains(_filter.DATATYPE_NAME.ToUpper))
            End If

            If _filter.TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TYPE_NAME.ToUpper.Contains(_filter.TYPE_NAME.ToUpper))
            End If

            If _filter.LEVEL_NAME <> "" Then
                lst = lst.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.OBJECT_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_NAME.ToUpper.Contains(_filter.OBJECT_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetListCommendList(ByVal actflg As String) As List(Of CommendListDTO)
        Try
            If actflg <> "" Then
                Dim query = From p In Context.HU_COMMEND_LIST
                            From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                            From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                            From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                            Where p.ACTFLG = actflg
                            Select New CommendListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATATYPE_ID = p.DATATYPE_ID,
                                       .DATATYPE_NAME = da.NAME_VN,
                                       .TYPE_ID = p.TYPE_ID,
                                       .TYPE_NAME = t.NAME_VN,
                                       .NUMBER_ORDER = p.NUMBER_ORDER,
                                       .LEVEL_ID = p.LEVEL_ID,
                                       .LEVEL_NAME = l.NAME,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = o.NAME_VN,
                                       .EXCEL = p.EXCEL,
                                       .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

                Return query.ToList
            Else
                Dim query = From p In Context.HU_COMMEND_LIST
                            From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                            From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                            From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                            Select New CommendListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATATYPE_ID = p.DATATYPE_ID,
                                       .DATATYPE_NAME = da.NAME_VN,
                                       .TYPE_ID = p.TYPE_ID,
                                       .TYPE_NAME = t.NAME_VN,
                                       .NUMBER_ORDER = p.NUMBER_ORDER,
                                       .LEVEL_ID = p.LEVEL_ID,
                                       .LEVEL_NAME = l.NAME,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = o.NAME_VN,
                                       .EXCEL = p.EXCEL,
                                       .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

                Return query.ToList
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommendListID(ByVal ID As Decimal) As List(Of CommendListDTO)
        Try
            Dim query = From p In Context.HU_COMMEND_LIST
                        From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                        From l In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.LEVEL_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                        Where p.ID = ID
                        Select New CommendListDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .REMARK = p.REMARK,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .DATATYPE_ID = p.DATATYPE_ID,
                                   .DATATYPE_NAME = da.NAME_VN,
                                   .TYPE_ID = p.TYPE_ID,
                                   .TYPE_NAME = t.NAME_VN,
                                   .NUMBER_ORDER = p.NUMBER_ORDER,
                                   .LEVEL_ID = p.LEVEL_ID,
                                   .LEVEL_NAME = l.NAME,
                                   .OBJECT_ID = p.OBJECT_ID,
                                   .OBJECT_NAME = o.NAME_VN,
                                  .EXCEL = p.EXCEL,
                                   .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCommendList(ByVal objCommendList As CommendListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendListData As New HU_COMMEND_LIST
        Try
            objCommendListData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_LIST.EntitySet.Name)
            objCommendListData.CODE = objCommendList.CODE
            objCommendListData.NAME = objCommendList.NAME
            objCommendListData.DATATYPE_ID = objCommendList.DATATYPE_ID
            objCommendListData.TYPE_ID = objCommendList.TYPE_ID
            objCommendListData.OBJECT_ID = objCommendList.OBJECT_ID
            objCommendListData.NUMBER_ORDER = objCommendList.NUMBER_ORDER
            objCommendListData.LEVEL_ID = objCommendList.LEVEL_ID
            objCommendListData.REMARK = objCommendList.REMARK
            objCommendListData.ACTFLG = objCommendList.ACTFLG
            objCommendListData.EXCEL = objCommendList.EXCEL
            Context.HU_COMMEND_LIST.AddObject(objCommendListData)
            Context.SaveChanges(log)
            gID = objCommendListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyCommendList(ByVal objCommendList As CommendListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendListData As New HU_COMMEND_LIST With {.ID = objCommendList.ID}
        Try
            Context.HU_COMMEND_LIST.Attach(objCommendListData)
            objCommendListData.ID = objCommendList.ID
            objCommendListData.CODE = objCommendList.CODE
            objCommendListData.NAME = objCommendList.NAME
            objCommendListData.DATATYPE_ID = objCommendList.DATATYPE_ID
            objCommendListData.TYPE_ID = objCommendList.TYPE_ID
            objCommendListData.OBJECT_ID = objCommendList.OBJECT_ID
            objCommendListData.NUMBER_ORDER = objCommendList.NUMBER_ORDER
            objCommendListData.LEVEL_ID = objCommendList.LEVEL_ID
            objCommendListData.REMARK = objCommendList.REMARK
            objCommendListData.EXCEL = objCommendList.EXCEL
            Context.SaveChanges(log)
            gID = objCommendListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ActiveCommendList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean
        Dim lstCommendListData As List(Of HU_COMMEND_LIST)
        Try
            lstCommendListData = (From p In Context.HU_COMMEND_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendListData.Count - 1
                lstCommendListData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function DeleteCommendList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_COMMEND_LIST)
            lstData = (From p In Context.HU_COMMEND_LIST Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_COMMEND_LIST.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCommendList(ByVal _validate As CommendListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMMEND_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMMEND_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If Not IsNothing(_validate.LEVEL_ID) Then
                    If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                        query = (From p In Context.HU_COMMEND_LEVEL
                                 Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                                 And p.ID = _validate.ID).FirstOrDefault
                        Return (Not query Is Nothing)
                    End If
                    If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                        query = (From p In Context.HU_COMMEND_LEVEL
                                 Where p.ID = _validate.ID).FirstOrDefault
                        Return (query Is Nothing)
                    End If
                Else
                    If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                        query = (From p In Context.HU_COMMEND_LIST
                                 Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                                 And p.ID = _validate.ID).FirstOrDefault
                        Return (Not query Is Nothing)
                    End If
                    If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                        query = (From p In Context.HU_COMMEND_LIST
                                 Where p.ID = _validate.ID).FirstOrDefault
                        Return (query Is Nothing)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommendCode(ByVal id As Decimal) As String
        Try
            Dim query = From p In Context.HU_COMMEND_LIST
                        Where p.ID = id
                        Select p.CODE

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
#Region "DM Kỷ luật"
    Public Function GetDisciplineList(ByVal _filter As DisciplineListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineListDTO)
        Try
            Dim query = From p In Context.HU_DISCIPLINE_LIST
                        From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                        Select New DisciplineListDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .DATATYPE_ID = p.DATATYPE_ID,
                                   .DATATYPE_NAME = da.NAME_VN,
                                   .TYPE_ID = p.TYPE_ID,
                                   .TYPE_NAME = t.NAME_VN,
                                   .NUMBER_ORDER = p.NUMBER_ORDER,
                                   .OBJECT_ID = p.OBJECT_ID,
                                   .OBJECT_NAME = o.NAME_VN,
                                   .EXCEL = p.EXCEL,
                                   .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

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
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            If _filter.DATATYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.DATATYPE_NAME.ToUpper.Contains(_filter.DATATYPE_NAME.ToUpper))
            End If

            If _filter.TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TYPE_NAME.ToUpper.Contains(_filter.TYPE_NAME.ToUpper))
            End If

            If _filter.LEVEL_NAME <> "" Then
                lst = lst.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.OBJECT_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_NAME.ToUpper.Contains(_filter.OBJECT_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetListDisciplineList(ByVal actflg As String) As List(Of DisciplineListDTO)
        Try
            If actflg <> "" Then
                Dim query = From p In Context.HU_DISCIPLINE_LIST
                            From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                            From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                            Where p.ACTFLG = actflg
                            Select New DisciplineListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATATYPE_ID = p.DATATYPE_ID,
                                       .DATATYPE_NAME = da.NAME_VN,
                                       .TYPE_ID = p.TYPE_ID,
                                       .TYPE_NAME = t.NAME_VN,
                                       .NUMBER_ORDER = p.NUMBER_ORDER,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = o.NAME_VN,
                                       .EXCEL = p.EXCEL,
                                       .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

                Return query.ToList
            Else
                Dim query = From p In Context.HU_DISCIPLINE_LIST
                            From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                            From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                            Select New DisciplineListDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATATYPE_ID = p.DATATYPE_ID,
                                       .DATATYPE_NAME = da.NAME_VN,
                                       .TYPE_ID = p.TYPE_ID,
                                       .TYPE_NAME = t.NAME_VN,
                                       .NUMBER_ORDER = p.NUMBER_ORDER,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = o.NAME_VN,
                                       .EXCEL = p.EXCEL,
                                       .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

                Return query.ToList
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetDisciplineListID(ByVal ID As Decimal) As List(Of DisciplineListDTO)
        Try
            Dim query = From p In Context.HU_DISCIPLINE_LIST
                        From da In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DATATYPE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.TYPE_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.OBJECT_ID).DefaultIfEmpty
                        Where p.ID = ID
                        Select New DisciplineListDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .REMARK = p.REMARK,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .DATATYPE_ID = p.DATATYPE_ID,
                                   .DATATYPE_NAME = da.NAME_VN,
                                   .TYPE_ID = p.TYPE_ID,
                                   .TYPE_NAME = t.NAME_VN,
                                   .NUMBER_ORDER = p.NUMBER_ORDER,
                                   .OBJECT_ID = p.OBJECT_ID,
                                   .OBJECT_NAME = o.NAME_VN,
                                  .EXCEL = p.EXCEL,
                                   .EXCEL_BOOL = If(p.EXCEL = -1, True, False)}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertDisciplineList(ByVal objDisciplineList As DisciplineListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDisciplineListData As New HU_DISCIPLINE_LIST
        Try
            objDisciplineListData.ID = Utilities.GetNextSequence(Context, Context.HU_DISCIPLINE_LIST.EntitySet.Name)
            objDisciplineListData.CODE = objDisciplineList.CODE
            objDisciplineListData.NAME = objDisciplineList.NAME
            objDisciplineListData.DATATYPE_ID = objDisciplineList.DATATYPE_ID
            objDisciplineListData.TYPE_ID = objDisciplineList.TYPE_ID
            objDisciplineListData.OBJECT_ID = objDisciplineList.OBJECT_ID
            objDisciplineListData.NUMBER_ORDER = objDisciplineList.NUMBER_ORDER
            objDisciplineListData.LEVEL_ID = objDisciplineList.LEVEL_ID
            objDisciplineListData.REMARK = objDisciplineList.REMARK
            objDisciplineListData.ACTFLG = objDisciplineList.ACTFLG
            objDisciplineListData.EXCEL = objDisciplineList.EXCEL
            Context.HU_DISCIPLINE_LIST.AddObject(objDisciplineListData)
            Context.SaveChanges(log)
            gID = objDisciplineListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyDisciplineList(ByVal objDisciplineList As DisciplineListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDisciplineListData As New HU_DISCIPLINE_LIST With {.ID = objDisciplineList.ID}
        Try
            Context.HU_DISCIPLINE_LIST.Attach(objDisciplineListData)
            objDisciplineListData.ID = objDisciplineList.ID
            objDisciplineListData.CODE = objDisciplineList.CODE
            objDisciplineListData.NAME = objDisciplineList.NAME
            objDisciplineListData.DATATYPE_ID = objDisciplineList.DATATYPE_ID
            objDisciplineListData.TYPE_ID = objDisciplineList.TYPE_ID
            objDisciplineListData.OBJECT_ID = objDisciplineList.OBJECT_ID
            objDisciplineListData.NUMBER_ORDER = objDisciplineList.NUMBER_ORDER
            objDisciplineListData.LEVEL_ID = objDisciplineList.LEVEL_ID
            objDisciplineListData.REMARK = objDisciplineList.REMARK
            objDisciplineListData.EXCEL = objDisciplineList.EXCEL
            Context.SaveChanges(log)
            gID = objDisciplineListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ActiveDisciplineList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean
        Dim lstDisciplineListData As List(Of HU_DISCIPLINE_LIST)
        Try
            lstDisciplineListData = (From p In Context.HU_DISCIPLINE_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstDisciplineListData.Count - 1
                lstDisciplineListData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function DeleteDisciplineList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_DISCIPLINE_LIST)
            lstData = (From p In Context.HU_DISCIPLINE_LIST Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_DISCIPLINE_LIST.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDisciplineList(ByVal _validate As DisciplineListDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_DISCIPLINE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_DISCIPLINE_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                'If Not IsNothing(_validate.LEVEL_ID) Then
                '    If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                '        query = (From p In Context.HU_Discipline_LEVEL
                '                 Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                '                 And p.ID = _validate.ID).FirstOrDefault
                '        Return (Not query Is Nothing)
                '    End If
                '    If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                '        query = (From p In Context.HU_Discipline_LEVEL
                '                 Where p.ID = _validate.ID).FirstOrDefault
                '        Return (query Is Nothing)
                '    End If
                'Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_DISCIPLINE_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                                 And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_DISCIPLINE_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            'End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetDisciplineCode(ByVal id As Decimal) As String
        Try
            Dim query = From p In Context.HU_DISCIPLINE_LIST
                        Where p.ID = id
                        Select p.CODE

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
#Region "Commend_Level - Cấp khen thưởng"


    Public Function GetCommendLevel(ByVal _filter As CommendLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO)

        Try
            Dim query = From p In Context.HU_COMMEND_LEVEL
                        Select New CommendLevelDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME = p.NAME,
                                   .COMMEND_LEVEL = p.COMMEND_LEVEL,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}

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
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCommendLevelID(ByVal ID As Decimal) As CommendLevelDTO

        Try
            Dim query = (From p In Context.HU_COMMEND_LEVEL
                         Where p.ID = ID
                         Select New CommendLevelDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                   .NAME = p.NAME,
                                    .COMMEND_LEVEL = p.COMMEND_LEVEL,
                                    .REMARK = p.REMARK,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendLevelData As New HU_COMMEND_LEVEL
        Dim iCount As Integer = 0
        Try
            objCommendLevelData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_LEVEL.EntitySet.Name)
            objCommendLevelData.CODE = objCommendLevel.CODE
            objCommendLevelData.NAME = objCommendLevel.NAME
            objCommendLevelData.COMMEND_LEVEL = objCommendLevel.COMMEND_LEVEL
            objCommendLevelData.ACTFLG = objCommendLevel.ACTFLG
            objCommendLevelData.REMARK = objCommendLevel.REMARK
            Context.HU_COMMEND_LEVEL.AddObject(objCommendLevelData)
            Context.SaveChanges(log)
            gID = objCommendLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    ''' <editby>Hongdx</editby>
    ''' <content>Them validate Ap dung</content>
    ''' <summary>
    ''' Check validate Code, Id, ACTFLG
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCommendLevel(ByVal _validate As CommendLevelDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_COMMEND_LEVEL
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendLevelData As HU_COMMEND_LEVEL
        Try
            objCommendLevelData = (From p In Context.HU_COMMEND_LEVEL Where p.ID = objCommendLevel.ID).FirstOrDefault
            objCommendLevelData.ID = objCommendLevel.ID
            objCommendLevelData.CODE = objCommendLevel.CODE
            objCommendLevelData.NAME = objCommendLevel.NAME
            objCommendLevelData.COMMEND_LEVEL = objCommendLevel.COMMEND_LEVEL
            objCommendLevelData.REMARK = objCommendLevel.REMARK
            Context.SaveChanges(log)
            gID = objCommendLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveCommendLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstCommendLevelData As List(Of HU_COMMEND_LEVEL)
        Try
            lstCommendLevelData = (From p In Context.HU_COMMEND_LEVEL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendLevelData.Count - 1
                lstCommendLevelData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteCommendLevel(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstCommendLevelData As List(Of HU_COMMEND_LEVEL)
        Try

            lstCommendLevelData = (From p In Context.HU_COMMEND_LEVEL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendLevelData.Count - 1
                Context.HU_COMMEND_LEVEL.DeleteObject(lstCommendLevelData(index))
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "Khoa dao tao - Course"
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

#Region "Tìm kiếm kế nhiệm (Talent Pool)"

    Public Function GetTalentPool(ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of TalentPoolDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_TALENT_POOL
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From e_cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New TalentPoolDTO With {.ID = p.p.ID,
                                                                    .CODE = p.e.EMPLOYEE_CODE,
                                                                    .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                    .BIRTH_DAY = p.e_cv.BIRTH_DATE,
                                                                    .GENDER = If(p.e_cv.GENDER = 565, "Nam", "Nữ"),
                                                                    .TITLE_NAME = p.t.NAME_VN,
                                                                    .ORG_NAME = p.o.NAME_VN,
                                                                    .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                    .ACTFLG = p.p.ACTFLG,
                                                                    .FILTER_ID = p.p.FILTER_ID,
                                                                    .EMP_SUCCESS_ID = p.p.EMP_SUCCESS_ID,
                                                                    .EMP_SUCCESS_NAME = p.e.FULLNAME_VN,
                                                                    .TITLE_SUCCESS_NAME = p.t.NAME_VN,
                                                                    .NOTE = p.p.NOTE,
                                                                    .CREATED_DATE = p.p.CREATED_DATE,
                                                                    .CREATED_BY = p.p.CREATED_BY,
                                                                    .CREATED_LOG = p.p.CREATED_LOG,
                                                                    .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                    .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                    .MODIFIED_LOG = p.p.MODIFIED_LOG})

            lst = lst.Where(Function(p) p.ACTFLG = "A")

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO), ByVal log As UserLog) As Boolean
        Try
            For idx = 0 To lstTalentPool.Count - 1
                Dim obj As TalentPoolDTO = lstTalentPool(idx)
                Dim objTalentPoolData As New HU_TALENT_POOL
                objTalentPoolData.ID = Utilities.GetNextSequence(Context, Context.HU_TALENT_POOL.EntitySet.Name)
                objTalentPoolData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                objTalentPoolData.FILTER_ID = obj.FILTER_ID
                objTalentPoolData.ACTFLG = "A"
                Context.HU_TALENT_POOL.AddObject(objTalentPoolData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveTalentPool(ByVal lst As List(Of Decimal), ByVal sActive As String,
                               ByVal log As UserLog) As Boolean

        Dim lstTalentPool As List(Of HU_TALENT_POOL)
        Try
            lstTalentPool = (From p In Context.HU_TALENT_POOL Where lst.Contains(p.ID)).ToList
            For index = 0 To lstTalentPool.Count - 1
                lstTalentPool(index).ACTFLG = sActive
                lstTalentPool(index).MODIFIED_DATE = DateTime.Now
                lstTalentPool(index).MODIFIED_BY = log.Username
                lstTalentPool(index).MODIFIED_LOG = log.ComputerName
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO, ByVal log As UserLog) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_TALENT_POOL.FILTER_TALENT_POOL",
                                           New With {.P_CON_POSTION = obj.P_CON_POSTION,
                                                     .P_CON_KN_VT = obj.P_CON_KN_VT,
                                                     .P_CON_DT_CT = obj.P_CON_DT_CT,
                                                     .P_CON_SENIOR_KN = obj.P_CON_SENIOR_KN,
                                                     .P_CON_AGE = obj.P_CON_AGE,
                                                     .P_CON_TD_HV = obj.P_CON_TD_HV,
                                                     .P_CON_BC_CC = obj.P_CON_BC_CC,
                                                     .P_CON_KDT = obj.P_CON_KDT,
                                                     .P_CON_NL_CM = obj.P_CON_NL_CM,
                                                     .P_CON_GENDER = obj.P_CON_GENDER,
                                                     .P_CON_KY_DG = obj.P_CON_KY_DG,
                                                     .P_CON_KQ_DG = obj.P_CON_KQ_DG,
                                                     .P_CON_STAFF_RANK = obj.P_CON_STAFF_RANK,
                                                     .P_USERNAME = log.Username,
                                                     .P_ORG_ID = obj.P_ORG_ID,
                                                     .DATA = cls.OUT_CURSOR}, False)
            Return dtData.Tables(0)
        End Using
        Return Nothing
    End Function
#End Region

#Region "Location"
    Public Function GetLocationID(ByVal ID As Decimal) As LocationDTO
        Dim query As LocationDTO
        Try
            query = (From p In Context.HU_LOCATION
                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     Where p.ID = ID
                     Select New LocationDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .ORG_ID = p.ORG_ID,
                                                  .ADDRESS = p.ADDRESS,
                                                  .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                  .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                  .LOCATION_SHORT_NAME = o.SHORT_NAME,
                                                  .WORK_ADDRESS = p.WORK_ADDRESS,
                                                  .PHONE = p.PHONE,
                                                  .FAX = p.FAX,
                                                  .WEBSITE = p.WEBSITE,
                                                  .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                  .BANK_ID = p.BANK_ID,
                                                  .TAX_CODE = p.TAX_CODE,
                                                  .TAX_DATE = p.TAX_DATE,
                                                  .TAX_PLACE = p.TAX_PLACE,
                                                  .EMP_LAW_ID = p.EMP_LAW_ID,
                                                  .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                  .BUSINESS_NAME = p.BUSINESS_NAME,
                                                  .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                  .NOTE = p.NOTE,
                                                  .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                  .LOCATION_VN_NAME = o.NAME_VN,
                                                  .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                  .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                  .PROVINCE_ID = p.PROVINCE_ID,
                                                  .DISTRICT_ID = p.DISTRICT_ID,
                                                  .WARD_ID = p.WARD_ID,
                                                  .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                  .FILE_LOGO = p.FILE_LOGO,
                                                  .ATTACH_FILE_LOGO = p.ATTACH_FILE_LOGO,
                                                  .FILE_HEADER = p.FILE_HEADER,
                                                  .ATTACH_FILE_HEADER = p.ATTACH_FILE_HEADER,
                                                  .FILE_FOOTER = p.FILE_FOOTER,
                                                  .ATTACH_FILE_FOOTER = p.ATTACH_FILE_FOOTER,
                                                  .CHANGE_TAX_CODE = p.CHANGE_TAX_CODE,
                                                  .NAME_VN = p.NAME_VN,
                                                  .REGION = p.REGION,
                         .INS_LIST_ID = p.INS_LIST_ID
                                                  }).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetLocation(ByVal sACT As String, ByVal lstOrgID As List(Of Decimal)) As List(Of LocationDTO)
        Dim query As ObjectQuery(Of LocationDTO)
        Try
            If sACT = "" Then
                query = (From p In Context.HU_LOCATION.Where(Function(x) lstOrgID.Contains(x.ORG_ID))
                         From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_LAW_ID).DefaultIfEmpty
                         From emp1 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_SIGNCONTRACT_ID).DefaultIfEmpty
                         From d In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                         From pr In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                         From w In Context.HU_WARD.Where(Function(f) f.ID = p.WARD_ID).DefaultIfEmpty
                         From b In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID).DefaultIfEmpty
                         From bb In Context.HU_BANK_BRANCH.Where(Function(f) f.ID = p.BANK_BRANCH_ID).DefaultIfEmpty
                         From i In Context.INS_LIST_INSURANCE.Where(Function(f) f.ID = p.INS_LIST_ID).DefaultIfEmpty
                         Where p.LOCATION_VN_NAME IsNot Nothing
                         Select New LocationDTO With {.ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .ORG_ID = p.ORG_ID,
                                                      .ADDRESS = p.ADDRESS,
                                                      .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                      .LOCATION_SHORT_NAME = p.LOCATION_SHORT_NAME,
                                                      .WORK_ADDRESS = p.WORK_ADDRESS,
                                                      .PHONE = p.PHONE,
                                                      .FAX = p.FAX,
                                                      .WEBSITE = p.WEBSITE,
                                                      .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                      .BANK_ID = p.BANK_ID,
                                                      .TAX_CODE = p.TAX_CODE,
                                                      .TAX_DATE = p.TAX_DATE,
                                                      .TAX_PLACE = p.TAX_PLACE,
                                                      .EMP_LAW_ID = p.EMP_LAW_ID,
                                                      .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                      .BUSINESS_NAME = p.BUSINESS_NAME,
                                                      .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                      .NOTE = p.NOTE,
                                                      .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                      .LOCATION_VN_NAME = p.LOCATION_VN_NAME,
                                                      .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                      .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                      .PROVINCE_ID = p.PROVINCE_ID,
                                                      .DISTRICT_ID = p.DISTRICT_ID,
                                                      .WARD_ID = p.WARD_ID,
                                                      .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                      .ATTACH_FILE_LOGO = p.ATTACH_FILE_LOGO,
                                                      .FILE_HEADER = p.FILE_HEADER,
                                                      .ATTACH_FILE_HEADER = p.ATTACH_FILE_HEADER,
                                                      .FILE_FOOTER = p.FILE_FOOTER,
                                                      .ATTACH_FILE_FOOTER = p.ATTACH_FILE_FOOTER,
                                                      .CHANGE_TAX_CODE = p.CHANGE_TAX_CODE,
                                                     .EMP_LAW_NAME = emp.FULLNAME_VN,
                                                      .EMP_SIGNCONTRACT_NAME = emp1.FULLNAME_VN,
                                                     .INS_LIST_ID = p.INS_LIST_ID,
                                                     .INS_LIST_NAME = i.NAME,
                                                     .BANK_BRANCH_NAME = bb.NAME,
                                                     .BANK_NAME = b.NAME,
                                                     .DISTRICT_NAME = d.NAME_VN,
                                                     .PROVINCE_NAME = pr.NAME_VN,
                                                     .WARD_NAME = w.NAME_VN})
            Else
                query = (From p In Context.HU_LOCATION.Where(Function(x) lstOrgID.Contains(x.ORG_ID))
                         From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_LAW_ID).DefaultIfEmpty
                         From emp1 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_SIGNCONTRACT_ID).DefaultIfEmpty
                         From d In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                         From pr In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                         From w In Context.HU_WARD.Where(Function(f) f.ID = p.WARD_ID).DefaultIfEmpty
                         From b In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID).DefaultIfEmpty
                         From bb In Context.HU_BANK_BRANCH.Where(Function(f) f.ID = p.BANK_BRANCH_ID).DefaultIfEmpty
                         From i In Context.INS_LIST_INSURANCE.Where(Function(f) f.ID = p.INS_LIST_ID).DefaultIfEmpty
                         Where p.ACTFLG = sACT AndAlso p.LOCATION_VN_NAME IsNot Nothing
                         Select New LocationDTO With {.ID = p.ID,
                                                     .CODE = p.CODE,
                                                    .ORG_ID = p.ORG_ID,
                                                      .ADDRESS = p.ADDRESS,
                                                      .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                     .LOCATION_SHORT_NAME = p.LOCATION_SHORT_NAME,
                                                   .WORK_ADDRESS = p.WORK_ADDRESS,
                                                     .PHONE = p.PHONE,
                                                     .FAX = p.FAX,
                                                     .WEBSITE = p.WEBSITE,
                                                    .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                     .BANK_ID = p.BANK_ID,
                                                     .TAX_CODE = p.TAX_CODE,
                                                     .TAX_DATE = p.TAX_DATE,
                                                     .TAX_PLACE = p.TAX_PLACE,
                                                     .EMP_LAW_ID = p.EMP_LAW_ID,
                                                     .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                     .BUSINESS_NAME = p.BUSINESS_NAME,
                                                     .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                     .NOTE = p.NOTE,
                                                     .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                     .LOCATION_VN_NAME = p.LOCATION_VN_NAME,
                                                     .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                     .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                      .PROVINCE_ID = p.PROVINCE_ID,
                                                      .DISTRICT_ID = p.DISTRICT_ID,
                                                      .WARD_ID = p.WARD_ID,
                                                      .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                      .ATTACH_FILE_LOGO = p.ATTACH_FILE_LOGO,
                                                      .FILE_HEADER = p.FILE_HEADER,
                                                      .ATTACH_FILE_HEADER = p.ATTACH_FILE_HEADER,
                                                      .FILE_FOOTER = p.FILE_FOOTER,
                                                      .ATTACH_FILE_FOOTER = p.ATTACH_FILE_FOOTER,
                                                      .CHANGE_TAX_CODE = p.CHANGE_TAX_CODE,
                                                    .EMP_LAW_NAME = emp.FULLNAME_VN,
                                                      .EMP_SIGNCONTRACT_NAME = emp1.FULLNAME_VN,
                                                     .INS_LIST_ID = p.INS_LIST_ID,
                                                     .INS_LIST_NAME = i.NAME,
                                                     .BANK_BRANCH_NAME = bb.NAME,
                                                     .BANK_NAME = b.NAME,
                                                     .DISTRICT_NAME = d.NAME_VN,
                                                     .PROVINCE_NAME = pr.NAME_VN,
                                                     .WARD_NAME = w.NAME_VN})
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetLocation_V2(ByVal _filter As LocationDTO,
                                   ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer, ByVal _param As ParamDTO,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of LocationDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_LOCATION
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_LAW_ID).DefaultIfEmpty
                        From emp1 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_SIGNCONTRACT_ID).DefaultIfEmpty
                        From d In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                        From pr In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        From w In Context.HU_WARD.Where(Function(f) f.ID = p.WARD_ID).DefaultIfEmpty
                        From b In Context.HU_BANK.Where(Function(f) f.ID = p.BANK_ID).DefaultIfEmpty
                        From bb In Context.HU_BANK_BRANCH.Where(Function(f) f.ID = p.BANK_BRANCH_ID).DefaultIfEmpty
                        From i In Context.INS_LIST_INSURANCE.Where(Function(f) f.ID = p.INS_LIST_ID).DefaultIfEmpty
                        From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGION).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New LocationDTO With {.ID = p.ID,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .CODE = p.CODE,
                                                     .ORG_ID = p.ORG_ID,
                                                     .ADDRESS = p.ADDRESS,
                                                     .CONTRACT_PLACE = p.CONTRACT_PLACE,
                                                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                     .LOCATION_SHORT_NAME = o.SHORT_NAME,
                                                     .WORK_ADDRESS = p.WORK_ADDRESS,
                                                     .PHONE = p.PHONE,
                                                     .FAX = p.FAX,
                                                     .WEBSITE = p.WEBSITE,
                                                     .ACCOUNT_NUMBER = p.ACCOUNT_NUMBER,
                                                     .BANK_ID = p.BANK_ID,
                                                     .TAX_CODE = p.TAX_CODE,
                                                     .TAX_DATE = p.TAX_DATE,
                                                     .TAX_PLACE = p.TAX_PLACE,
                                                     .EMP_LAW_ID = p.EMP_LAW_ID,
                                                     .EMP_SIGNCONTRACT_ID = p.EMP_SIGNCONTRACT_ID,
                                                     .BUSINESS_NAME = p.BUSINESS_NAME,
                                                     .BUSINESS_NUMBER = p.BUSINESS_NUMBER,
                                                     .NOTE = p.NOTE,
                                                     .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                                                     .NAME_VN = p.NAME_VN,
                                                     .LOCATION_VN_NAME = o.NAME_VN,
                                                     .BUSINESS_REG_DATE = p.BUSINESS_REG_DATE,
                                                     .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                                     .PROVINCE_ID = p.PROVINCE_ID,
                                                     .DISTRICT_ID = p.DISTRICT_ID,
                                                     .WARD_ID = p.WARD_ID,
                                                     .IS_SIGN_CONTRACT = p.IS_SIGN_CONTRACT,
                                                     .ATTACH_FILE_LOGO = p.ATTACH_FILE_LOGO,
                                                     .FILE_HEADER = p.FILE_HEADER,
                                                     .ATTACH_FILE_HEADER = p.ATTACH_FILE_HEADER,
                                                     .FILE_FOOTER = p.FILE_FOOTER,
                                                     .ATTACH_FILE_FOOTER = p.ATTACH_FILE_FOOTER,
                                                     .CHANGE_TAX_CODE = p.CHANGE_TAX_CODE,
                                                     .EMP_LAW_NAME = emp.FULLNAME_VN,
                                                     .EMP_SIGNCONTRACT_NAME = emp1.FULLNAME_VN,
                                                     .INS_LIST_ID = p.INS_LIST_ID,
                                                     .INS_LIST_NAME = i.NAME,
                                                     .BANK_BRANCH_NAME = bb.NAME,
                                                     .BANK_NAME = b.NAME,
                                                     .DISTRICT_NAME = d.NAME_VN,
                                                     .PROVINCE_NAME = pr.NAME_VN,
                                                     .WARD_NAME = w.NAME_VN,
                                                     .REGION = p.REGION,
                                                     .REGION_NAME = r.NAME_VN}
            Dim lst = query

            'Filter
            If Not String.IsNullOrEmpty(_filter.LOCATION_VN_NAME) Then
                lst = lst.Where(Function(p) p.LOCATION_VN_NAME.ToUpper.Contains(_filter.LOCATION_VN_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LOCATION_SHORT_NAME) Then
                lst = lst.Where(Function(p) p.LOCATION_SHORT_NAME.ToUpper.Contains(_filter.LOCATION_SHORT_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LOCATION_EN_NAME) Then
                lst = lst.Where(Function(p) p.LOCATION_EN_NAME.ToUpper.Contains(_filter.LOCATION_EN_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS) Then
                lst = lst.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PHONE) Then
                lst = lst.Where(Function(p) p.PHONE.ToUpper.Contains(_filter.PHONE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.WORK_ADDRESS) Then
                lst = lst.Where(Function(p) p.WORK_ADDRESS.ToUpper.Contains(_filter.WORK_ADDRESS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PROVINCE_NAME) Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.DISTRICT_NAME) Then
                lst = lst.Where(Function(p) p.DISTRICT_NAME.ToUpper.Contains(_filter.DISTRICT_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.WARD_NAME) Then
                lst = lst.Where(Function(p) p.WARD_NAME.ToUpper.Contains(_filter.WARD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.INS_LIST_NAME) Then
                lst = lst.Where(Function(p) p.INS_LIST_NAME.ToUpper.Contains(_filter.INS_LIST_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACCOUNT_NUMBER) Then
                lst = lst.Where(Function(p) p.ACCOUNT_NUMBER.ToUpper.Contains(_filter.ACCOUNT_NUMBER.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.BANK_NAME) Then
                lst = lst.Where(Function(p) p.BANK_NAME.ToUpper.Contains(_filter.BANK_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.BANK_BRANCH_NAME) Then
                lst = lst.Where(Function(p) p.BANK_BRANCH_NAME.ToUpper.Contains(_filter.BANK_BRANCH_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TAX_CODE) Then
                lst = lst.Where(Function(p) p.TAX_CODE.ToUpper.Contains(_filter.TAX_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CHANGE_TAX_CODE) Then
                lst = lst.Where(Function(p) p.CHANGE_TAX_CODE.ToUpper.Contains(_filter.CHANGE_TAX_CODE.ToUpper))
            End If
            If _filter.TAX_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TAX_DATE = _filter.TAX_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMP_LAW_NAME) Then
                lst = lst.Where(Function(p) p.EMP_LAW_NAME.ToUpper.Contains(_filter.EMP_LAW_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMP_SIGNCONTRACT_NAME) Then
                lst = lst.Where(Function(p) p.EMP_SIGNCONTRACT_NAME.ToUpper.Contains(_filter.EMP_SIGNCONTRACT_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.BUSINESS_NUMBER) Then
                lst = lst.Where(Function(p) p.BUSINESS_NUMBER.ToUpper.Contains(_filter.BUSINESS_NUMBER.ToUpper))
            End If
            If _filter.BUSINESS_REG_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BUSINESS_REG_DATE = _filter.BUSINESS_REG_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.WEBSITE) Then
                lst = lst.Where(Function(p) p.WEBSITE.ToUpper.Contains(_filter.WEBSITE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.FAX) Then
                lst = lst.Where(Function(p) p.FAX.ToUpper.Contains(_filter.FAX.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertLocation(ByVal objLocation As LocationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLocationData As New HU_LOCATION
        Try
            objLocationData.ID = Utilities.GetNextSequence(Context, Context.HU_LOCATION.EntitySet.Name)
            objLocationData.CODE = objLocation.CODE
            objLocationData.ORG_ID = objLocation.ORG_ID
            objLocationData.ADDRESS = objLocation.ADDRESS
            objLocationData.CONTRACT_PLACE = objLocation.CONTRACT_PLACE
            objLocationData.LOCATION_SHORT_NAME = objLocation.LOCATION_SHORT_NAME
            objLocationData.WORK_ADDRESS = objLocation.WORK_ADDRESS
            objLocationData.PHONE = objLocation.PHONE
            objLocationData.FAX = objLocation.FAX
            objLocationData.WEBSITE = objLocation.WEBSITE
            objLocationData.ACCOUNT_NUMBER = objLocation.ACCOUNT_NUMBER
            objLocationData.BANK_ID = objLocation.BANK_ID
            objLocationData.TAX_CODE = objLocation.TAX_CODE
            objLocationData.ACTFLG = objLocation.ACTFLG
            objLocationData.TAX_DATE = objLocation.TAX_DATE
            objLocationData.TAX_PLACE = objLocation.TAX_PLACE
            objLocationData.EMP_LAW_ID = objLocation.EMP_LAW_ID
            objLocationData.EMP_SIGNCONTRACT_ID = objLocation.EMP_SIGNCONTRACT_ID
            objLocationData.BUSINESS_NAME = objLocation.BUSINESS_NAME
            objLocationData.BUSINESS_NUMBER = objLocation.BUSINESS_NUMBER
            objLocationData.NOTE = objLocation.NOTE
            objLocationData.LOCATION_EN_NAME = objLocation.LOCATION_EN_NAME
            objLocationData.LOCATION_VN_NAME = objLocation.LOCATION_VN_NAME
            objLocationData.BUSINESS_REG_DATE = objLocation.BUSINESS_REG_DATE
            objLocationData.BANK_BRANCH_ID = objLocation.BANK_BRANCH_ID
            objLocationData.PROVINCE_ID = objLocation.PROVINCE_ID
            objLocationData.DISTRICT_ID = objLocation.DISTRICT_ID
            objLocationData.WARD_ID = objLocation.WARD_ID
            objLocationData.IS_SIGN_CONTRACT = objLocation.IS_SIGN_CONTRACT
            objLocationData.FILE_LOGO = objLocation.FILE_LOGO
            objLocationData.ATTACH_FILE_LOGO = objLocation.ATTACH_FILE_LOGO
            objLocationData.FILE_HEADER = objLocation.FILE_HEADER
            objLocationData.ATTACH_FILE_HEADER = objLocation.ATTACH_FILE_HEADER
            objLocationData.FILE_FOOTER = objLocation.FILE_FOOTER
            objLocationData.ATTACH_FILE_FOOTER = objLocation.ATTACH_FILE_FOOTER
            objLocationData.CHANGE_TAX_CODE = objLocation.CHANGE_TAX_CODE
            objLocationData.REGION = objLocation.REGION
            objLocationData.INS_LIST_ID = objLocation.INS_LIST_ID
            objLocationData.NAME_VN = objLocation.NAME_VN
            Context.HU_LOCATION.AddObject(objLocationData)
            Context.SaveChanges(log)
            gID = objLocationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyLocation(ByVal objLocation As LocationDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLocationData As New HU_LOCATION With {.ID = objLocation.ID}

        Try
            Context.HU_LOCATION.Attach(objLocationData)
            objLocationData.CODE = objLocation.CODE
            objLocationData.ORG_ID = objLocation.ORG_ID
            objLocationData.ADDRESS = objLocation.ADDRESS
            objLocationData.CONTRACT_PLACE = objLocation.CONTRACT_PLACE
            objLocationData.LOCATION_SHORT_NAME = objLocation.LOCATION_SHORT_NAME
            objLocationData.WORK_ADDRESS = objLocation.WORK_ADDRESS
            objLocationData.PHONE = objLocation.PHONE
            objLocationData.FAX = objLocation.FAX
            objLocationData.WEBSITE = objLocation.WEBSITE
            objLocationData.ACCOUNT_NUMBER = objLocation.ACCOUNT_NUMBER
            objLocationData.BANK_ID = objLocation.BANK_ID
            objLocationData.TAX_CODE = objLocation.TAX_CODE
            objLocationData.TAX_DATE = objLocation.TAX_DATE
            objLocationData.TAX_PLACE = objLocation.TAX_PLACE
            objLocationData.EMP_LAW_ID = objLocation.EMP_LAW_ID
            objLocationData.EMP_SIGNCONTRACT_ID = objLocation.EMP_SIGNCONTRACT_ID
            objLocationData.BUSINESS_NAME = objLocation.BUSINESS_NAME
            objLocationData.BUSINESS_NUMBER = objLocation.BUSINESS_NUMBER
            objLocationData.NOTE = objLocation.NOTE
            objLocationData.LOCATION_EN_NAME = objLocation.LOCATION_EN_NAME
            objLocationData.LOCATION_VN_NAME = objLocation.LOCATION_VN_NAME
            objLocationData.BUSINESS_REG_DATE = objLocation.BUSINESS_REG_DATE
            objLocationData.BANK_BRANCH_ID = objLocation.BANK_BRANCH_ID
            objLocationData.PROVINCE_ID = objLocation.PROVINCE_ID
            objLocationData.DISTRICT_ID = objLocation.DISTRICT_ID
            objLocationData.WARD_ID = objLocation.WARD_ID
            objLocationData.IS_SIGN_CONTRACT = objLocation.IS_SIGN_CONTRACT
            objLocationData.FILE_LOGO = objLocation.FILE_LOGO
            objLocationData.ATTACH_FILE_LOGO = objLocation.ATTACH_FILE_LOGO
            objLocationData.FILE_HEADER = objLocation.FILE_HEADER
            objLocationData.ATTACH_FILE_HEADER = objLocation.ATTACH_FILE_HEADER
            objLocationData.FILE_FOOTER = objLocation.FILE_FOOTER
            objLocationData.ATTACH_FILE_FOOTER = objLocation.ATTACH_FILE_FOOTER
            objLocationData.CHANGE_TAX_CODE = objLocation.CHANGE_TAX_CODE
            objLocationData.REGION = objLocation.REGION
            objLocationData.NAME_VN = objLocation.NAME_VN
            objLocationData.INS_LIST_ID = objLocation.INS_LIST_ID
            Context.SaveChanges(log)
            gID = objLocationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ActiveLocation(ByVal lstLocation As List(Of LocationDTO), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstLocationData As List(Of HU_LOCATION)
        Dim lstIDLocation As List(Of Decimal) = (From p In lstLocation.ToList Select p.ID).ToList
        lstLocationData = (From p In Context.HU_LOCATION Where lstIDLocation.Contains(p.ID)).ToList
        For index = 0 To lstLocationData.Count - 1
            lstLocationData(index).ACTFLG = sActive
            lstLocationData(index).MODIFIED_DATE = DateTime.Now
            lstLocationData(index).MODIFIED_BY = log.Username
            lstLocationData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ActiveLocationID(ByVal lstLocation As LocationDTO, ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim location As HU_LOCATION
        Try
            location = (From p In Context.HU_LOCATION Where p.ID = lstLocation.ID).SingleOrDefault()
            If location IsNot Nothing Then
                location.ACTFLG = sActive
                location.MODIFIED_DATE = DateTime.Now
                location.MODIFIED_BY = log.Username
                location.MODIFIED_LOG = log.ComputerName
                Context.SaveChanges(log)
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteLocationID(ByVal lstlocation As Decimal,
                                  ByVal log As UserLog) As Boolean
        Dim location As HU_LOCATION
        Try
            Dim _count = (From p In Context.HU_CONTRACT Where p.ID_SIGN_CONTRACT = lstlocation).ToList
            If _count.Count <= 0 Then
                location = (From p In Context.HU_LOCATION Where p.ID = lstlocation).SingleOrDefault()
                If location IsNot Nothing Then
                    Context.HU_LOCATION.DeleteObject(location)
                    Context.SaveChanges(log)
                    Return True
                End If
                Return False
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region " danh mục người ký"
    'load dữ liệu
    Public Function GET_HU_SIGNER(ByVal _filter As SignerDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_SIGNER
                        From g In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username)

            Dim lst = query.Select(Function(f) New SignerDTO With {
                                        .ID = f.p.ID,
                                        .SIGNER_CODE = f.p.SIGNER_CODE,
                                        .NAME = f.p.NAME,
                                        .TITLE_NAME = f.p.TITLE_NAME,
                                        .SIGNER_ID = f.p.SIGNER_ID,
                                        .ORG_NAME = f.g.NAME_VN,
                                        .REMARK = f.p.REMARK,
                                        .ORG_ID = f.p.ORG_ID,
                                        .ACTFLG = If(f.p.ACTFLG = 1, "Áp dụng", "Ngưng áp dụng")
                                       })

            Return lst.ToList.ToTable()

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'thêm người ký
    Public Function INSERT_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.INSERT_HU_SIGNER", New With {
                                                                                                 PA.NAME,
                                                                                                 PA.SIGNER_CODE,
                                                                                                 PA.SIGNER_ID,
                                                                                                 PA.TITLE_NAME,
                                                                                                PA.REMARK,
                                                                                                 PA.ORG_ID,
                                                                                                  PA.CREATED_BY,
                                                                                                PA.CREATED_LOG,
                                                                                                 .P_OUT = cls.OUT_NUMBER})
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' update dữ liệu 

    Public Function UPDATE_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.UPDATE_HU_SIGNER", New With {
                                                                                            PA.ID,
                                                                                            PA.NAME,
                                                                                            PA.SIGNER_CODE,
                                                                                            PA.SIGNER_ID,
                                                                                            PA.TITLE_NAME,
                                                                                            PA.REMARK,
                                                                                            PA.ORG_ID,
                                                                                            PA.CREATED_BY,
                                                                                            PA.CREATED_LOG,
                                                                                            .P_OUT = cls.OUT_NUMBER})
                Return True
            End Using
        Catch ex As Exception

        End Try
    End Function
    'CHECK DL DA TON TAI HAY CHUA
    Public Function CHECK_EXIT(ByVal P_ID As String, ByVal idemp As Decimal, ByVal ORG_ID As Decimal, ByVal title_name As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.CHECK_EXIT", New With {
                                                                                    P_ID,
                                                                                    idemp,
                                                                                    ORG_ID,
                                                                                    title_name,
                                                                                  .P_OUT = cls.OUT_CURSOR})
                If Decimal.Parse(dtData(0)("CHECK1").ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If


            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'XOA
    Public Function DeleteSigner(ByVal lstID As String)
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.DELETE_SIGNER", New With {
                                                                                    lstID
                                                                                 })
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'AP DUNG HOAC NGUNG AP DUNG
    Public Function DeactiveAndActiveSigner(ByVal lstID As String, ByVal sActive As Decimal)
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.DeactiveAndActiveSigner", New With {
                                                                                    lstID,
                                                                                    sActive
                                                                                 })
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function

    Public Function CHECK_LOCATION_EXITS(ByVal P_ID As Decimal?, ByVal ORG_ID As Decimal) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.CHECK_LOCATION_EXITS", New With {.P_ID = P_ID, .P_ORG_ID = ORG_ID, .P_OUT = cls.OUT_CURSOR})
                If Decimal.Parse(dtData(0)("CHECK1").ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Work Place"
    Public Function GetWorkPlace(ByVal _filter As WorkPlaceDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkPlaceDTO)

        Try
            Dim query = From p In Context.HU_WORK_PLACE
                        From province In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        From district In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                        From ward In Context.HU_WARD.Where(Function(f) f.ID = p.WARD_ID).DefaultIfEmpty
                        From region In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGION_ID).DefaultIfEmpty
                        Select New WorkPlaceDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .ACTFLG_Search = p.ACTFLG,
                                   .PROVINCE_ID = p.PROVINCE_ID,
                                   .PROVINCE_NAME = province.NAME_VN,
                                   .DISTRICT_ID = p.DISTRICT_ID,
                                   .DISTRICT_NAME = district.NAME_VN,
                                   .WARD_ID = p.WARD_ID,
                                   .WARD_NAME = ward.NAME_VN,
                                   .REGION_ID = p.REGION_ID,
                                   .PLACE = p.PLACE,
                                   .REGION_NAME = region.NAME_VN,
                                   .PHONE_NUMBER = p.PHONE_NUMBER,
                                   .FAX = p.FAX}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PROVINCE_NAME) Then
                lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.DISTRICT_NAME) Then
                lst = lst.Where(Function(p) p.DISTRICT_NAME.ToUpper.Contains(_filter.DISTRICT_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.WARD_NAME) Then
                lst = lst.Where(Function(p) p.WARD_NAME.ToUpper.Contains(_filter.WARD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REGION_NAME) Then
                lst = lst.Where(Function(p) p.REGION_NAME.ToUpper.Contains(_filter.REGION_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PLACE) Then
                lst = lst.Where(Function(p) p.PLACE.ToUpper.Contains(_filter.PLACE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PHONE_NUMBER) Then
                lst = lst.Where(Function(p) p.PHONE_NUMBER.ToUpper.Contains(_filter.PHONE_NUMBER.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.FAX) Then
                lst = lst.Where(Function(p) p.FAX.ToUpper.Contains(_filter.FAX.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG_Search) Then
                lst = lst.Where(Function(p) p.ACTFLG_Search.ToUpper = _filter.ACTFLG_Search.ToUpper)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetWorkPlaceID(ByVal ID As Decimal) As WorkPlaceDTO

        Try
            Dim query = (From p In Context.HU_WORK_PLACE
                         From province In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                         From district In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                         From ward In Context.HU_WARD.Where(Function(f) f.ID = p.WARD_ID).DefaultIfEmpty
                         From region In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGION_ID).DefaultIfEmpty
                         Where p.ID = ID
                         Select New WorkPlaceDTO With {
                                    .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .PROVINCE_ID = p.PROVINCE_ID,
                                   .PROVINCE_NAME = province.NAME_VN,
                                   .DISTRICT_ID = p.DISTRICT_ID,
                                   .DISTRICT_NAME = district.NAME_VN,
                                   .WARD_ID = p.WARD_ID,
                                   .WARD_NAME = ward.NAME_VN,
                                   .REGION_ID = p.REGION_ID,
                                   .PLACE = p.PLACE,
                                   .REGION_NAME = region.NAME_VN,
                                   .PHONE_NUMBER = p.PHONE_NUMBER,
                                   .FAX = p.FAX}).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertWorkPlace(ByVal objWorkPlace As WorkPlaceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkPlaceData As New HU_WORK_PLACE
        Dim iCount As Integer = 0
        Try
            objWorkPlaceData.ID = Utilities.GetNextSequence(Context, Context.HU_WORK_PLACE.EntitySet.Name)
            objWorkPlaceData.CODE = objWorkPlace.CODE
            objWorkPlaceData.NAME_VN = objWorkPlace.NAME_VN
            objWorkPlaceData.ACTFLG = objWorkPlace.ACTFLG
            objWorkPlaceData.REMARK = objWorkPlace.REMARK
            objWorkPlaceData.PROVINCE_ID = objWorkPlace.PROVINCE_ID
            objWorkPlaceData.DISTRICT_ID = objWorkPlace.DISTRICT_ID
            objWorkPlaceData.WARD_ID = objWorkPlace.WARD_ID
            objWorkPlaceData.PLACE = objWorkPlace.PLACE
            objWorkPlaceData.REGION_ID = objWorkPlace.REGION_ID
            objWorkPlaceData.PHONE_NUMBER = objWorkPlace.PHONE_NUMBER
            objWorkPlaceData.FAX = objWorkPlace.FAX
            Context.HU_WORK_PLACE.AddObject(objWorkPlaceData)
            Context.SaveChanges(log)
            gID = objWorkPlaceData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateWorkPlace(ByVal _validate As WorkPlaceDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                query = (From p In Context.HU_WORK_PLACE
                         Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                         And p.ID <> _validate.ID).FirstOrDefault
                Return (query Is Nothing)
            Else
                query = (From p In Context.HU_TITLE
                         Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                         And p.ID = _validate.ID).FirstOrDefault
                Return (Not query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyWorkPlace(ByVal objWorkPlace As WorkPlaceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkPlaceData As HU_WORK_PLACE
        Try
            objWorkPlaceData = (From p In Context.HU_WORK_PLACE Where p.ID = objWorkPlace.ID).FirstOrDefault
            objWorkPlaceData.CODE = objWorkPlace.CODE
            objWorkPlaceData.NAME_VN = objWorkPlace.NAME_VN
            objWorkPlaceData.REMARK = objWorkPlace.REMARK
            objWorkPlaceData.PROVINCE_ID = objWorkPlace.PROVINCE_ID
            objWorkPlaceData.DISTRICT_ID = objWorkPlace.DISTRICT_ID
            objWorkPlaceData.WARD_ID = objWorkPlace.WARD_ID
            objWorkPlaceData.PLACE = objWorkPlace.PLACE
            objWorkPlaceData.REGION_ID = objWorkPlace.REGION_ID
            objWorkPlaceData.PHONE_NUMBER = objWorkPlace.PHONE_NUMBER
            objWorkPlaceData.FAX = objWorkPlace.FAX
            Context.SaveChanges(log)
            gID = objWorkPlaceData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveWorkPlace(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Dim lstWorkPlaceData As List(Of HU_WORK_PLACE)
        Try
            lstWorkPlaceData = (From p In Context.HU_WORK_PLACE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstWorkPlaceData.Count - 1
                lstWorkPlaceData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkPlace(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstWorkPlaceData As List(Of HU_WORK_PLACE)
        Try

            lstWorkPlaceData = (From p In Context.HU_WORK_PLACE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstWorkPlaceData.Count - 1
                Context.HU_WORK_PLACE.DeleteObject(lstWorkPlaceData(index))
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "Signer Setup"
    Public Function GetSingerSetups(ByVal _filter As SignerSetupDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable

        Try
            Using cls As New DataAccess.QueryData

                Dim strWhere As String = "WHERE 1=1"

                If _filter.EFFECT_DATE.HasValue Then
                    strWhere += " AND TO_DATE(A.EFFECT_DATE,'DD/MM/YYYY') >= TO_DATE('" + _filter.EFFECT_DATE.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')))"
                End If

                If Not String.IsNullOrEmpty(_filter.FUNC_NAME) Then
                    strWhere += " AND UPPER(A.FUNC_NAME) LIKE UPPER(N'%" + _filter.FUNC_NAME.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.SETUP_TYPE_NAME) Then
                    strWhere += " AND UPPER(A.SETUP_TYPE_NAME) LIKE UPPER(N'%" + _filter.SETUP_TYPE_NAME.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.SIGNER_NAME) Then
                    strWhere += " AND UPPER(A.SIGNER_NAME) LIKE UPPER(N'%" + _filter.SIGNER_NAME.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.SIGNER_TITLE_NAME) Then
                    strWhere += " AND UPPER(A.SIGNER_TITLE_NAME) LIKE UPPER(N'%" + _filter.SIGNER_TITLE_NAME.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.REMARK) Then
                    strWhere += " AND UPPER(A.REMARK) LIKE UPPER(N'%" + _filter.REMARK.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                    strWhere += " AND UPPER(A.ACTFLG) LIKE UPPER(N'%" + _filter.ACTFLG.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.BASE_AUTHOR) Then
                    strWhere += " AND UPPER(A.BASE_AUTHOR) LIKE UPPER(N'%" + _filter.BASE_AUTHOR.ToUpper.Trim + "%')"
                End If

                If Not String.IsNullOrEmpty(_filter.DEPUTY_AUTHOR) Then
                    strWhere += " AND UPPER(A.DEPUTY_AUTHOR) LIKE UPPER(N'%" + _filter.DEPUTY_AUTHOR.ToUpper.Trim + "%')"
                End If

                If _filter.AUTHOR_EFFECT_DATE.HasValue Then
                    strWhere += " AND TO_DATE(A.AUTHOR_EFFECT_DATE,'DD/MM/YYYY') >= TO_DATE('" + _filter.AUTHOR_EFFECT_DATE.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')))"
                End If

                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_SIGNER_SETUP",
                                               New With {.P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_WHERE_CONDITION = strWhere,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")

                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetListSignerSetup(ByVal _filter As SignerSetupDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of SignerSetupDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.HUV_SIGNER_SETUP
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New SignerSetupDTO With {
                            .ID = p.ID,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .FUNC_ID = p.FUNC_ID,
                            .FUNC_NAME = p.FUNC_NAME,
                            .SETUP_TYPE_ID = p.SETUP_TYPE,
                            .SETUP_TYPE_NAME = p.SETUP_TYPE_NAME,
                            .BASE_AUTHOR = p.BASE_AUTHOR,
                            .AUTHOR_EFFECT_DATE = p.AUTHOR_EFFECT_DATE,
                            .DEPUTY_AUTHOR = p.DEPUTY_AUTHOR,
                            .ORG_ID = p.ORG_ID,
                            .ORG_NAME = p.ORG_NAME,
                            .ORG_DESC = o.DESCRIPTION_PATH,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE,
                            .SIGNER_ID = p.SIGNER_ID,
                            .JOIN_DATE = p.JOIN_DATE,
                            .SIGNER_NAME = p.SIGNER_NAME,
                            .CER_BUS_RESG = p.CER_BUS_RESG,
                            .CER_BUS_RESG_EFFECT_DATE = p.CER_BUS_RESG_EFFECT_DATE,
                            .SIGNER_TITLE_ID = p.SIGNER_TITLE_ID,
                            .SIGNER_TITLE_NAME = p.SIGNER_TITLE_NAME,
                            .BASE1 = p.BASE1,
                            .BASE2 = p.BASE2,
                            .BASE3 = p.BASE3,
                            .BASE4 = p.BASE4,
                            .BASE5 = p.BASE5,
                            .BASE6 = p.BASE6,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}
            Dim lst = query
            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.FUNC_NAME) Then
                lst = lst.Where(Function(f) f.FUNC_NAME.ToUpper.Contains(_filter.FUNC_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.SETUP_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.SETUP_TYPE_NAME.ToUpper.Contains(_filter.SETUP_TYPE_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.SIGNER_NAME) Then
                lst = lst.Where(Function(f) f.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.SIGNER_TITLE_NAME) Then
                lst = lst.Where(Function(f) f.SIGNER_TITLE_NAME.ToUpper.Contains(_filter.SIGNER_TITLE_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.BASE_AUTHOR) Then
                lst = lst.Where(Function(f) f.BASE_AUTHOR.ToUpper.Contains(_filter.BASE_AUTHOR.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.DEPUTY_AUTHOR) Then
                lst = lst.Where(Function(f) f.DEPUTY_AUTHOR.ToUpper.Contains(_filter.DEPUTY_AUTHOR.ToUpper))
            End If

            If _filter.AUTHOR_EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.AUTHOR_EFFECT_DATE = _filter.AUTHOR_EFFECT_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertSignerSetup(ByVal objSignSet As SignerSetupDTO, ByVal log As UserLog, ByVal _lst_Type As List(Of Decimal)) As Boolean
        Dim iCount As Integer = 0
        Try
            If _lst_Type.Count > 0 Then
                For Each item In _lst_Type
                    Dim objSignSetData As New HU_SIGNER_SETUP
                    objSignSetData.ID = Utilities.GetNextSequence(Context, Context.HU_SIGNER_SETUP.EntitySet.Name)
                    objSignSetData.EFFECT_DATE = objSignSet.EFFECT_DATE
                    objSignSetData.FUNC_ID = objSignSet.FUNC_ID
                    objSignSetData.SIGNER_ID = objSignSet.SIGNER_ID
                    objSignSetData.REMARK = objSignSet.REMARK
                    objSignSetData.SETUP_TYPE = item
                    objSignSetData.BASE_AUTHOR = objSignSet.BASE_AUTHOR
                    objSignSetData.AUTHOR_EFFECT_DATE = objSignSet.AUTHOR_EFFECT_DATE
                    objSignSetData.DEPUTY_AUTHOR = objSignSet.DEPUTY_AUTHOR
                    objSignSetData.CER_BUS_RESG = objSignSet.CER_BUS_RESG
                    objSignSetData.ORG_ID = objSignSet.ORG_ID
                    objSignSetData.CER_BUS_RESG_EFFECT_DATE = objSignSet.CER_BUS_RESG_EFFECT_DATE
                    objSignSetData.ACTFLG = "A"
                    objSignSetData.SIGNER_TITLE = objSignSet.SIGNER_TITLE_NAME
                    objSignSetData.BASE1 = objSignSet.BASE1
                    objSignSetData.BASE2 = objSignSet.BASE2
                    objSignSetData.BASE3 = objSignSet.BASE3
                    objSignSetData.BASE4 = objSignSet.BASE4
                    objSignSetData.BASE5 = objSignSet.BASE5
                    objSignSetData.BASE6 = objSignSet.BASE6
                    Context.HU_SIGNER_SETUP.AddObject(objSignSetData)
                Next
            Else
                Dim objSignSetData As New HU_SIGNER_SETUP
                objSignSetData.ID = Utilities.GetNextSequence(Context, Context.HU_SIGNER_SETUP.EntitySet.Name)
                objSignSetData.EFFECT_DATE = objSignSet.EFFECT_DATE
                objSignSetData.FUNC_ID = objSignSet.FUNC_ID
                objSignSetData.SIGNER_ID = objSignSet.SIGNER_ID
                objSignSetData.REMARK = objSignSet.REMARK
                objSignSetData.BASE_AUTHOR = objSignSet.BASE_AUTHOR
                objSignSetData.AUTHOR_EFFECT_DATE = objSignSet.AUTHOR_EFFECT_DATE
                objSignSetData.DEPUTY_AUTHOR = objSignSet.DEPUTY_AUTHOR
                objSignSetData.ORG_ID = objSignSet.ORG_ID
                objSignSetData.ACTFLG = "A"
                objSignSetData.SIGNER_TITLE = objSignSet.SIGNER_TITLE_NAME
                objSignSetData.BASE1 = objSignSet.BASE1
                objSignSetData.BASE2 = objSignSet.BASE2
                objSignSetData.BASE3 = objSignSet.BASE3
                objSignSetData.BASE4 = objSignSet.BASE4
                objSignSetData.BASE5 = objSignSet.BASE5
                objSignSetData.BASE6 = objSignSet.BASE6
                Context.HU_SIGNER_SETUP.AddObject(objSignSetData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateSignSet(ByVal _validate As SignerSetupDTO) As Boolean
        Try
            Dim item As HU_SIGNER_SETUP
            If _validate.SETUP_TYPE_ID IsNot Nothing Then
                item = (From p In Context.HU_SIGNER_SETUP Where p.EFFECT_DATE = _validate.EFFECT_DATE AndAlso
                         p.FUNC_ID = _validate.FUNC_ID AndAlso p.SETUP_TYPE = _validate.SETUP_TYPE_ID AndAlso p.ORG_ID = _validate.ORG_ID And p.ID <> _validate.ID).FirstOrDefault
            Else
                item = (From p In Context.HU_SIGNER_SETUP Where p.EFFECT_DATE = _validate.EFFECT_DATE AndAlso
                         p.FUNC_ID = _validate.FUNC_ID AndAlso p.ORG_ID = _validate.ORG_ID And p.ID <> _validate.ID).FirstOrDefault
            End If
            If Not IsNothing(item) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifySignerSetup(ByVal objSignSet As SignerSetupDTO, ByVal log As UserLog, ByVal _lst_Type As List(Of Decimal)) As Boolean
        Try
            Dim obj = (From p In Context.HU_SIGNER_SETUP Where p.ID = objSignSet.ID).FirstOrDefault
            Context.HU_SIGNER_SETUP.DeleteObject(obj)
            If _lst_Type.Count > 0 Then
                For Each item In _lst_Type
                    Dim objSignSetData As New HU_SIGNER_SETUP
                    objSignSetData.ID = Utilities.GetNextSequence(Context, Context.HU_SIGNER_SETUP.EntitySet.Name)
                    objSignSetData.EFFECT_DATE = objSignSet.EFFECT_DATE
                    objSignSetData.FUNC_ID = objSignSet.FUNC_ID
                    objSignSetData.SIGNER_ID = objSignSet.SIGNER_ID
                    objSignSetData.REMARK = objSignSet.REMARK
                    objSignSetData.SETUP_TYPE = item
                    objSignSetData.BASE_AUTHOR = objSignSet.BASE_AUTHOR
                    objSignSetData.CER_BUS_RESG = objSignSet.CER_BUS_RESG
                    objSignSetData.CER_BUS_RESG_EFFECT_DATE = objSignSet.CER_BUS_RESG_EFFECT_DATE
                    objSignSetData.AUTHOR_EFFECT_DATE = objSignSet.AUTHOR_EFFECT_DATE
                    objSignSetData.DEPUTY_AUTHOR = objSignSet.DEPUTY_AUTHOR
                    objSignSetData.ORG_ID = objSignSet.ORG_ID
                    objSignSetData.ACTFLG = "A"
                    objSignSetData.SIGNER_TITLE = objSignSet.SIGNER_TITLE_NAME
                    objSignSetData.BASE1 = objSignSet.BASE1
                    objSignSetData.BASE2 = objSignSet.BASE2
                    objSignSetData.BASE3 = objSignSet.BASE3
                    objSignSetData.BASE4 = objSignSet.BASE4
                    objSignSetData.BASE5 = objSignSet.BASE5
                    objSignSetData.BASE6 = objSignSet.BASE6
                    Context.HU_SIGNER_SETUP.AddObject(objSignSetData)
                Next
            Else
                Dim objSignSetData As New HU_SIGNER_SETUP
                objSignSetData.ID = Utilities.GetNextSequence(Context, Context.HU_SIGNER_SETUP.EntitySet.Name)
                objSignSetData.EFFECT_DATE = objSignSet.EFFECT_DATE
                objSignSetData.FUNC_ID = objSignSet.FUNC_ID
                objSignSetData.SIGNER_ID = objSignSet.SIGNER_ID
                objSignSetData.REMARK = objSignSet.REMARK
                objSignSetData.BASE_AUTHOR = objSignSet.BASE_AUTHOR
                objSignSetData.AUTHOR_EFFECT_DATE = objSignSet.AUTHOR_EFFECT_DATE
                objSignSetData.CER_BUS_RESG = objSignSet.CER_BUS_RESG
                objSignSetData.CER_BUS_RESG_EFFECT_DATE = objSignSet.CER_BUS_RESG_EFFECT_DATE
                objSignSetData.DEPUTY_AUTHOR = objSignSet.DEPUTY_AUTHOR
                objSignSetData.ORG_ID = objSignSet.ORG_ID
                objSignSetData.ACTFLG = "A"
                objSignSetData.SIGNER_TITLE = objSignSet.SIGNER_TITLE_NAME
                objSignSetData.BASE1 = objSignSet.BASE1
                objSignSetData.BASE2 = objSignSet.BASE2
                objSignSetData.BASE3 = objSignSet.BASE3
                objSignSetData.BASE4 = objSignSet.BASE4
                objSignSetData.BASE5 = objSignSet.BASE5
                objSignSetData.BASE6 = objSignSet.BASE6
                Context.HU_SIGNER_SETUP.AddObject(objSignSetData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveSignerSetup(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Dim lstSignerSetup As List(Of HU_SIGNER_SETUP)
        Try
            lstSignerSetup = (From p In Context.HU_SIGNER_SETUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstSignerSetup.Count - 1
                lstSignerSetup(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteSignerSetup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstSignerSetup As List(Of HU_SIGNER_SETUP)
        Try

            lstSignerSetup = (From p In Context.HU_SIGNER_SETUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstSignerSetup.Count - 1
                Context.HU_SIGNER_SETUP.DeleteObject(lstSignerSetup(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


    Public Function GET_EXPORT_ORG_BARND() As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_COMMON_BUSINESS.GET_EXPORT_ORG_BARND",
                                           New With {.P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                     .P_CUR3 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function IMPORT_HU_ORG_BARND(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_BUSINESS.IMPORT_HU_ORG_BARND",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "event manage"
    Public Function GetEventManageByID(ByVal _id As Decimal) As EventManageDTO
        Try
            Dim obj = (From p In Context.PO_EVENT Where p.ID = _id
                       Select New EventManageDTO With {
                            .ID = p.ID,
                            .START_DATE = p.START_DATE,
                            .END_DATE = p.END_DATE,
                            .TITLE = p.TITLE,
                            .DETAIL = p.DETAIL,
                            .ATTACH_FILE = p.ATTACH_FILE,
                            .FILE_NAME = p.FILENAME,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                           .MODIFIED_DATE = p.MODIFIED_DATE,
                            .IS_SHOW = p.IS_SHOW
                        }).FirstOrDefault
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEventManageFileByte(ByVal _id As Decimal, ByVal _fileName As String) As Byte()
        Using rep As New ProfileRepository
            Dim dsFile As New Byte()
            Try
                Dim obj = rep.GetEventManageByID(_id)
                If obj Is Nothing Then
                    Return Nothing
                End If
                Dim target As String = AppDomain.CurrentDomain.BaseDirectory & "EventManage"
                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                End If
                Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & "Profile\UploadFile\PortalEvent\" & obj.ATTACH_FILE & "\" & _fileName
                Dim bytes = System.IO.File.ReadAllBytes(rootPath)
                Return bytes
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetEmpByEvent(ByVal _id As Decimal?) As List(Of EventEmpDTO)

        Try
            Dim query = From p In Context.PO_EVENT_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ot In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        Where p.PO_EVENT_ID = _id
                        Select New EventEmpDTO() With {
                       .ID = p.PO_EVENT_ID,
                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                       .ORG_NAME = o.NAME_VN,
                       .TITLE_NAME = ot.NAME_VN}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEventManage(ByVal _filter As EventManageDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of EventManageDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = _filter.USERNAME,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = False})
            End Using

            Dim query = From p In Context.PO_EVENT
                        From l In Context.HUV_PO_EVENT.Where(Function(f) f.PO_EVENT_ID = p.ID).DefaultIfEmpty
                        From s In Context.SE_USER.Where(Function(f) f.USERNAME.ToUpper = p.CREATED_BY.ToUpper).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = s.EMPLOYEE_ID).DefaultIfEmpty
                        From c In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper = _filter.USERNAME)
                        Select New EventManageDTO With {
                       .ID = p.ID,
                       .TITLE = p.TITLE,
                       .ADD_TIME = p.ADD_TIME,
                       .IS_SHOW = p.IS_SHOW,
                       .DETAIL = p.DETAIL,
                       .CREATED_DATE = p.CREATED_DATE,
                       .START_DATE = p.START_DATE,
                       .END_DATE = p.END_DATE,
                       .ATTACH_FILE = p.ATTACH_FILE,
                       .FILE_NAME = p.FILENAME,
                       .EMPLOYEE_CREATED = e.FULLNAME_VN,
                            .TEMPLATE_CODE = p.TEMPLATE_CODE,
                       .EMPIDs = l.LST_EMP,
                       .STATUS_NAME = If(p.IS_SHOW = True, "Đã gửi", "Chưa gửi")}
            Dim lst = query

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(p) p.START_DATE >= _filter.START_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(p) p.START_DATE <= _filter.END_DATE)
            End If
            If _filter.USERNAME.ToUpper <> "ADMIN" Then
                lst = lst.Where(Function(p) p.TEMPLATE_CODE Is Nothing Or p.TEMPLATE_CODE <> "ATTENDANCE_CHECK")
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertEventManage(ByVal objEventManage As EventManageDTO, ByVal log As UserLog) As Boolean
        Dim objEventManageData As New PO_EVENT
        Dim iCount As Integer = 0
        Try
            objEventManageData.ID = Utilities.GetNextSequence(Context, Context.PO_EVENT.EntitySet.Name)
            objEventManageData.IS_SHOW = 0
            objEventManageData.TITLE = objEventManage.TITLE
            objEventManageData.DETAIL = objEventManage.DETAIL
            objEventManageData.ADD_TIME = objEventManage.ADD_TIME
            objEventManageData.START_DATE = objEventManage.START_DATE
            objEventManageData.END_DATE = objEventManage.END_DATE
            objEventManageData.ATTACH_FILE = objEventManage.ATTACH_FILE
            objEventManageData.FILENAME = objEventManage.FILE_NAME
            Context.PO_EVENT.AddObject(objEventManageData)
            Context.SaveChanges(log)
            If objEventManage.EMPIDs IsNot Nothing Then
                Dim listemp = objEventManage.EMPIDs.Split(",").ToList
                For Each item In listemp
                    Dim objEventEmpData As New PO_EVENT_EMP
                    objEventEmpData.PO_EVENT_ID = objEventManageData.ID
                    objEventEmpData.EMPLOYEE_ID = item
                    Context.PO_EVENT_EMP.AddObject(objEventEmpData)
                    Context.SaveChanges(log)
                Next
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function ModifyEventManage(ByVal objEventManage As EventManageDTO, ByVal log As UserLog) As Boolean
        Dim objEventManageData As PO_EVENT
        Dim lstEventEmp As List(Of PO_EVENT_EMP)
        Try
            objEventManageData = (From p In Context.PO_EVENT Where p.ID = objEventManage.ID).FirstOrDefault
            objEventManageData.IS_SHOW = objEventManage.IS_SHOW
            objEventManageData.TITLE = objEventManage.TITLE
            objEventManageData.DETAIL = objEventManage.DETAIL
            objEventManageData.START_DATE = objEventManage.START_DATE
            objEventManageData.END_DATE = objEventManage.END_DATE
            objEventManageData.ATTACH_FILE = objEventManage.ATTACH_FILE
            objEventManageData.FILENAME = objEventManage.FILE_NAME


            lstEventEmp = (From p In Context.PO_EVENT_EMP Where p.PO_EVENT_ID = objEventManage.ID).ToList
            For index = 0 To lstEventEmp.Count - 1
                Context.PO_EVENT_EMP.DeleteObject(lstEventEmp(index))
            Next
            Context.SaveChanges(log)

            If objEventManage.EMPIDs IsNot Nothing Then
                Dim listemp = objEventManage.EMPIDs.Split(",").ToList
                For Each item In listemp
                    Dim objEventEmpData As New PO_EVENT_EMP
                    objEventEmpData.PO_EVENT_ID = objEventManageData.ID
                    objEventEmpData.EMPLOYEE_ID = item
                    Context.PO_EVENT_EMP.AddObject(objEventEmpData)
                    Context.SaveChanges(log)
                Next
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function DeleteEventManage(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstEventManage As List(Of PO_EVENT)
        Try

            lstEventManage = (From p In Context.PO_EVENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstEventManage.Count - 1
                Context.PO_EVENT.DeleteObject(lstEventManage(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ApplyEventManage(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstEventManage As List(Of PO_EVENT)
        Try
            Dim checkdate = (From p In Context.PO_EVENT Where lstID.Contains(p.ID) And (Date.Now < p.START_DATE Or p.END_DATE < Date.Now) Select p).Count
            If checkdate > 0 Then
                Return False
            End If

            lstEventManage = (From p In Context.PO_EVENT Where lstID.Contains(p.ID)).ToList
            Dim lstIDEventManage = (From p In Context.PO_EVENT Where lstID.Contains(p.ID) And p.TEMPLATE_CODE <> "ATTENDANCE_CHECK" Select p.ID).ToList
            Dim valueID As String = String.Join(",", lstIDEventManage)
            For index = 0 To lstEventManage.Count - 1
                lstEventManage(index).IS_SHOW = -1
                If lstEventManage(index).TEMPLATE_CODE <> "ATTENDANCE_CHECK" Then
                    Dim objSend As New PO_EVENT_SEND_MAIL
                    objSend.ID = Utilities.GetNextSequence(Context, Context.PO_EVENT_SEND_MAIL.EntitySet.Name)
                    objSend.EVENT_ID = lstEventManage(index).ID
                    objSend.ACTION_DATE = Date.Now
                    objSend.ACTION_STATUS = 0
                    Context.PO_EVENT_SEND_MAIL.AddObject(objSend)
                    Dim lstEventManageEmp = (From p In Context.PO_EVENT_EMP Where lstID.Contains(p.PO_EVENT_ID)).ToList

                    For i = 0 To lstEventManageEmp.Count - 1
                        lstEventManageEmp(index).IS_READ = 0
                    Next
                End If
            Next
            Context.SaveChanges()
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PROFILE_BUSINESS.SENDNOTIFICATION",
                                 New With {.P_LIST_ID = valueID})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function UnapplyEventManage(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstEventManage As List(Of PO_EVENT)
        Try
            lstEventManage = (From p In Context.PO_EVENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstEventManage.Count - 1
                lstEventManage(index).IS_SHOW = 0
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEventManageByte(ByVal _id As Decimal) As Byte()
        Using rep As New ProfileRepository
            Dim dsFile As New Byte()
            Try
                Dim obj = rep.GetEventManageByID(_id)

                Dim dsFileName = obj.FILE_NAME.Split(",")

                Dim target As String = AppDomain.CurrentDomain.BaseDirectory & "EventManage"

                If Not Directory.Exists(target) Then
                    Directory.CreateDirectory(target)
                End If

                Dim FILENAME As String = Format(Date.Now, "yyyyMMddHHmmss") '& ".zip"
                'Using zip As New ZipFile
                For Each item In dsFileName
                        Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & "Profile\UploadFile\PortalEvent\" & obj.ATTACH_FILE & "\" & item.ToString()

                        Dim file = New FileInfo(rootPath)
                        file.CopyTo(Path.Combine(target + "\" & item.ToString()), True)

                    'dsFileName.AddFile(target + "\" & item.ToString(), "")
                Next
                ''zip.Save(target & FILENAME)
                'End Using
                Dim bytes = System.IO.File.ReadAllBytes(target & FILENAME)
                ''File.Delete(target & FILENAME)
                Return bytes
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        End Using
    End Function
    Public Function PortalSendImageRelation(ByVal fileFor As String, ByVal userID As Decimal, ByVal id As Decimal, ByVal _imageBinary As Byte(), ByVal imageEx As String) As Boolean
        Try
            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                Dim strGuid = Guid.NewGuid().ToString()
                Dim strLink = "Profile\UploadFile\StaffFamily\" & strGuid & "\"
                Dim LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "Profile\UploadFile\StaffFamily\" & strGuid & "\"
                Dim userInf = (From u In Context.SE_USER Where u.ID = userID).FirstOrDefault()
                Dim employeeID = userInf.EMPLOYEE_ID
                Dim employeeCode = (From p In Context.HU_EMPLOYEE Where p.ID = employeeID Select p.EMPLOYEE_CODE).FirstOrDefault
                Dim imageName = employeeCode & "_" & Date.Now.ToString("yyyyMMddHHmmss") & "." & imageEx


                If fileFor = "WORKING_BEFORE" Then
                    Dim objEmpEditData = (From p In Context.HU_WORKING_BEFORE_EDIT Where p.ID = id).FirstOrDefault()

                    If objEmpEditData.FILE_NAME Is Nothing Then
                        If Not Directory.Exists(LinkDetail.Trim) Then
                            Directory.CreateDirectory(LinkDetail.Trim)
                        End If

                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                        ms.WriteTo(fs)
                        ' clean up
                        ms.Close()

                        fs.Close()
                        fs.Dispose()

                        objEmpEditData.FILE_NAME = strGuid

                        Dim fileupload As FileUploadDTO = New FileUploadDTO
                        fileupload.NAME = strGuid
                        fileupload.LINK = strLink.Trim
                        fileupload.FILE_NAME = imageName
                        AddFileUpload(fileupload)
                    Else
                        Dim f_name = objEmpEditData.FILE_NAME

                        Dim objUserFile = (From p In Context.HU_USERFILES Where p.NAME = f_name).FirstOrDefault()

                        If objUserFile IsNot Nothing AndAlso objUserFile.LINK IsNot Nothing AndAlso objUserFile.LINK <> "" Then
                            strLink = objUserFile.LINK
                            LinkDetail = AppDomain.CurrentDomain.BaseDirectory & objUserFile.LINK
                            strGuid = objUserFile.NAME
                        End If

                        If Not Directory.Exists(LinkDetail.Trim) Then
                            Directory.CreateDirectory(LinkDetail.Trim)
                        End If
                        'Xóa ảnh cũ của nhân viên.
                        Try
                            Dim sFile() As String = Directory.GetFiles(LinkDetail.Trim, objUserFile.FILE_NAME)
                            If sFile.Length > 0 Then
                                For Each s In sFile
                                    File.Delete(s)
                                Next
                            End If
                        Catch ex As Exception
                        End Try
                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                        ms.WriteTo(fs)
                        ' clean up
                        ms.Close()

                        fs.Close()
                        fs.Dispose()

                        objEmpEditData.FILE_NAME = strGuid

                        objUserFile.FILE_NAME = imageName

                        Context.SaveChanges()
                    End If
                ElseIf fileFor = "EMP_CERTIFICATE" Then
                    Dim objEmpEditData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where p.ID = id).FirstOrDefault()

                    If objEmpEditData.FILE_NAME Is Nothing Then
                        If Not Directory.Exists(LinkDetail.Trim) Then
                            Directory.CreateDirectory(LinkDetail.Trim)
                        End If

                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                        ms.WriteTo(fs)
                        ' clean up
                        ms.Close()

                        fs.Close()
                        fs.Dispose()

                        objEmpEditData.FILE_NAME = strGuid

                        Dim fileupload As FileUploadDTO = New FileUploadDTO
                        fileupload.NAME = strGuid
                        fileupload.LINK = strLink.Trim
                        fileupload.FILE_NAME = imageName
                        AddFileUpload(fileupload)
                    Else
                        Dim f_name = objEmpEditData.FILE_NAME

                        Dim objUserFile = (From p In Context.HU_USERFILES Where p.NAME = f_name).FirstOrDefault()

                        If objUserFile IsNot Nothing AndAlso objUserFile.LINK IsNot Nothing AndAlso objUserFile.LINK <> "" Then
                            strLink = objUserFile.LINK
                            LinkDetail = AppDomain.CurrentDomain.BaseDirectory & objUserFile.LINK
                            strGuid = objUserFile.NAME
                        End If

                        If Not Directory.Exists(LinkDetail.Trim) Then
                            Directory.CreateDirectory(LinkDetail.Trim)
                        End If
                        'Xóa ảnh cũ của nhân viên.
                        Try
                            Dim sFile() As String = Directory.GetFiles(LinkDetail.Trim, objUserFile.FILE_NAME)
                            If sFile.Length > 0 Then
                                For Each s In sFile
                                    File.Delete(s)
                                Next
                            End If
                        Catch ex As Exception
                        End Try
                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                        ms.WriteTo(fs)
                        ' clean up
                        ms.Close()

                        fs.Close()
                        fs.Dispose()

                        objEmpEditData.FILE_NAME = strGuid

                        objUserFile.FILE_NAME = imageName

                        Context.SaveChanges()
                    End If
                Else
                    Dim objEmpEditData = (From p In Context.HU_FAMILY_EDIT Where p.ID = id).FirstOrDefault()

                    If (fileFor = "NPT" And objEmpEditData.FILE_NPT Is Nothing) Or (fileFor <> "NPT" And objEmpEditData.FILE_FAMILY Is Nothing) Then
                        If Not Directory.Exists(LinkDetail.Trim) Then
                            Directory.CreateDirectory(LinkDetail.Trim)
                        End If


                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                        ms.WriteTo(fs)
                        ' clean up
                        ms.Close()

                        fs.Close()
                        fs.Dispose()

                        If fileFor = "NPT" Then
                            objEmpEditData.FILE_NPT = strGuid
                        Else
                            objEmpEditData.FILE_FAMILY = strGuid
                        End If

                        Dim fileupload As FileUploadDTO = New FileUploadDTO
                        fileupload.NAME = strGuid
                        fileupload.LINK = strLink.Trim
                        fileupload.FILE_NAME = imageName
                        AddFileUpload(fileupload)
                    Else
                        Dim f_name = If(fileFor = "NPT", objEmpEditData.FILE_NPT, objEmpEditData.FILE_FAMILY)

                        Dim objUserFile = (From p In Context.HU_USERFILES Where p.NAME = f_name).FirstOrDefault()


                        If objUserFile IsNot Nothing AndAlso objUserFile.LINK IsNot Nothing AndAlso objUserFile.LINK <> "" Then
                            strLink = objUserFile.LINK
                            LinkDetail = AppDomain.CurrentDomain.BaseDirectory & objUserFile.LINK
                            strGuid = objUserFile.NAME
                        End If

                        If Not Directory.Exists(LinkDetail.Trim) Then
                            Directory.CreateDirectory(LinkDetail.Trim)
                        End If
                        'Xóa ảnh cũ của nhân viên.
                        Try
                            Dim sFile() As String = Directory.GetFiles(LinkDetail.Trim, objUserFile.FILE_NAME)
                            If sFile.Length > 0 Then
                                For Each s In sFile
                                    File.Delete(s)
                                Next
                            End If
                        Catch ex As Exception
                        End Try
                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                        ms.WriteTo(fs)
                        ' clean up
                        ms.Close()

                        fs.Close()
                        fs.Dispose()

                        If fileFor = "NPT" Then
                            objEmpEditData.FILE_NPT = strGuid
                        Else
                            objEmpEditData.FILE_FAMILY = strGuid
                        End If


                        objUserFile.FILE_NAME = imageName


                        Context.SaveChanges()
                    End If

                End If
                Return True

            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function PortalSendImage_mb(ByVal employeeCode As String, ByVal userID As Decimal, ByVal _imageBinary As Byte(), ByVal imageEx As String, ByVal IsEdit As Decimal) As Boolean
        Try
            WriteExceptionLog(New Exception, MethodBase.GetCurrentMethod.Name, "PortalSendImage_mb _mobile _ " & employeeCode & " _ " & IsEdit)
            If IsEdit = 1 Then 'edit
                If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                    Dim strGuid = Guid.NewGuid().ToString()
                    Dim strLink = "Profile\UploadFile\StaffProfile\" & strGuid & "\"
                    Dim LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "Profile\UploadFile\StaffProfile\" & strGuid & "\"
                    Dim userInf = (From u In Context.SE_USER Where u.ID = userID).FirstOrDefault()
                    Dim employeeID = userInf.EMPLOYEE_ID
                    Dim objEmpEditData = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = employeeID And p.STATUS <> 2).FirstOrDefault()
                    Dim objUserFile = (From p In Context.HU_USERFILES Where p.NAME = objEmpEditData.IMAGE).FirstOrDefault()
                    ' với mobile truyền user vào employeeID
                    If employeeCode = "" Then
                        employeeCode = (From p In Context.HU_EMPLOYEE Where p.ID = employeeID).FirstOrDefault().EMPLOYEE_CODE
                    End If
                    If objUserFile IsNot Nothing AndAlso objUserFile.LINK IsNot Nothing AndAlso objUserFile.LINK <> "" Then
                        strLink = objUserFile.LINK
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & objUserFile.LINK
                        strGuid = objUserFile.NAME
                    End If
                    'Dim savepath = ""
                    ''Dim savepath_host = ""
                    Dim imageName = employeeCode & "_" & Date.Now.ToString("yyyyMMddHHmmss") & "." & imageEx
                    'savepath = AppDomain.CurrentDomain.BaseDirectory & "EmployeeImageEdit"

                    If Not Directory.Exists(LinkDetail.Trim) Then
                        Directory.CreateDirectory(LinkDetail.Trim)
                    End If
                    'Xóa ảnh cũ của nhân viên.
                    Try
                        Dim sFile() As String = Directory.GetFiles(LinkDetail.Trim, objUserFile.FILE_NAME)
                        If sFile.Length > 0 Then
                            For Each s In sFile
                                File.Delete(s)
                            Next
                        End If
                    Catch ex As Exception
                    End Try
                    Dim ms As New MemoryStream(_imageBinary)
                    ' đọc lại từ base lưu sang file vật lý
                    Dim fs As New FileStream(LinkDetail.Trim & imageName, FileMode.Create)
                    ms.WriteTo(fs)
                    ' clean up
                    ms.Close()

                    fs.Close()
                    fs.Dispose()

                    objEmpEditData.IMAGE = strGuid
                    If objUserFile Is Nothing Then
                        Dim fileupload As FileUploadDTO = New FileUploadDTO
                        fileupload.NAME = strGuid
                        fileupload.LINK = strLink.Trim
                        fileupload.FILE_NAME = imageName
                        AddFileUpload(fileupload)
                    Else
                        objUserFile.FILE_NAME = imageName
                    End If

                    Context.SaveChanges()


                    Return True

                End If

            Else
                If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                    Dim userInf = (From u In Context.SE_USER Where u.ID = userID).FirstOrDefault()
                    Dim employeeID = userInf.EMPLOYEE_ID
                    Dim objEmpCVData = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = employeeID).FirstOrDefault()
                    ' với mobile truyền user vào employeeID
                    If employeeCode = "" Then
                        employeeCode = (From p In Context.HU_EMPLOYEE Where p.ID = employeeID).FirstOrDefault().EMPLOYEE_CODE
                    End If
                    Dim savepath = ""
                    Dim savepath_app = ""
                    'Dim savepath_host = ""
                    Dim imageName = employeeCode & "." & imageEx
                    savepath = AppDomain.CurrentDomain.BaseDirectory & "EmployeeImage"
                    savepath_app = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP_APP" Select P.VALUE).FirstOrDefault
                    'savepath_host = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP_HOST" Select P.VALUE).FirstOrDefault

                    If Not Directory.Exists(savepath) Then
                        Directory.CreateDirectory(savepath)
                    End If
                    If Not Directory.Exists(savepath_app) Then
                        Directory.CreateDirectory(savepath_app)
                    End If
                    'If Not Directory.Exists(savepath_host) Then
                    '    Directory.CreateDirectory(savepath_host)
                    'End If
                    'Xóa ảnh cũ của nhân viên.
                    Try
                        Dim sFile() As String = Directory.GetFiles(savepath, objEmpCVData.IMAGE)
                        Dim sFile_app() As String = Directory.GetFiles(savepath_app, objEmpCVData.IMAGE)
                        'Dim sFile_host() As String = Directory.GetFiles(savepath_host, objEmpCVData.IMAGE)
                        If sFile.Length > 0 Then
                            For Each s In sFile
                                File.Delete(s)
                            Next
                        End If
                        If sFile_app.Length > 0 Then
                            For Each s In sFile_app
                                File.Delete(s)
                            Next
                        End If
                        'If sFile_host.Length > 0 Then
                        '    For Each s In sFile_host
                        '        File.Delete(s)
                        '    Next
                        'End If
                    Catch ex As Exception

                    End Try



                    Dim ms As New MemoryStream(_imageBinary)
                    ' đọc lại từ base lưu sang file vật lý
                    ' mệt vl
                    Dim fs As New FileStream(savepath & "\" & imageName, FileMode.Create)
                    Dim fs_app As New FileStream(savepath_app & "\" & imageName, FileMode.Create)
                    'Dim fs_host As New FileStream(savepath_host & "\" & imageName, FileMode.Create)
                    ms.WriteTo(fs)
                    ms.WriteTo(fs_app)
                    'ms.WriteTo(fs_host)

                    ' clean up
                    ms.Close()

                    fs.Close()
                    fs.Dispose()

                    fs_app.Close()
                    fs_app.Dispose()

                    'fs_host.Close()
                    'fs_host.Dispose()

                    objEmpCVData.IMAGE = imageName
                    Context.SaveChanges()

                    Return True
                End If
            End If


        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function PersonRelationImage_mb(ByVal userId As Decimal, ByRef sError As String, ByVal id As Decimal, ByVal tab As Decimal, ByVal fileFor As String) As Byte()
        Try
            Dim __imageBinary() As Byte = Nothing
            Dim LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImageUploadfile.png"
            Dim __fileInfo As IO.FileInfo
            Dim userInf = (From u In Context.SE_USER Where u.ID = userId).FirstOrDefault()
            Dim _employeeID = userInf.EMPLOYEE_ID

            If id = 0 Then
                __fileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImageUploadfile.png") 'Nếu ko có thì lấy ảnh mặc định

                Dim _FStream As New IO.FileStream(__fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
                Dim _NumBytes As Long = __fileInfo.Length
                Dim _BinaryReader As New IO.BinaryReader(_FStream)
                __imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                __fileInfo = Nothing
                _NumBytes = 0
                _FStream.Close()
                _FStream.Dispose()
                _BinaryReader.Close()
                Return __imageBinary
            Else

                If fileFor = "WORKING_BEFORE" Then
                    If tab = 1 Then
                        Dim userFiles = (From p In Context.HU_WORKING_BEFORE
                                         From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                                         Where p.ID = id
                                         Select New FileUploadDTO With {
                                        .LINK = u.LINK,
                                        .FILE_NAME = u.FILE_NAME
                                    }).FirstOrDefault
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
                    Else
                        Dim userFiles = (From p In Context.HU_WORKING_BEFORE_EDIT
                                         From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                                         Where p.ID = id
                                         Select New FileUploadDTO With {
                                        .LINK = u.LINK,
                                        .FILE_NAME = u.FILE_NAME
                                    }).FirstOrDefault
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME

                    End If
                ElseIf fileFor = "EMP_CERTIFICATE" Then
                    If tab = 1 Then
                        Dim userFiles = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY
                                         From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                                         Where p.ID = id
                                         Select New FileUploadDTO With {
                                            .LINK = u.LINK,
                                            .FILE_NAME = u.FILE_NAME
                                        }).FirstOrDefault
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
                    Else
                        Dim userFiles = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                                         From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                                         Where p.ID = id
                                         Select New FileUploadDTO With {
                                            .LINK = u.LINK,
                                            .FILE_NAME = u.FILE_NAME
                                        }).FirstOrDefault
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME

                    End If
                Else
                    If tab = 1 Then
                        Dim userFiles = (From p In Context.HU_FAMILY
                                         From u In Context.HU_USERFILES.Where(Function(f) f.NAME = If(fileFor = "NPT", p.FILE_NPT, p.FILE_FAMILY)).DefaultIfEmpty
                                         Where p.ID = id
                                         Select New FileUploadDTO With {
                                        .LINK = u.LINK,
                                        .FILE_NAME = u.FILE_NAME
                                    }).FirstOrDefault
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
                    Else
                        Dim userFiles = (From p In Context.HU_FAMILY_EDIT
                                         From u In Context.HU_USERFILES.Where(Function(f) f.NAME = If(fileFor = "NPT", p.FILE_NPT, p.FILE_FAMILY)).DefaultIfEmpty
                                         Where p.ID = id
                                         Select New FileUploadDTO With {
                                        .LINK = u.LINK,
                                        .FILE_NAME = u.FILE_NAME
                                    }).FirstOrDefault
                        LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME

                    End If
                End If




                If File.Exists(LinkDetail) Then
                    __fileInfo = New FileInfo(LinkDetail)
                Else
                    __fileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImageUploadfile.png") 'Nếu ko có thì lấy ảnh mặc định
                End If
                Dim _FStream As New IO.FileStream(__fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
                Dim _NumBytes As Long = __fileInfo.Length
                Dim _BinaryReader As New IO.BinaryReader(_FStream)
                __imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                __fileInfo = Nothing
                _NumBytes = 0
                _FStream.Close()
                _FStream.Dispose()
                _BinaryReader.Close()
                Return __imageBinary
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            sError = ex.ToString
        End Try
    End Function

    Public Function Profile_mb(ByVal userId As Decimal, ByRef sError As String, ByVal id As Decimal, ByVal tab As Decimal, ByVal fileFor As String) As Byte()
        Try
            Dim __imageBinary() As Byte = Nothing
            Dim LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoimgCMND.png"
            Dim __fileInfo As IO.FileInfo
            Dim userInf = (From u In Context.SE_USER Where u.ID = userId).FirstOrDefault()
            Dim _employeeID = userInf.EMPLOYEE_ID
            Dim existEdit = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = _employeeID And p.STATUS <> 2).Any
            If fileFor = "CMND" Then
                If existEdit Then
                    Dim userFiles = (From p In Context.HU_EMPLOYEE_EDIT
                                     From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_CMND).DefaultIfEmpty
                                     Where p.EMPLOYEE_ID = _employeeID And p.STATUS <> 2
                                     Select New FileUploadDTO With {
                                        .LINK = u.LINK,
                                        .FILE_NAME = u.FILE_NAME
                                    }).FirstOrDefault
                    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
                End If

                'Else
                '    Dim empCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = _employeeID
                '                 Select New EmployeeCVDTO With {
                '                     .IMAGE = p.IMAGE
                '                }).FirstOrDefault
                '    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\" & empCV.IMAGE
                'End If
            ElseIf fileFor = "CMNDBACK" Then
                If existEdit Then
                    Dim userFiles = (From p In Context.HU_EMPLOYEE_EDIT
                                     From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_CMND_BACK).DefaultIfEmpty
                                     Where p.EMPLOYEE_ID = _employeeID And p.STATUS <> 2
                                     Select New FileUploadDTO With {
                                        .LINK = u.LINK,
                                        .FILE_NAME = u.FILE_NAME
                                    }).FirstOrDefault
                    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
                End If

                'Else
                '    Dim empCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = _employeeID
                '                 Select New EmployeeCVDTO With {
                '                     .IMAGE = p.IMAGE
                '                }).FirstOrDefault
                '    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\" & empCV.IMAGE
                'End If

            End If


                If File.Exists(LinkDetail) Then
                __fileInfo = New FileInfo(LinkDetail)
            Else
                __fileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoimgCMND.png") 'Nếu ko có thì lấy ảnh mặc định
            End If
            Dim _FStream As New IO.FileStream(__fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _NumBytes As Long = __fileInfo.Length
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            __imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            __fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return __imageBinary

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            sError = ex.ToString
        End Try
    End Function

    Public Function EmployeeImage_mb(ByVal userId As Decimal, ByRef sError As String, ByVal IsEdit As Decimal,
                                     Optional ByVal isOneEmployee As Boolean = True,
                                     Optional ByVal img_link As String = "") As Byte()
        Try
            'WriteExceptionLog(New Exception, MethodBase.GetCurrentMethod.Name, "EmployeeImage_mb _mobile _ " & IsEdit & " _ " & IsEdit)
            If IsEdit = 1 Then 'edit
                Dim __imageBinary() As Byte = Nothing
                Dim LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImage.jpg"
                Dim __fileInfo As IO.FileInfo
                Dim userInf = (From u In Context.SE_USER Where u.ID = userId).FirstOrDefault()
                Dim _employeeID = userInf.EMPLOYEE_ID
                'Dim objEmpEditData = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = _employeeID And p.STATUS <> 2).FirstOrDefault()
                'Dim objUserFile = (From p In Context.HU_USERFILES Where p.NAME = objEmpEditData.IMAGE).FirstOrDefault()
                'If objUserFile IsNot Nothing AndAlso objUserFile.LINK IsNot Nothing AndAlso objUserFile.LINK <> "" Then
                '    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & objUserFile.LINK & objUserFile.FILE_NAME
                'End If
                Dim existEdit = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = _employeeID And p.STATUS <> 2).Any
                If existEdit Then
                    Dim userFiles = (From p In Context.HU_EMPLOYEE_EDIT
                                     From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.IMAGE).DefaultIfEmpty
                                     Where p.EMPLOYEE_ID = _employeeID And p.STATUS <> 2
                                     Select New FileUploadDTO With {
                                    .LINK = u.LINK,
                                    .FILE_NAME = u.FILE_NAME
                                }).FirstOrDefault
                    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
                Else
                    Dim empCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = _employeeID
                                 Select New EmployeeCVDTO With {
                                 .IMAGE = p.IMAGE
                            }).FirstOrDefault
                    LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\" & empCV.IMAGE
                End If
                If File.Exists(LinkDetail) Then
                    __fileInfo = New FileInfo(LinkDetail)
                Else
                    __fileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImage.jpg") 'Nếu ko có thì lấy ảnh mặc định
                End If
                Dim _FStream As New IO.FileStream(__fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
                Dim _NumBytes As Long = __fileInfo.Length
                Dim _BinaryReader As New IO.BinaryReader(_FStream)
                __imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                __fileInfo = Nothing
                _NumBytes = 0
                _FStream.Close()
                _FStream.Dispose()
                _BinaryReader.Close()
                Return __imageBinary
            Else
                Dim employeeId As String = (From p In Context.SE_USER Where p.ID = userId Select p.EMPLOYEE_ID).FirstOrDefault()

                Dim sEmployeeImage As String = ""
                If Not isOneEmployee Then
                    sEmployeeImage = img_link
                Else
                    sEmployeeImage = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = employeeId
                                      Select p.IMAGE).FirstOrDefault
                End If

                Dim _imageBinary() As Byte = Nothing
                Dim fileDirectory = ""
                Dim fileDirectoryApp = ""
                Dim filepath = ""
                Dim filepathApp = ""
                Dim filepathDefault = ""

                'fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
                fileDirectory = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP" Select P.VALUE).FirstOrDefault
                fileDirectoryApp = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP_APP" Select P.VALUE).FirstOrDefault
                Dim _fileInfo As IO.FileInfo
                If sEmployeeImage IsNot Nothing AndAlso sEmployeeImage.Trim().Length > 0 Then
                    filepath = fileDirectory & "\" & sEmployeeImage
                    filepathApp = fileDirectoryApp & "\" & sEmployeeImage
                Else
                    filepath = fileDirectory & "\NoImage.jpg"
                    filepathApp = fileDirectoryApp & "\NoImage.jpg"
                End If
                filepathDefault = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImage.jpg"
                'Kiểm tra file có tồn tại ko
                If File.Exists(filepath) Then
                    _fileInfo = New FileInfo(filepath)
                    If Not File.Exists(filepathApp) Then
                        If Not Directory.Exists(fileDirectoryApp) Then
                            Directory.CreateDirectory(fileDirectoryApp)
                        End If
                        My.Computer.FileSystem.CopyFile(filepath, filepathApp)
                    End If
                Else
                    _fileInfo = New FileInfo(filepathDefault) 'Nếu ko có thì lấy ảnh mặc định
                End If

                Dim _FStream As New IO.FileStream(_fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
                Dim _NumBytes As Long = _fileInfo.Length
                Dim _BinaryReader As New IO.BinaryReader(_FStream)
                _imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                _fileInfo = Nothing
                _NumBytes = 0
                _FStream.Close()
                _FStream.Dispose()
                _BinaryReader.Close()
                Return _imageBinary
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            sError = ex.ToString
        End Try
    End Function

#End Region
#Region "OM"
    Public Function GetOrgTreeList(ByVal username As String, ByVal sACT As String) As List(Of OrganizationDTO)
        Dim query As ObjectQuery(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = username.ToUpper).FirstOrDefault
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = username.ToUpper,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = True})
            End Using

            If u IsNot Nothing Then
                Dim query1 = (From p In Context.SE_GROUP Where p.IS_ADMIN <> 0 Select p.ID).FirstOrDefault
                If query1 = Nothing Then
                    Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList
                    Dim query_org_acc = (From org In Context.HU_ORGANIZATION
                                         From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = org.ID And f.USERNAME.ToUpper = username.ToUpper).DefaultIfEmpty
                                         Where (From user In Context.SE_USER_ORG_ACCESS
                                                Where user.USER_ID = u.ID
                                                Select user.ORG_ID).Contains(org.ID)
                                         Select New OrganizationDTO With {
                                .ID = org.ID,
                                .CODE = org.CODE,
                                .NAME_VN = org.NAME_VN,
                                .NAME_EN = org.NAME_EN,
                                .PARENT_ID = org.PARENT_ID,
                                .ORD_NO = org.ORD_NO,
                                .ACTFLG = org.ACTFLG,
                                .STATUS_ID = 0,
                                .DISSOLVE_DATE = org.DISSOLVE_DATE,
                                .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                                .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                                .FOUNDATION_DATE = org.FOUNDATION_DATE}).Distinct

                    lstOrgs = query_org_acc.ToList
                    If lstOrgs.Count > 0 AndAlso (From p In lstOrgs Where p.HIERARCHICAL_PATH = "1").Count = 0 Then
                        Dim lstOrgPer = lstOrgs.Select(Function(f) f.ID).ToList
                        Dim lstOrgID As New List(Of Decimal)
                        For Each org In lstOrgs
                            lstOrgID.Add(org.ID)
                            If org.HIERARCHICAL_PATH <> "" Then
                                If org.HIERARCHICAL_PATH.Split(";").Length > 1 Then
                                    For i As Integer = 0 To org.HIERARCHICAL_PATH.Split(";").Length - 2
                                        Dim str = org.HIERARCHICAL_PATH.Split(";")(i)
                                        If str <> "" Then
                                            Dim orgid = Decimal.Parse(str)
                                            If Not lstOrgPer.Contains(orgid) And Not lstOrgID.Contains(orgid) Then
                                                lstOrgID.Add(orgid)
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        Next

                        If sACT = "" Then
                            query = (From p In Context.HU_ORGANIZATION
                                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty()
                                     From v In Context.HU_EMPLOYEE_CV.Where(Function(b) b.EMPLOYEE_ID = e.ID).DefaultIfEmpty()
                                     From t In Context.HU_TITLE.Where(Function(a) a.ID = e.TITLE_ID).DefaultIfEmpty()
                                     From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.STATUS_ID).DefaultIfEmpty()
                                     From ot In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.ORG_LEVEL).DefaultIfEmpty()
                                     From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ID And f.USERNAME.ToUpper = username.ToUpper).DefaultIfEmpty()
                                     Order By p.ORD_NO, p.CODE, p.NAME_VN
                                     Where lstOrgID.Contains(p.ID)
                                     Select New OrganizationDTO With {.ID = p.ID,
                                                                      .CODE = p.CODE,
                                                                      .NAME_VN = p.NAME_VN,
                                                                      .NAME_EN = p.NAME_EN,
                                                                      .PARENT_ID = p.PARENT_ID,
                                                                      .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                                      .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                                      .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                                      .ORD_NO = p.ORD_NO,
                                                                      .TITLE_NAME = t.CODE & " - " & t.NAME_VN,
                                                                      .NLEVEL = p.NLEVEL,
                                                                      .ORG_LEVEL = p.ORG_LEVEL,
                                                                      .ORG_LEVEL_NAME = ot.NAME_VN,
                                                                      .GROUPPROJECT = If(p.GROUPPROJECT Is Nothing, 0, p.GROUPPROJECT),
                                                                      .EFFECT_DATE = p.EFFECT_DATE,
                                                                      .MOBILE = p.MOBILE,
                                                                      .PROVINCE_NAME = p.PROVINCE_NAME,
                                                                      .PIT_NO = p.PIT_NO,
                                                                      .ADDRESS = p.ADDRESS,
                                                                      .UY_BAN = p.UY_BAN,
                                                                      .HIERARCHICAL_PATH = p.HIERARCHICAL_PATH,
                                                                  .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                                                      .ACTFLG = If(p.ACTFLG = "A", "Sử dụng", "Ngừng sử dụng"),
                                                                      .STATUS_NAME = If(o.NAME_VN = "Mới khởi tạo", "", "disable-inupt"),
                                                                      .COLOR = If(p.ACTFLG = "I", "#969696", "#070101"),
                                                                      .IMAGE = v.IMAGE,
                                         .FILENAME = p.FILENAME,
                                         .ATTACH_FILE = p.ATTACH_FILE})
                        Else
                            query = (From p In Context.HU_ORGANIZATION
                                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty
                                     From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.STATUS_ID).DefaultIfEmpty()
                                     From ot In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.ORG_LEVEL).DefaultIfEmpty()
                                     From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ID And f.USERNAME.ToUpper = username.ToUpper).DefaultIfEmpty
                                     Where p.ACTFLG = sACT And lstOrgID.Contains(p.ID)
                                     Order By p.ORD_NO, p.CODE, p.NAME_VN
                                     Select New OrganizationDTO With {.ID = p.ID,
                                                                  .CODE = p.CODE,
                                                                  .NAME_VN = p.NAME_VN,
                                                                  .NAME_EN = p.NAME_EN,
                                                                  .PARENT_ID = p.PARENT_ID,
                                                                  .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                                  .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                                  .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                                  .NLEVEL = p.NLEVEL,
                                                                  .UY_BAN = p.UY_BAN,
                                                                  .GROUPPROJECT = If(p.GROUPPROJECT Is Nothing, 0, p.GROUPPROJECT),
                                                                  .EFFECT_DATE = p.EFFECT_DATE,
                                                                  .ACTFLG = If(p.ACTFLG = "A", "Sử dụng", "Ngừng sử dụng"),
                                                                  .STATUS_NAME = If(o.NAME_VN = "Mới khởi tạo", "", "disable-inupt"),
                                                                  .COLOR = If(p.ACTFLG = "I", "#969696", "#070101"),
                                                                  .ORD_NO = p.ORD_NO,
                                     .ORG_LEVEL = p.ORG_LEVEL,
                                                                      .ORG_LEVEL_NAME = ot.NAME_VN,
                                     .FILENAME = p.FILENAME,
                                     .ATTACH_FILE = p.ATTACH_FILE})
                        End If
                    End If
                Else
                    If sACT = "" Then
                        query = (From p In Context.HU_ORGANIZATION
                                 From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty()
                                 From v In Context.HU_EMPLOYEE_CV.Where(Function(b) b.EMPLOYEE_ID = e.ID).DefaultIfEmpty()
                                 From t In Context.HU_TITLE.Where(Function(a) a.ID = e.TITLE_ID).DefaultIfEmpty()
                                 From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.STATUS_ID).DefaultIfEmpty()
                                 From ot In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.ORG_LEVEL).DefaultIfEmpty()
                                 From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ID And f.USERNAME.ToUpper = username.ToUpper).DefaultIfEmpty
                                 Order By p.ORD_NO, p.CODE, p.NAME_VN
                                 Select New OrganizationDTO With {.ID = p.ID,
                                                                  .CODE = p.CODE,
                                                                  .NAME_VN = p.NAME_VN,
                                                                  .NAME_EN = p.NAME_EN,
                                                                  .PARENT_ID = p.PARENT_ID,
                                                                  .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                                  .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                                  .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                                  .ORD_NO = p.ORD_NO,
                                                                  .TITLE_NAME = t.CODE & " - " & t.NAME_VN,
                                                                  .NLEVEL = p.NLEVEL,
                                                                  .GROUPPROJECT = If(p.GROUPPROJECT Is Nothing, 0, p.GROUPPROJECT),
                                                                  .EFFECT_DATE = p.EFFECT_DATE,
                                                                  .MOBILE = p.MOBILE,
                                                                  .PROVINCE_NAME = p.PROVINCE_NAME,
                                                                  .PIT_NO = p.PIT_NO,
                                                                  .ADDRESS = p.ADDRESS,
                                                                  .UY_BAN = p.UY_BAN,
                                                                  .HIERARCHICAL_PATH = p.HIERARCHICAL_PATH,
                                                              .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                                                  .ACTFLG = If(p.ACTFLG = "A", "Sử dụng", "Ngừng sử dụng"),
                                                                  .STATUS_NAME = If(o.NAME_VN = "Mới khởi tạo", "", "disable-inupt"),
                                                                  .COLOR = If(p.ACTFLG = "I", "#969696", "#070101"),
                                                                  .IMAGE = v.IMAGE,
                                     .ORG_LEVEL = p.ORG_LEVEL,
                                                                      .ORG_LEVEL_NAME = ot.NAME_VN,
                                     .FILENAME = p.FILENAME,
                                     .ATTACH_FILE = p.ATTACH_FILE})
                    Else
                        query = (From p In Context.HU_ORGANIZATION
                                 From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REPRESENTATIVE_ID).DefaultIfEmpty
                                 From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.STATUS_ID).DefaultIfEmpty()
                                 From ot In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.ORG_LEVEL).DefaultIfEmpty()
                                 From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ID And f.USERNAME.ToUpper = username.ToUpper).DefaultIfEmpty
                                 Where p.ACTFLG = sACT
                                 Order By p.ORD_NO, p.CODE, p.NAME_VN
                                 Select New OrganizationDTO With {.ID = p.ID,
                                                                  .CODE = p.CODE,
                                                                  .NAME_VN = p.NAME_VN,
                                                                  .NAME_EN = p.NAME_EN,
                                                                  .PARENT_ID = p.PARENT_ID,
                                                                  .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                                  .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                                  .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                                  .NLEVEL = p.NLEVEL,
                                                                  .UY_BAN = p.UY_BAN,
                                                                  .GROUPPROJECT = If(p.GROUPPROJECT Is Nothing, 0, p.GROUPPROJECT),
                                                                  .EFFECT_DATE = p.EFFECT_DATE,
                                                                  .ACTFLG = If(p.ACTFLG = "A", "Sử dụng", "Ngừng sử dụng"),
                                                                  .STATUS_NAME = If(o.NAME_VN = "Mới khởi tạo", "", "disable-inupt"),
                                                                  .COLOR = If(p.ACTFLG = "I", "#969696", "#070101"),
                                                                  .ORD_NO = p.ORD_NO,
                                     .ORG_LEVEL = p.ORG_LEVEL,
                                                                      .ORG_LEVEL_NAME = ot.NAME_VN,
                                     .FILENAME = p.FILENAME,
                                     .ATTACH_FILE = p.ATTACH_FILE})
                    End If
                End If
            End If

            Dim lst = query
            If query IsNot Nothing Then
                Return query.ToList
            Else
                Return New List(Of OrganizationDTO)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOrgTreeEmp(ByVal sACT As String) As List(Of OrganizationDTO)
        Dim query As ObjectQuery(Of OrganizationDTO)
        Try

            If sACT = "" Then
                query = (From p In Context.HU_ORGANIZATION
                         From t In Context.HU_TITLE.Where(Function(a) a.ID = p.POSITION_ID).DefaultIfEmpty()
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.MASTER Or f.TITLE_ID = t.ID).DefaultIfEmpty()
                         From v In Context.HU_EMPLOYEE_CV.Where(Function(b) b.EMPLOYEE_ID = e.ID).DefaultIfEmpty()
                         From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.STATUS_ID).DefaultIfEmpty()
                         Order By p.ORD_NO, p.CODE, p.NAME_VN
                         Select New OrganizationDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                          .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                          .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                          .ORD_NO = p.ORD_NO,
                                                          .TITLE_NAME = t.CODE & " - " & t.NAME_VN,
                                                          .NLEVEL = p.NLEVEL,
                                                          .GROUPPROJECT = If(p.GROUPPROJECT Is Nothing, 0, p.GROUPPROJECT),
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .MOBILE = p.MOBILE,
                                                          .PROVINCE_NAME = p.PROVINCE_NAME,
                                                          .PIT_NO = p.PIT_NO,
                                                          .ADDRESS = p.ADDRESS,
                                                          .ACTFLG = If(p.ACTFLG = "A", "Sử dụng", "Ngừng sử dụng"),
                                                          .STATUS_NAME = o.NAME_VN,
                                                          .POSITION_ID = p.POSITION_ID,
                                                           .POSITION_NAME = e.FULLNAME_VN,
                                                          .COLOR = If(p.STATUS_ID = 8528, "#969696", "#070101"),
                                                          .IMAGE = v.IMAGE,
                                                         .ATTACH_FILE = p.ATTACH_FILE,
                                                         .FILENAME = p.FILENAME})
            Else
                query = (From p In Context.HU_ORGANIZATION
                         From t In Context.HU_TITLE.Where(Function(a) a.ID = p.POSITION_ID).DefaultIfEmpty()
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.MASTER Or f.TITLE_ID = t.ID).DefaultIfEmpty
                         From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = p.STATUS_ID).DefaultIfEmpty()
                         Where (p.ACTFLG = sACT And p.STATUS_ID = 8528) Or p.STATUS_ID = 8527
                         Order By p.ORD_NO, p.CODE, p.NAME_VN
                         Select New OrganizationDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .TITLE_NAME = t.CODE & " - " & t.NAME_VN,
                                                          .POSITION_ID = p.POSITION_ID,
                                                          .POSITION_NAME = e.FULLNAME_VN,
                                                          .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                          .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                          .NLEVEL = p.NLEVEL,
                                                          .GROUPPROJECT = If(p.GROUPPROJECT Is Nothing, 0, p.GROUPPROJECT),
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .ACTFLG = If(p.ACTFLG = "A", "Sử dụng", "Ngừng sử dụng"),
                                                          .STATUS_NAME = o.NAME_VN,
                                                          .COLOR = If(p.STATUS_ID = 8528, "#969696", "#070101"),
                                                          .ORD_NO = p.ORD_NO,
                                                           .ATTACH_FILE = p.ATTACH_FILE,
                                                         .FILENAME = p.FILENAME})
            End If
            Dim lst = query
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ActiveOrgTreeList(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As String,
                             ByVal log As UserLog) As Boolean
        '// Dim lstOrgTitleData As List(Of HU_ORGANIZATION_OM)
        '// Dim lstOrgeData As List(Of HU_ORGANIZATION)
        Try

            '// Active/Inactive OM
            Dim lstOrgOMData As List(Of HU_ORGANIZATION) = (From p In Context.HU_ORGANIZATION Where lstOrgTitle.Contains(p.ID)).ToList
            For index = 0 To lstOrgOMData.Count - 1
                lstOrgOMData(index).ACTFLG = sActive
                lstOrgOMData(index).MODIFIED_DATE = DateTime.Now
                lstOrgOMData(index).MODIFIED_BY = log.Username
                lstOrgOMData(index).MODIFIED_LOG = log.ComputerName
            Next

            '// Active/Inactive OM ACTIVITIES
            Dim lstOrgOMActivitiesData As List(Of HU_ORGANIZATION_OM_ACTIVITIES) = (From p In Context.HU_ORGANIZATION_OM_ACTIVITIES Where lstOrgTitle.Contains(p.ID)).ToList
            For index = 0 To lstOrgOMActivitiesData.Count - 1
                lstOrgOMActivitiesData(index).ACTFLG = sActive
                lstOrgOMActivitiesData(index).MODIFIED_DATE = DateTime.Now
                lstOrgOMActivitiesData(index).MODIFIED_BY = log.Username
                lstOrgOMActivitiesData(index).MODIFIED_LOG = log.ComputerName
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCompanyLevel(ByVal orgID As Decimal) As String
        Try
            Dim companyId = (From p In Context.OT_OTHER_LIST Where p.TYPE_ID = 43 And p.CODE = "COM" Select p.ID).FirstOrDefault
            Dim orgLevel = (From p In Context.HU_ORGANIZATION Where orgID = p.ID).FirstOrDefault
            If companyId = orgLevel.ORG_LEVEL Then
                Return orgLevel.NAME_VN
            Else
                If orgLevel.PARENT_ID IsNot Nothing Then
                    Return GetCompanyLevel(orgLevel.PARENT_ID)
                Else
                    Return ""
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetDataByProcedures(ByVal isBank As Decimal, Optional ByVal ID As Decimal = 0, Optional ByVal Name As String = "", Optional ByVal sLang As String = "vi-VN") As DataTable
        Try
            '// Generate token
            'If (Not (New TokenCheck).CheckToken(Token, MethodBase.GetCurrentMethod().Name.ToString())) Then
            '    Return New DataTable
            'End If

            Dim ds As New DataTable
            Using cls As New DataAccess.QueryData
                If isBank = 1 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_JOB_BAND",
                                             New With {.P_LANGUAGE = sLang,
                                                       .P_CUR = cls.OUT_CURSOR})

                ElseIf isBank = 2 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_ID_BY_NAME",
                                             New With {.P_NAME = Name,
                                                       .P_CUR = cls.OUT_CURSOR})

                ElseIf isBank = 3 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.CHECK_FUNCTION_EXIST",
                                             New With {.P_FUNCTIONID = ID,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 4 Then
                    ds = cls.ExecuteStore("PKG_PROFILE.CHECK_ACTIVE_ORG",
                                             New With {.P_ID = ID,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 5 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_JOB",
                                             New With {.P_LANGUAGE = sLang,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 6 Then
                    ds = cls.ExecuteStore("PKG_PROFILE.CHECK_AUTOCODE_WAGE",
                                             New With {.P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 7 Then
                    ds = cls.ExecuteStore("PKG_PROFILE.CHECK_AUTOCODE_CONTRACT",
                                             New With {.P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 8 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_STAFF_RANK",
                                             New With {.P_POSITION_ID = ID,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 9 Then
                    'dev lười ko thêm 1 tham số nên convert kiểu này
                    Dim s As New Decimal
                    If Name <> "" Then
                        s = Decimal.Parse(Name)
                    End If
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_TITLE_BY_ORG",
                                             New With {.P_ORGID = ID,
                                                       .P_EMPLOYEEID = s,
                                                       .P_LANGUAGE = sLang,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 10 Then
                    ds = cls.ExecuteStore("PKG_PROFILE.EMPLOYEE_CHECK_TERMINATE",
                                             New With {.P_ID = ID,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 11 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.CHECK_ACTIVE_ORGOM",
                                             New With {.P_ID = ID,
                                                       .P_CUR = cls.OUT_CURSOR})
                ElseIf isBank = 12 Then
                    ds = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_JOB_LEVEL_BY_TITLE",
                                             New With {.P_ID = ID,
                                                       .P_CUR = cls.OUT_CURSOR})
                End If
            End Using
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ModifyOrgTreeList(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog) As Boolean
        Dim objOrganizationData As New HU_ORGANIZATION With {.ID = objOrganization.ID}
        Try
            objOrganizationData = (From p In Context.HU_ORGANIZATION Where p.ID = objOrganization.ID).FirstOrDefault
            objOrganizationData.ID = objOrganization.ID
            objOrganizationData.CODE = objOrganization.CODE
            objOrganizationData.NAME_VN = objOrganization.NAME_VN.Trim
            objOrganizationData.NAME_EN = objOrganization.NAME_EN.Trim
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.NLEVEL = objOrganization.NLEVEL
            objOrganizationData.GROUPPROJECT = objOrganization.GROUPPROJECT
            objOrganizationData.EFFECT_DATE = objOrganization.EFFECT_DATE
            Context.SaveChanges(log)
            'gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyOrgTreeListPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean
        Try

            For Each item As OrganizationPathDTO In lstPath
                Dim objOrganizationData As New HU_ORGANIZATION With {.ID = item.ID}
                Context.HU_ORGANIZATION.Attach(objOrganizationData)
                objOrganizationData.DESCRIPTION_PATH = item.DESCRIPTION_PATH
                objOrganizationData.HIERARCHICAL_PATH = item.HIERARCHICAL_PATH
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetPossition(ByVal _filter As TitleDTO,
                                 ByVal _param As ParamDTO,
                                 ByVal sLang As String,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal IsReadWrite As Boolean = False,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of TitleDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG4",
                                 New With {.P_USERNAME = log.Username.ToUpper(),
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            'Dim query
            If (IsReadWrite) Then
                Dim query = (From p In Context.HU_TITLE
                             From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                             From phan_loai In Context.OT_OTHER_LIST.Where(Function(f) f.ID = j.PHAN_LOAI_ID).DefaultIfEmpty
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                             From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                             From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                             From dm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pd.MASTER).DefaultIfEmpty
                             From lm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER).DefaultIfEmpty
                             From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                             From i In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                             From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                             Where o.ACTFLG = "A"
                             Select (New TitleDTO With {
                                  .ID = p.ID,
                                  .CODE = p.CODE,
                                  .NAME_EN = p.NAME_EN,
                                  .NAME_VN = p.NAME_VN,
                                  .REMARK = p.REMARK,
                                  .JOB_NAME = j.CODE,
                                  .ORG_ID = p.ORG_ID,
                                  .ORG_NAME = If(sLang = "vi-VN", o.NAME_VN, o.NAME_EN),
                                  .ORG_DESC = o.DESCRIPTION_PATH,
                                  .IS_OWNER = p.ISOWNER,
                                  .IS_UYBAN = If(o.UY_BAN IsNot Nothing AndAlso o.UY_BAN = -1, True, False),
                                  .LM = p.LM,
                                  .LM_CODE = tt.CODE,
                                  .LM_NAME = If(sLang = "vi-VN", tt.NAME_VN, tt.NAME_EN),
                                  .CSM = p.CSM,
                                  .CSM_CODE = pd.CODE,
                                  .CSM_NAME = If(sLang = "vi-VN", pd.NAME_VN, pd.NAME_EN),
                                  .EMP_CSM = dm.EMPLOYEE_CODE & " - " & dm.FULLNAME_VN,
                                  .EMP_LM = lm.EMPLOYEE_CODE & " - " & lm.FULLNAME_VN,
                                  .IS_NONPHYSICAL = If(p.IS_NONPHYSICAL IsNot Nothing AndAlso p.IS_NONPHYSICAL = -1, True, False),
                                  .CREATED_DATE = p.CREATED_DATE,
                                  .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                  .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                  .TITLE_GROUP_ID = j.PHAN_LOAI_ID,
                                  .TITLE_GROUP_NAME = phan_loai.NAME_VN,
                                  .MASTER = p.MASTER,
                                  .MASTER_NAME = If(sLang = "vi-VN", m.EMPLOYEE_CODE & " - " & m.FULLNAME_VN, m.FULLNAME_EN),
                                  .INTERIM = p.INTERIM,
                                  .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                  .INTERIM_NAME = If(sLang = "vi-VN", i.EMPLOYEE_CODE & " - " & i.FULLNAME_VN, i.FULLNAME_EN),
                                  .BOTH = If(p.ISOWNER = -1, 1, 0),
                                  .IS_PLAN = p.IS_PLAN,
                                  .PHAN_LOAI_ID = j.PHAN_LOAI_ID,
                                  .PHAN_LOAI_NAME = phan_loai.NAME_VN,
                                  .Is_Vacant = If(p.MASTER Is Nothing AndAlso p.INTERIM Is Nothing, True, False)}))
                'From et In Context.HU_EMPLOYEE.Where(Function(f) f.TITLE_ID = p.ID).DefaultIfEmpty
                '.EMPLOYEE_NAME = et.FULLNAME_VN,
                '.EMPLOYEE_CODE = et.EMPLOYEE_CODE,
                '.EMPLOYEE_ID = et.ID,
                Dim lst = query

                If Not String.IsNullOrEmpty(_filter.CODE) Then
                    lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                    lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                    lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.JOB_NAME) Then
                    lst = lst.Where(Function(p) p.JOB_NAME.ToUpper.Contains(_filter.JOB_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If

                If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                    lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.REMARK) Then
                    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.MASTER_NAME) Then
                    lst = lst.Where(Function(p) p.MASTER_NAME.ToUpper.Contains(_filter.MASTER_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.INTERIM_NAME) Then
                    lst = lst.Where(Function(p) p.INTERIM_NAME.ToUpper.Contains(_filter.INTERIM_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.LM_CODE) Then
                    lst = lst.Where(Function(p) p.LM_CODE.ToUpper.Contains(_filter.LM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.CSM_CODE) Then
                    lst = lst.Where(Function(p) p.CSM_CODE.ToUpper.Contains(_filter.CSM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                    lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.PHAN_LOAI_NAME) Then
                    lst = lst.Where(Function(p) p.PHAN_LOAI_NAME.ToUpper.Contains(_filter.PHAN_LOAI_NAME.ToUpper))
                End If
                If _filter.Is_Vacant Then
                    lst = lst.Where(Function(p) p.Is_Vacant = True)
                End If
                If _filter.IS_UYBAN Then
                    lst = lst.Where(Function(p) p.IS_UYBAN = True And p.IS_NONPHYSICAL = True)
                Else
                    lst = lst.Where(Function(p) p.IS_UYBAN = False And p.IS_NONPHYSICAL = False)
                End If
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

                Return lst.ToList
            Else
                Dim query = (From p In Context.HU_TITLE
                             From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                             From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                             From phan_loai In Context.OT_OTHER_LIST.Where(Function(f) f.ID = j.PHAN_LOAI_ID).DefaultIfEmpty
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                             From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                             From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                             From dm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pd.MASTER).DefaultIfEmpty
                             From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                             From i In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                             From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                             Where o.ACTFLG = "A"
                             Select (New TitleDTO With {
                                  .ID = p.ID,
                                  .CODE = p.CODE,
                                  .NAME_EN = p.NAME_EN,
                                  .NAME_VN = p.NAME_VN,
                                  .REMARK = p.REMARK,
                                  .JOB_NAME = j.CODE,
                                  .ORG_ID = p.ORG_ID,
                                  .ORG_NAME = If(sLang = "vi-VN", o.NAME_VN, o.NAME_EN),
                                  .IS_OWNER = p.ISOWNER,
                                  .IS_UYBAN = If(o.UY_BAN IsNot Nothing AndAlso o.UY_BAN = -1, True, False),
                                  .LM = p.LM,
                                  .LM_CODE = tt.CODE,
                                  .LM_NAME = If(sLang = "vi-VN", tt.NAME_VN, tt.NAME_EN),
                                  .CSM = p.CSM,
                                  .CSM_CODE = pd.CODE,
                                  .CSM_NAME = If(sLang = "vi-VN", pd.NAME_VN, pd.NAME_EN),
                                  .EMP_CSM = dm.EMPLOYEE_CODE & " - " & dm.FULLNAME_VN,
                                  .IS_NONPHYSICAL = If(p.IS_NONPHYSICAL IsNot Nothing AndAlso p.IS_NONPHYSICAL = -1, True, False),
                                  .CREATED_DATE = p.CREATED_DATE,
                                  .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                  .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                  .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                  .TITLE_GROUP_NAME = group.NAME_VN,
                                  .MASTER = p.MASTER,
                                  .MASTER_NAME = If(sLang = "vi-VN", m.EMPLOYEE_CODE & " - " & m.FULLNAME_VN, m.FULLNAME_EN),
                                  .INTERIM = p.INTERIM,
                                  .INTERIM_NAME = If(sLang = "vi-VN", i.EMPLOYEE_CODE & " - " & i.FULLNAME_VN, i.FULLNAME_EN),
                                  .BOTH = If(p.ISOWNER = -1, 1, 0),
                                  .IS_PLAN = p.IS_PLAN,
                                  .PHAN_LOAI_ID = j.PHAN_LOAI_ID,
                                  .PHAN_LOAI_NAME = phan_loai.NAME_VN}))


                Dim lst = query

                If Not String.IsNullOrEmpty(_filter.CODE) Then
                    lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                    lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                    lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.JOB_NAME) Then
                    lst = lst.Where(Function(p) p.JOB_NAME.ToUpper.Contains(_filter.JOB_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If

                If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                    lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.REMARK) Then
                    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.MASTER_NAME) Then
                    lst = lst.Where(Function(p) p.MASTER_NAME.ToUpper.Contains(_filter.MASTER_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.INTERIM_NAME) Then
                    lst = lst.Where(Function(p) p.INTERIM_NAME.ToUpper.Contains(_filter.INTERIM_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.LM_CODE) Then
                    lst = lst.Where(Function(p) p.LM_CODE.ToUpper.Contains(_filter.LM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.CSM_CODE) Then
                    lst = lst.Where(Function(p) p.CSM_CODE.ToUpper.Contains(_filter.CSM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                    lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.PHAN_LOAI_NAME) Then
                    lst = lst.Where(Function(p) p.PHAN_LOAI_NAME.ToUpper.Contains(_filter.PHAN_LOAI_NAME.ToUpper))
                End If
                If _filter.Is_Vacant Then
                    lst = lst.Where(Function(p) p.Is_Vacant = True)
                End If
                If _filter.IS_UYBAN Then
                    lst = lst.Where(Function(p) p.IS_UYBAN = True And p.IS_NONPHYSICAL = True)
                End If
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

                Return lst.ToList
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetPossition2(ByVal _filter As TitleDTO,
                                 ByVal _param As ParamDTO,
                                 ByVal sLang As String,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal IsReadWrite As Boolean = False,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of TitleDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper(),
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            'Dim query
            If (IsReadWrite) Then
                Dim dateNow = Date.Now.Date
                Dim query = (From p In Context.HU_TITLE
                             From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                             From phan_loai In Context.OT_OTHER_LIST.Where(Function(f) f.ID = j.PHAN_LOAI_ID).DefaultIfEmpty
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                             From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                             From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                             From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                             From i In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                             From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                             Where m.WORK_STATUS Is Nothing Or (m.WORK_STATUS IsNot Nothing And (m.WORK_STATUS <> 257 Or (m.WORK_STATUS = 257 And m.TER_EFFECT_DATE > dateNow)))
                             Select (New TitleDTO With {
                                  .ID = p.ID,
                                  .CODE = p.CODE,
                                  .NAME_EN = p.NAME_EN,
                                  .NAME_VN = p.NAME_VN,
                                  .REMARK = p.REMARK,
                                  .JOB_NAME = j.CODE,
                                  .ORG_ID = p.ORG_ID,
                                  .ORG_NAME = If(sLang = "vi-VN", o.NAME_VN, o.NAME_EN),
                                  .ORG_DESC = o.DESCRIPTION_PATH,
                                  .IS_OWNER = p.ISOWNER,
                                  .LM = p.LM,
                                  .LM_CODE = tt.CODE,
                                  .LM_NAME = If(sLang = "vi-VN", tt.NAME_VN, tt.NAME_EN),
                                  .CSM = p.CSM,
                                  .CSM_CODE = pd.CODE,
                                  .CSM_NAME = If(sLang = "vi-VN", pd.NAME_VN, pd.NAME_EN),
                                  .IS_NONPHYSICAL = p.IS_NONPHYSICAL,
                                  .CREATED_DATE = p.CREATED_DATE,
                                  .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                  .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                  .TITLE_GROUP_ID = j.PHAN_LOAI_ID,
                                  .TITLE_GROUP_NAME = phan_loai.NAME_VN,
                                  .TITLE_GROUP_CODE = phan_loai.CODE,
                                  .MASTER = p.MASTER,
                                  .MASTER_NAME = If(sLang = "vi-VN", m.EMPLOYEE_CODE & " - " & m.FULLNAME_VN, m.EMPLOYEE_CODE & " - " & m.FULLNAME_EN),
                                  .INTERIM = p.INTERIM,
                                  .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                  .INTERIM_NAME = If(sLang = "vi-VN", i.EMPLOYEE_CODE & " - " & i.FULLNAME_VN, i.EMPLOYEE_CODE & " - " & i.FULLNAME_EN),
                                  .BOTH = If(p.ISOWNER = -1, 1, 0),
                                  .IS_PLAN = p.IS_PLAN,
                                  .PHAN_LOAI_ID = j.PHAN_LOAI_ID,
                                  .PHAN_LOAI_NAME = phan_loai.NAME_VN}))
                'From et In Context.HU_EMPLOYEE.Where(Function(f) f.TITLE_ID = p.ID).DefaultIfEmpty
                '.EMPLOYEE_NAME = et.FULLNAME_VN,
                '.EMPLOYEE_CODE = et.EMPLOYEE_CODE,
                '.EMPLOYEE_ID = et.ID,
                Dim lst = query

                If Not String.IsNullOrEmpty(_filter.CODE) Then
                    lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                    lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                    lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.JOB_NAME) Then
                    lst = lst.Where(Function(p) p.JOB_NAME.ToUpper.Contains(_filter.JOB_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If

                If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                    lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.REMARK) Then
                    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.MASTER_NAME) Then
                    lst = lst.Where(Function(p) p.MASTER_NAME.ToUpper.Contains(_filter.MASTER_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.INTERIM_NAME) Then
                    lst = lst.Where(Function(p) p.INTERIM_NAME.ToUpper.Contains(_filter.INTERIM_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.LM_CODE) Then
                    lst = lst.Where(Function(p) p.LM_CODE.ToUpper.Contains(_filter.LM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.CSM_CODE) Then
                    lst = lst.Where(Function(p) p.CSM_CODE.ToUpper.Contains(_filter.CSM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                    lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.PHAN_LOAI_NAME) Then
                    lst = lst.Where(Function(p) p.PHAN_LOAI_NAME.ToUpper.Contains(_filter.PHAN_LOAI_NAME.ToUpper))
                End If
                If _filter.Is_Vacant Then
                    lst = lst.Where(Function(p) String.IsNullOrEmpty(p.MASTER_NAME) AndAlso String.IsNullOrEmpty(p.INTERIM_NAME))
                End If

                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

                Return lst.ToList
            Else
                Dim dateNow = Date.Now.Date
                Dim query = (From p In Context.HU_TITLE
                             From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                             From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                             From phan_loai In Context.OT_OTHER_LIST.Where(Function(f) f.ID = j.PHAN_LOAI_ID).DefaultIfEmpty
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                             From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                             From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                             From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                             From i In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                             From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                             Where m.WORK_STATUS Is Nothing Or (m.WORK_STATUS IsNot Nothing And (m.WORK_STATUS <> 257 Or (m.WORK_STATUS = 257 And m.TER_EFFECT_DATE > dateNow)))
                             Select (New TitleDTO With {
                                  .ID = p.ID,
                                  .CODE = p.CODE,
                                  .NAME_EN = p.NAME_EN,
                                  .NAME_VN = p.NAME_VN,
                                  .REMARK = p.REMARK,
                                  .JOB_NAME = j.CODE,
                                  .ORG_ID = p.ORG_ID,
                                  .ORG_NAME = If(sLang = "vi-VN", o.NAME_VN, o.NAME_EN),
                                  .IS_OWNER = p.ISOWNER,
                                  .LM = p.LM,
                                  .LM_CODE = tt.CODE,
                                  .LM_NAME = If(sLang = "vi-VN", tt.NAME_VN, tt.NAME_EN),
                                  .CSM = p.CSM,
                                  .CSM_CODE = pd.CODE,
                                  .CSM_NAME = If(sLang = "vi-VN", pd.NAME_VN, pd.NAME_EN),
                                  .CREATED_DATE = p.CREATED_DATE,
                                  .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                  .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                  .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                  .TITLE_GROUP_NAME = group.NAME_VN,
                                  .TITLE_GROUP_CODE = phan_loai.CODE,
                                  .MASTER = p.MASTER,
                                  .MASTER_NAME = If(sLang = "vi-VN", m.EMPLOYEE_CODE & " - " & m.FULLNAME_VN, m.EMPLOYEE_CODE & " - " & m.FULLNAME_EN),
                                  .INTERIM = p.INTERIM,
                                  .INTERIM_NAME = If(sLang = "vi-VN", i.EMPLOYEE_CODE & " - " & i.FULLNAME_VN, i.EMPLOYEE_CODE & " - " & i.FULLNAME_EN),
                                  .BOTH = If(p.ISOWNER = -1, 1, 0),
                                  .IS_PLAN = p.IS_PLAN,
                                  .PHAN_LOAI_ID = j.PHAN_LOAI_ID,
                                  .PHAN_LOAI_NAME = phan_loai.NAME_VN}))


                Dim lst = query

                If Not String.IsNullOrEmpty(_filter.CODE) Then
                    lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                    lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                    lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.JOB_NAME) Then
                    lst = lst.Where(Function(p) p.JOB_NAME.ToUpper.Contains(_filter.JOB_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If

                If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                    lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.REMARK) Then
                    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.MASTER_NAME) Then
                    lst = lst.Where(Function(p) p.MASTER_NAME.ToUpper.Contains(_filter.MASTER_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.INTERIM_NAME) Then
                    lst = lst.Where(Function(p) p.INTERIM_NAME.ToUpper.Contains(_filter.INTERIM_NAME.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.LM_CODE) Then
                    lst = lst.Where(Function(p) p.LM_CODE.ToUpper.Contains(_filter.LM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.CSM_CODE) Then
                    lst = lst.Where(Function(p) p.CSM_CODE.ToUpper.Contains(_filter.CSM_CODE.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                    lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
                End If
                If Not String.IsNullOrEmpty(_filter.PHAN_LOAI_NAME) Then
                    lst = lst.Where(Function(p) p.PHAN_LOAI_NAME.ToUpper.Contains(_filter.PHAN_LOAI_NAME.ToUpper))
                End If
                If _filter.Is_Vacant Then
                    lst = lst.Where(Function(p) String.IsNullOrEmpty(p.MASTER_NAME) AndAlso String.IsNullOrEmpty(p.INTERIM_NAME))
                End If

                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

                Return lst.ToList
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetPossitionPortal(ByVal _filter As TitleDTO,
                                       ByVal EmployeeId As Decimal,
                                       ByVal sLang As String,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleDTO)

        Try
            Dim DOB As Nullable(Of Date)
            Dim lst As IQueryable(Of TitleDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_POSITION_PORTAL",
                                           New With {.P_LANGUAGE = sLang,
                                                     .P_EMPLOYEE_ID = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New TitleDTO With {.ID = If(row("ID") IsNot DBNull.Value, Decimal.Parse(row("ID").ToString), Nothing),
                                                   .CODE = If(row("CODE") IsNot DBNull.Value, row("CODE").ToString, Nothing),
                                                   .NAME_EN = If(row("NAME_EN") IsNot DBNull.Value, row("NAME_EN").ToString, Nothing),
                                                   .NAME_VN = If(row("NAME_VN") IsNot DBNull.Value, row("NAME_VN").ToString, Nothing),
                                                    .ORG_ID = If(row("ORG_ID") IsNot DBNull.Value, Decimal.Parse(row("ORG_ID")), Nothing),
                                                   .ORG_NAME = If(row("ORG_NAME") IsNot DBNull.Value, row("ORG_NAME").ToString, Nothing),
                                                   .IS_OWNER = If(row("ISOWNER") = -1, True, False),
                                                   .LM_CODE = If(row("LM_CODE") IsNot DBNull.Value, row("LM_CODE").ToString, Nothing),
                                                   .CSM_CODE = If(row("CSM_CODE") IsNot DBNull.Value, row("CSM_CODE").ToString, Nothing),
                                                   .MASTER_NAME = If(row("MASTER_NAME") IsNot DBNull.Value, row("MASTER_NAME").ToString, Nothing),
                                                   .INTERIM_NAME = If(row("INTERIM_NAME") IsNot DBNull.Value, row("INTERIM_NAME").ToString, Nothing),
                                                   .IS_NONPHYSICAL = If(row("IS_NONPHYSICAL") IsNot DBNull.Value And row("IS_NONPHYSICAL") = -1, True, False),
                                                   .IS_PLAN = If(row("IS_PLAN") IsNot DBNull.Value And row("IS_PLAN") = -1, True, False),
                                                   .COST_CENTER = If(row("COST_CENTER") IsNot DBNull.Value, row("COST_CENTER").ToString, Nothing),
                                                   .EFFECTIVE_DATE = If(row("EFFECTIVE_DATE") IsNot DBNull.Value, Date.Parse(row("EFFECTIVE_DATE").ToString), DOB),
                                                   .BOTH = If(row("ISOWNER") = -1, 1, 0)
                                                  }).AsQueryable
                End If
            End Using
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.JOB_NAME) Then
                lst = lst.Where(Function(p) p.JOB_NAME.ToUpper.Contains(_filter.JOB_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.MASTER_NAME) Then
                lst = lst.Where(Function(p) p.MASTER_NAME.ToUpper.Contains(_filter.MASTER_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.INTERIM_NAME) Then
                lst = lst.Where(Function(p) p.INTERIM_NAME.ToUpper.Contains(_filter.INTERIM_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LM_CODE) Then
                lst = lst.Where(Function(p) p.LM_CODE.ToUpper.Contains(_filter.LM_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CSM_CODE) Then
                lst = lst.Where(Function(p) p.CSM_CODE.ToUpper.Contains(_filter.CSM_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ORG_ID_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID_SEARCH)
            End If
            If _filter.EFFECTIVE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECTIVE_DATE = _filter.EFFECTIVE_DATE)
            End If

            'lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetPossitionPortal")
            Throw ex
        End Try
    End Function
    Public Function ModifyOrgTreeEmp(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        'Dim objOrgData As New HU_ORGANIZATION With {.ID = objOrganization.ID}
        '// Cập nhật các thông tin bổ sung chỉ dùng ở Histaff
        Try

            '// ORG Histaff
            Dim objOrganizationData As New HU_ORGANIZATION With {.ID = objOrganization.ID}
            objOrganizationData = (From p In Context.HU_ORGANIZATION Where p.ID = objOrganization.ID).FirstOrDefault
            objOrganizationData.ID = objOrganization.ID
            objOrganizationData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrganizationData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationData.ADDRESS = objOrganization.ADDRESS
            objOrganizationData.FAX = objOrganization.FAX
            objOrganizationData.MOBILE = objOrganization.MOBILE
            objOrganizationData.POSITION_ID = objOrganization.POSITION_ID
            objOrganizationData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrganizationData.PIT_NO = objOrganization.PIT_NO
            'objOrganizationData.ORD_NO = objOrganization.ORD_NO
            objOrganizationData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID
            'Them file dinh kem
            objOrganizationData.ATTACH_FILE = objOrganization.ATTACH_FILE
            objOrganizationData.FILENAME = objOrganization.FILENAME

            '// ORG OM
            Dim objOrganizationOMData = New HU_ORGANIZATION_OM With {.ID = objOrganization.ID}
            objOrganizationOMData = (From p In Context.HU_ORGANIZATION_OM Where p.ID = objOrganization.ID).FirstOrDefault
            objOrganizationOMData.ID = objOrganization.ID
            objOrganizationOMData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrganizationOMData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrganizationOMData.ADDRESS = objOrganization.ADDRESS
            objOrganizationOMData.FAX = objOrganization.FAX
            objOrganizationOMData.MOBILE = objOrganization.MOBILE
            'objOrganizationOMData.STATUS_ID = objOrganization.STATUS_ID
            objOrganizationOMData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrganizationOMData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationOMData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrganizationOMData.PIT_NO = objOrganization.PIT_NO
            'objOrganizationOMData.ORD_NO = objOrganization.ORD_NO
            objOrganizationOMData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID

            '// ORG OM_ACTIVITIES
            Dim objOrgOMActivitiesData = New HU_ORGANIZATION_OM_ACTIVITIES With {.ID = objOrganization.ID}
            objOrgOMActivitiesData = (From p In Context.HU_ORGANIZATION_OM_ACTIVITIES Where p.ID = objOrganization.ID).FirstOrDefault
            objOrgOMActivitiesData.ID = objOrganization.ID
            objOrgOMActivitiesData.FOUNDATION_DATE = objOrganization.FOUNDATION_DATE
            objOrgOMActivitiesData.DISSOLVE_DATE = objOrganization.DISSOLVE_DATE
            objOrgOMActivitiesData.ADDRESS = objOrganization.ADDRESS
            objOrgOMActivitiesData.FAX = objOrganization.FAX
            objOrgOMActivitiesData.MOBILE = objOrganization.MOBILE
            'objOrgOMActivitiesData.STATUS_ID = objOrganization.STATUS_ID
            objOrgOMActivitiesData.PROVINCE_NAME = objOrganization.PROVINCE_NAME
            objOrgOMActivitiesData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrgOMActivitiesData.DATE_BUSINESS = objOrganization.DATE_BUSINESS
            objOrgOMActivitiesData.PIT_NO = objOrganization.PIT_NO
            'objOrgOMActivitiesData.ORD_NO = objOrganization.ORD_NO
            objOrgOMActivitiesData.REPRESENTATIVE_ID = objOrganization.REPRESENTATIVE_ID


            Context.SaveChanges(log)
            gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertOrgTreeList(ByVal objOrganization As OrganizationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOrganizationData As New HU_ORGANIZATION
        Try

            Dim Count1 As Integer = (From p In Context.HU_ORGANIZATION Where p.ID = objOrganization.PARENT_ID Select p.PARENT_ID).Count
            Dim pr1 As Integer
            If Count1 <> 0 Then
                Dim query = (From p In Context.HU_ORGANIZATION Where p.ID = objOrganization.PARENT_ID Select p.PARENT_ID).FirstOrDefault
                pr1 = If(query IsNot Nothing, query, 0)
            End If
            Dim Count2 As Integer = (From p In Context.HU_ORGANIZATION Where p.ID = pr1 Select p.PARENT_ID).Count
            Dim pr2 As Integer
            If Count2 <> 0 Then
                Dim query = (From p In Context.HU_ORGANIZATION Where p.ID = pr1 Select p.PARENT_ID).FirstOrDefault
                pr2 = If(query IsNot Nothing, query, 0)
            End If
            Dim Count3 As Integer = (From p In Context.HU_ORGANIZATION Where p.ID = pr2 Select p.PARENT_ID).Count
            Dim pr3 As Integer
            If Count3 <> 0 Then
                Dim query = (From p In Context.HU_ORGANIZATION Where p.ID = pr2 Select p.PARENT_ID).FirstOrDefault
                pr3 = If(query IsNot Nothing, query, 0)
            End If
            Dim Count4 As Integer = (From p In Context.HU_ORGANIZATION Where p.ID = pr3 Select p.PARENT_ID).Count
            Dim ORGID = Utilities.GetNextSequence(Context, Context.HU_ORGANIZATION.EntitySet.Name)
            objOrganizationData.ID = ORGID
            objOrganizationData.CODE = objOrganization.CODE
            objOrganizationData.NAME_VN = objOrganization.NAME_VN.Trim
            objOrganizationData.NAME_EN = objOrganization.NAME_EN.Trim
            objOrganizationData.COST_CENTER_CODE = objOrganization.COST_CENTER_CODE
            objOrganizationData.NLEVEL = Count1 + Count2 + Count3 + Count4
            objOrganizationData.EFFECT_DATE = objOrganization.EFFECT_DATE
            objOrganizationData.GROUPPROJECT = objOrganization.GROUPPROJECT
            objOrganizationData.STATUS_ID = 8528
            objOrganizationData.POSITION_ID = objOrganization.POSITION_ID

            objOrganizationData.ACTFLG = "A"
            objOrganizationData.PARENT_ID = objOrganization.PARENT_ID
            objOrganizationData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH + ";" + ORGID.ToString
            objOrganizationData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH + ";" + objOrganization.CODE + " - " + objOrganization.NAME_VN.Trim

            Context.HU_ORGANIZATION.AddObject(objOrganizationData)
            Context.SaveChanges(log)
            gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function Activejob(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstjobData As List(Of HU_JOB)
        Try
            lstjobData = (From p In Context.HU_JOB Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstjobData.Count - 1
                lstjobData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckJobExistInTitle(JobId As Decimal) As Boolean
        Dim count = (From p In Context.HU_TITLE Where p.JOB_ID = JobId).Count
        If count > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function Deletejob(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstjobData As List(Of HU_JOB)
        Try

            lstjobData = (From p In Context.HU_JOB Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstjobData.Count - 1
                Context.HU_JOB.DeleteObject(lstjobData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function Getjob(ByVal _filter As JobDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            ByVal Language As String,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobDTO)

        Try
            Dim query = From p In Context.HU_JOB
                        From a In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.JOB_BAND_ID).DefaultIfEmpty
                        From b In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.JOB_FAMILY_ID).DefaultIfEmpty
                        From c In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PHAN_LOAI_ID).DefaultIfEmpty
                        Select New JobDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .NOTE = p.NOTE,
                                   .REQUEST = p.REQUEST,
                                   .PURPOSE = p.PURPOSE,
                                   .NAMECBO = p.CODE + " - " + p.NAME_VN,
                                   .JOB_BAND_ID = p.JOB_BAND_ID,
                                   .JOB_BAND_NAME = If(Language = "vi-VN", a.NAME_VN, a.NAME_EN),
                                   .JOB_FAMILY_ID = p.JOB_FAMILY_ID,
                                   .JOB_FAMILY_NAME = If(Language = "vi-VN", b.NAME_VN, b.NAME_EN),
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .PHAN_LOAI_ID = p.PHAN_LOAI_ID,
                                   .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                   .PHAN_LOAI_NAME = If(Language = "vi-VN", c.NAME_VN, c.NAME_EN)}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PHAN_LOAI_NAME) Then
                lst = lst.Where(Function(p) p.PHAN_LOAI_NAME.ToUpper.Contains(_filter.PHAN_LOAI_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.JOB_BAND_NAME) Then
                lst = lst.Where(Function(p) p.JOB_BAND_NAME.ToUpper.Contains(_filter.JOB_BAND_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.JOB_FAMILY_NAME) Then
                lst = lst.Where(Function(p) p.JOB_FAMILY_NAME.ToUpper.Contains(_filter.JOB_FAMILY_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetjobID(ByVal ID As Decimal) As JobDTO

        Try
            Dim query = (From p In Context.HU_JOB
                         From j In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.JOB_BAND_ID).DefaultIfEmpty
                         From b In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.JOB_FAMILY_ID).DefaultIfEmpty
                         From c In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PHAN_LOAI_ID).DefaultIfEmpty
                         Where p.ID = ID
                         Select New JobDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .NOTE = p.NOTE,
                                    .REQUEST = p.REQUEST,
                                    .PURPOSE = p.PURPOSE,
                                    .JOB_BAND_ID = p.JOB_BAND_ID,
                                    .JOB_BAND_NAME = j.NAME_VN,
                                    .JOB_FAMILY_ID = p.JOB_FAMILY_ID,
                                    .JOB_FAMILY_NAME = b.NAME_VN,
                                    .PHAN_LOAI_ID = p.PHAN_LOAI_ID,
                                    .PHAN_LOAI_NAME = c.NAME_VN,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                    .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                    .FUNCTION_ID = p.FUNCTION_ID})

            Dim job = query.FirstOrDefault

            If job IsNot Nothing Then

                job.lstPosition = (From p In Context.HU_JOB_POSITION
                                   Where p.JOB_ID = job.ID
                                   Select New JobPositionDTO With {.ID = p.ID,
                                                                        .NAME = p.NAME,
                                                                        .NAME_EN = p.NAME_EN,
                                                                        .JOB_ID = p.JOB_ID}).ToList
            End If

            Return job
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function Insertjob(ByVal objjob As JobDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objjobData As New HU_JOB
        Dim obj As New HU_JOB_POSITION
        Dim iCount As Integer = 0
        Try
            objjobData.ID = Utilities.GetNextSequence(Context, Context.HU_JOB.EntitySet.Name)
            objjobData.CODE = AutoGenCode("0", "HU_JOB", "CODE")
            objjobData.NAME_EN = objjob.NAME_EN
            objjobData.NAME_VN = objjob.NAME_VN
            objjobData.ACTFLG = objjob.ACTFLG
            objjobData.NOTE = objjob.NOTE
            objjobData.REQUEST = objjob.REQUEST
            objjobData.PURPOSE = objjob.PURPOSE
            objjobData.PHAN_LOAI_ID = objjob.PHAN_LOAI_ID
            objjobData.JOB_BAND_ID = objjob.JOB_BAND_ID
            objjobData.JOB_FAMILY_ID = objjob.JOB_FAMILY_ID
            Context.HU_JOB.AddObject(objjobData)

            'obj.ID = Utilities.GetNextSequence(Context, Context.HU_JOB_POSITION.EntitySet.Name)
            'obj.JOB_ID = objjobData.ID
            'obj.ACTFLG = objjob.ACTFLG
            'obj.NAME = objjob.NAME_VN
            'obj.NAME_EN = IIf(objjob.NAME_EN Is Nothing, objjob.NAME_VN, objjob.NAME_EN)
            'Context.HU_JOB_POSITION.AddObject(obj)

            Context.SaveChanges(log)
            gID = objjobData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function Validatejob(ByVal _validate As JobDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_JOB
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_JOB
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_JOB
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_JOB
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Modifyjob(ByVal objjob As JobDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objjobData As HU_JOB
        Try
            objjobData = (From p In Context.HU_JOB Where p.ID = objjob.ID).FirstOrDefault
            objjobData.ID = objjob.ID
            objjobData.CODE = objjob.CODE
            objjobData.NAME_EN = objjob.NAME_EN
            objjobData.NAME_VN = objjob.NAME_VN
            objjobData.NOTE = objjob.NOTE
            objjobData.REQUEST = objjob.REQUEST
            objjobData.PURPOSE = objjob.PURPOSE
            objjobData.PHAN_LOAI_ID = objjob.PHAN_LOAI_ID
            objjobData.JOB_BAND_ID = objjob.JOB_BAND_ID
            objjobData.JOB_FAMILY_ID = objjob.JOB_FAMILY_ID
            Dim lstTitle = (From p In Context.HU_TITLE Where p.JOB_ID = objjob.ID).ToList
            For Each item In lstTitle
                item.NAME_VN = objjob.NAME_VN
                item.NAME_EN = objjob.NAME_EN
            Next
            Context.SaveChanges(log)
            gID = objjobData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    '//Kiểm tra trùng mã

    Public Function ValidateJobCode(ByVal _validate As JobDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_JOB
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_JOB
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_JOB
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_JOB
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetjobFunctionByJobID(ByVal ID As Decimal) As List(Of JobFunctionDTO)

        Try
            Dim query = (From p In Context.HU_JOB_FUNCTION
                         From a In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                         From s In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FUNCTION_ID).DefaultIfEmpty
                         Where p.JOB_ID = ID
                         Select New JobFunctionDTO With {
                                    .ID = p.ID,
                                    .NAME = p.NAME,
                                    .NAME_EN = p.NAME_EN,
                                    .PARENT_ID = p.PARENT_ID,
                                    .FUNCTION_ID = p.FUNCTION_ID,
                                    .FUNCTION_NAME = s.NAME_VN,
                                    .JOB_ID = p.JOB_ID})

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertjobFunction(ByVal objjob As JobFunctionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objjobData As New HU_JOB_FUNCTION
        Dim iCount As Integer = 0
        Try
            objjobData.ID = Utilities.GetNextSequence(Context, Context.HU_JOB_FUNCTION.EntitySet.Name)
            objjobData.JOB_ID = objjob.JOB_ID
            objjobData.NAME = objjob.NAME
            objjobData.NAME_EN = objjob.NAME_EN
            objjobData.PARENT_ID = objjob.PARENT_ID
            objjobData.FUNCTION_ID = objjob.FUNCTION_ID
            Context.HU_JOB_FUNCTION.AddObject(objjobData)

            Context.SaveChanges(log)
            gID = objjobData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyjobFunction(ByVal objjob As JobFunctionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objjobData As HU_JOB_FUNCTION
        Try
            objjobData = (From p In Context.HU_JOB_FUNCTION Where p.ID = objjob.ID).FirstOrDefault
            objjobData.ID = objjob.ID
            objjobData.JOB_ID = objjob.JOB_ID
            objjobData.NAME = objjob.NAME
            objjobData.NAME_EN = objjob.NAME_EN
            objjobData.PARENT_ID = objjob.PARENT_ID
            objjobData.FUNCTION_ID = objjob.FUNCTION_ID

            Context.SaveChanges(log)
            gID = objjobData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeletejobFunction(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstjobData As List(Of HU_JOB_FUNCTION)
        Try

            lstjobData = (From p In Context.HU_JOB_FUNCTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstjobData.Count - 1
                Context.HU_JOB_FUNCTION.DeleteObject(lstjobData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function ActivePositions(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByRef Status As String,
                                  ByVal log As UserLog) As Boolean
        'Dim lstTitleData As List(Of HU_TITLE)
        Try
            '// Generate token
            If sActive = "A" Then
                For Each it In lstID
                    Dim titleId = (From p In Context.HU_TITLE Where p.ID = it).FirstOrDefault
                    If titleId IsNot Nothing Then
                        Dim Pos = From t In Context.HU_TITLE Where t.ORG_ID = titleId.ORG_ID And t.ISOWNER = -1 And t.ACTFLG = "A"
                        If titleId.ISOWNER = -1 Then
                            If Pos IsNot Nothing Then
                                Status = "Đơn vị đã có trưởng, thao tác không thành công"
                                Return False
                            Else
                                titleId.ACTFLG = sActive
                            End If
                        Else
                            titleId.ACTFLG = sActive
                        End If
                    End If
                Next
            Else
                For Each item In lstID
                    Dim objTitle = (From p In Context.HU_TITLE_ACTIVITIES
                                    Where (p.LM = item Or p.CSM = item) _
                                    And p.ACTFLG = "A").FirstOrDefault
                    Dim titleId = (From p In Context.HU_TITLE_ACTIVITIES Where p.ID = item).FirstOrDefault
                    If titleId IsNot Nothing Then
                        If titleId.MASTER IsNot Nothing Or titleId.INTERIM IsNot Nothing Then
                            Status = "Vị trí đã có nhân viên, thao tác không thành công"
                            Return False
                        End If
                        If titleId.HIRING_STATUS = -1 Then
                            Status = "Vị trí đang được tuyển dụng, thao tác không thành công"
                            Return False
                        End If
                        If objTitle IsNot Nothing Then
                            Status = "Vị trí đang là quản lý của vị trí khác, thao tác không thành công"
                            Return False
                        End If
                        titleId.ACTFLG = sActive
                        titleId.EFFECTIVE_DATE = Date.Now.Date
                        titleId.TYPE_ACTIVITIES = "UPDATE"
                    End If

                    Dim lstTitleData = (From p In Context.HU_TITLE Where lstID.Contains(p.ID)).ToList
                    For index = 0 To lstTitleData.Count - 1
                        lstTitleData(index).ACTFLG = sActive
                    Next
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function DeletePositions(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstTitleData As List(Of HU_TITLE_ACTIVITIES)
        Try
            lstTitleData = (From p In Context.HU_TITLE_ACTIVITIES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_TITLE_ACTIVITIES.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function CheckOwnerExist(ByVal objTitle As TitleDTO) As Boolean
        Try
            Dim exits = From t In Context.HU_TITLE
                        Where t.ISOWNER = -1 And t.ACTFLG = "A" And t.ORG_ID = objTitle.ORG_ID And objTitle.ID <> t.ID
            If exits.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetPositionByID(ByVal sID As Decimal) As List(Of TitleDTO)

        Try
            Dim query = (From p In Context.HU_TITLE.ToList Where p.ID = sID
                         Select New TitleDTO With {.ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyPossition(ByVal objTitle As TitleDTO, ByRef gID As Decimal, ByVal log As UserLog,
                                    Optional ByVal OrgIDDefault As Decimal = 1,
                                    Optional ByVal IsDissolveDefault As Decimal = 0) As Boolean
        Dim objTitleData As New HU_TITLE
        Dim objJobDescriptionData As New HU_JOB_DESCRIPTION
        Try

            objTitleData = (From p In Context.HU_TITLE Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = "A"
            'objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            'objTitleData.REMARK = objTitle.REMARK
            objTitleData.JOB_ID = objTitle.JOB_ID
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.ISOWNER = objTitle.IS_OWNER
            objTitleData.IS_NONPHYSICAL = objTitle.IS_NONPHYSICAL
            objTitleData.IS_PLAN = objTitle.IS_PLAN
            objTitleData.LM = objTitle.LM
            objTitleData.CSM = objTitle.CSM
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.FILENAME = objTitle.FILENAME
            objTitleData.UPLOADFILE = objTitle.UPLOADFILE
            objTitleData.WORKING_TIME = objTitle.WORKING_TIME
            Context.SaveChanges(log)
            gID = objTitleData.ID
            objJobDescriptionData = (From p In Context.HU_JOB_DESCRIPTION Where p.TITLE_ID = objTitle.ID).FirstOrDefault
            If objJobDescriptionData Is Nothing Then
                objJobDescriptionData = New HU_JOB_DESCRIPTION
                objJobDescriptionData.ID = Utilities.GetNextSequence(Context, Context.HU_JOB_DESCRIPTION.EntitySet.Name)
                objJobDescriptionData.TITLE_ID = objTitle.ID
                Context.HU_JOB_DESCRIPTION.AddObject(objJobDescriptionData)
                Context.SaveChanges(log)
                objJobDescriptionData = (From p In Context.HU_JOB_DESCRIPTION Where p.TITLE_ID = objTitle.ID).FirstOrDefault
            End If
            objJobDescriptionData.JOB_TARGET_1 = objTitle.JobDescription.JOB_TARGET_1
            objJobDescriptionData.JOB_TARGET_2 = objTitle.JobDescription.JOB_TARGET_2
            objJobDescriptionData.JOB_TARGET_3 = objTitle.JobDescription.JOB_TARGET_3
            objJobDescriptionData.JOB_TARGET_4 = objTitle.JobDescription.JOB_TARGET_4
            objJobDescriptionData.JOB_TARGET_5 = objTitle.JobDescription.JOB_TARGET_5
            objJobDescriptionData.JOB_TARGET_6 = objTitle.JobDescription.JOB_TARGET_6
            objJobDescriptionData.TDCM = objTitle.JobDescription.TDCM
            objJobDescriptionData.MAJOR = objTitle.JobDescription.MAJOR_NAME
            objJobDescriptionData.WORK_EXP = objTitle.JobDescription.WORK_EXP
            objJobDescriptionData.LANGUAGE = objTitle.JobDescription.LANGUAGE
            objJobDescriptionData.COMPUTER = objTitle.JobDescription.COMPUTER
            objJobDescriptionData.SUPPORT_SKILL = objTitle.JobDescription.SUPPORT_SKILL
            objJobDescriptionData.INTERNAL_1 = objTitle.JobDescription.INTERNAL_1
            objJobDescriptionData.INTERNAL_2 = objTitle.JobDescription.INTERNAL_2
            objJobDescriptionData.INTERNAL_3 = objTitle.JobDescription.INTERNAL_3
            objJobDescriptionData.OUTSIDE_1 = objTitle.JobDescription.OUTSIDE_1
            objJobDescriptionData.OUTSIDE_2 = objTitle.JobDescription.OUTSIDE_2
            objJobDescriptionData.OUTSIDE_3 = objTitle.JobDescription.OUTSIDE_3
            objJobDescriptionData.RESPONSIBILITY_1 = objTitle.JobDescription.RESPONSIBILITY_1
            objJobDescriptionData.RESPONSIBILITY_2 = objTitle.JobDescription.RESPONSIBILITY_2
            objJobDescriptionData.RESPONSIBILITY_3 = objTitle.JobDescription.RESPONSIBILITY_3
            objJobDescriptionData.RESPONSIBILITY_4 = objTitle.JobDescription.RESPONSIBILITY_4
            objJobDescriptionData.RESPONSIBILITY_5 = objTitle.JobDescription.RESPONSIBILITY_5
            objJobDescriptionData.DETAIL_RESPONSIBILITY_1 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_1
            objJobDescriptionData.DETAIL_RESPONSIBILITY_2 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_2
            objJobDescriptionData.DETAIL_RESPONSIBILITY_3 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_3
            objJobDescriptionData.DETAIL_RESPONSIBILITY_4 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_4
            objJobDescriptionData.DETAIL_RESPONSIBILITY_5 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_5
            objJobDescriptionData.OUT_RESULT_1 = objTitle.JobDescription.OUT_RESULT_1
            objJobDescriptionData.OUT_RESULT_2 = objTitle.JobDescription.OUT_RESULT_2
            objJobDescriptionData.OUT_RESULT_3 = objTitle.JobDescription.OUT_RESULT_3
            objJobDescriptionData.OUT_RESULT_4 = objTitle.JobDescription.OUT_RESULT_4
            objJobDescriptionData.OUT_RESULT_5 = objTitle.JobDescription.OUT_RESULT_5
            objJobDescriptionData.PERMISSION_1 = objTitle.JobDescription.PERMISSION_1
            objJobDescriptionData.PERMISSION_2 = objTitle.JobDescription.PERMISSION_2
            objJobDescriptionData.PERMISSION_3 = objTitle.JobDescription.PERMISSION_3
            objJobDescriptionData.PERMISSION_4 = objTitle.JobDescription.PERMISSION_4
            objJobDescriptionData.PERMISSION_5 = objTitle.JobDescription.PERMISSION_5
            objJobDescriptionData.PERMISSION_6 = objTitle.JobDescription.PERMISSION_6
            objJobDescriptionData.FILE_NAME = objTitle.JobDescription.FILE_NAME
            objJobDescriptionData.UPLOAD_FILE = objTitle.JobDescription.UPLOAD_FILE
            Context.SaveChanges(log)
            If objTitle.EFFECTIVE_DATE <= DateTime.Now Then
                Try
                    Using q As New DataAccess.QueryData()
                        Dim params As New Dictionary(Of String, Object)
                        q.ExecuteStoreProcedureNonQueryTask("PKG_OMS_BUSINESS.JOB_AUTO_UPDATE_TITLE_TEMP", params)
                    End Using
                Catch ex As Exception

                End Try
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function ValidatePosition_ACTIVITIES(ByVal _validate As TitleDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE_ACTIVITIES
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_TITLE_ACTIVITIES
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE_ACTIVITIES
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_TITLE_ACTIVITIES
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ValidatePosition(ByVal _validate As TitleDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.HU_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.HU_TITLE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.HU_TITLE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertPossition(ByVal objTitle As TitleDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal,
                                    Optional ByVal OrgIDDefault As Decimal = 1,
                                    Optional ByVal IsDissolveDefault As Decimal = 0) As Boolean
        Dim objTitleData As New HU_TITLE
        Dim objJobDescriptionData As New HU_JOB_DESCRIPTION
        Dim iCount As Integer = 0
        Try

            '// Check quyen
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper(),
                                           .P_ORGID = OrgIDDefault,
                                           .P_ISDISSOLVE = IsDissolveDefault})
            End Using

            Dim objChk As List(Of Decimal) = (From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = objTitle.ORG_ID And f.USERNAME = log.Username.ToUpper())
                                              Select chosen.ORG_ID).ToList()
            If (objChk.Count < 1) Then
                Return False
            End If

            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
            objTitleData.CODE = AutoGenCodeHuTile("HU_TITLE", "CODE")
            objTitleData.NAME_EN = objTitle.NAME_EN
            'objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.ACTFLG = "A"
            'objTitleData.REMARK = objTitle.REMARK
            objTitleData.JOB_ID = objTitle.JOB_ID
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.ISOWNER = objTitle.IS_OWNER
            objTitleData.IS_NONPHYSICAL = objTitle.IS_NONPHYSICAL
            objTitleData.IS_PLAN = objTitle.IS_PLAN
            objTitleData.LM = objTitle.LM
            objTitleData.CSM = objTitle.CSM
            'objTitleData.CSM_1 = objTitle.CSM_1
            'objTitleData.CSM_2 = objTitle.CSM_2
            'objTitleData.CSM_3 = objTitle.CSM_3
            'objTitleData.CSM_4 = objTitle.CSM_4
            objTitleData.COST_CENTER = objTitle.COST_CENTER
            'objTitleData.WORK_LOCATION = objTitle.WORK_LOCATION
            'objTitleData.JOB_SPEC = objTitle.JOB_SPEC
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.FILENAME = objTitle.FILENAME
            objTitleData.UPLOADFILE = objTitle.UPLOADFILE
            objTitleData.WORKING_TIME = objTitle.WORKING_TIME
            Context.HU_TITLE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            objTitle.ID = gID

            objJobDescriptionData.ID = Utilities.GetNextSequence(Context, Context.HU_JOB_DESCRIPTION.EntitySet.Name)
            objJobDescriptionData.JOB_TARGET_1 = objTitle.JobDescription.JOB_TARGET_1
            objJobDescriptionData.JOB_TARGET_2 = objTitle.JobDescription.JOB_TARGET_2
            objJobDescriptionData.JOB_TARGET_3 = objTitle.JobDescription.JOB_TARGET_3
            objJobDescriptionData.JOB_TARGET_4 = objTitle.JobDescription.JOB_TARGET_4
            objJobDescriptionData.JOB_TARGET_5 = objTitle.JobDescription.JOB_TARGET_5
            objJobDescriptionData.JOB_TARGET_6 = objTitle.JobDescription.JOB_TARGET_6
            objJobDescriptionData.TDCM = objTitle.JobDescription.TDCM
            objJobDescriptionData.MAJOR = objTitle.JobDescription.MAJOR_NAME
            objJobDescriptionData.WORK_EXP = objTitle.JobDescription.WORK_EXP
            objJobDescriptionData.LANGUAGE = objTitle.JobDescription.LANGUAGE
            objJobDescriptionData.COMPUTER = objTitle.JobDescription.COMPUTER
            objJobDescriptionData.SUPPORT_SKILL = objTitle.JobDescription.SUPPORT_SKILL
            objJobDescriptionData.INTERNAL_1 = objTitle.JobDescription.INTERNAL_1
            objJobDescriptionData.INTERNAL_2 = objTitle.JobDescription.INTERNAL_2
            objJobDescriptionData.INTERNAL_3 = objTitle.JobDescription.INTERNAL_3
            objJobDescriptionData.OUTSIDE_1 = objTitle.JobDescription.OUTSIDE_1
            objJobDescriptionData.OUTSIDE_2 = objTitle.JobDescription.OUTSIDE_2
            objJobDescriptionData.OUTSIDE_3 = objTitle.JobDescription.OUTSIDE_3
            objJobDescriptionData.RESPONSIBILITY_1 = objTitle.JobDescription.RESPONSIBILITY_1
            objJobDescriptionData.RESPONSIBILITY_2 = objTitle.JobDescription.RESPONSIBILITY_2
            objJobDescriptionData.RESPONSIBILITY_3 = objTitle.JobDescription.RESPONSIBILITY_3
            objJobDescriptionData.RESPONSIBILITY_4 = objTitle.JobDescription.RESPONSIBILITY_4
            objJobDescriptionData.RESPONSIBILITY_5 = objTitle.JobDescription.RESPONSIBILITY_5
            objJobDescriptionData.DETAIL_RESPONSIBILITY_1 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_1
            objJobDescriptionData.DETAIL_RESPONSIBILITY_2 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_2
            objJobDescriptionData.DETAIL_RESPONSIBILITY_3 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_3
            objJobDescriptionData.DETAIL_RESPONSIBILITY_4 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_4
            objJobDescriptionData.DETAIL_RESPONSIBILITY_5 = objTitle.JobDescription.DETAIL_RESPONSIBILITY_5
            objJobDescriptionData.OUT_RESULT_1 = objTitle.JobDescription.OUT_RESULT_1
            objJobDescriptionData.OUT_RESULT_2 = objTitle.JobDescription.OUT_RESULT_2
            objJobDescriptionData.OUT_RESULT_3 = objTitle.JobDescription.OUT_RESULT_3
            objJobDescriptionData.OUT_RESULT_4 = objTitle.JobDescription.OUT_RESULT_4
            objJobDescriptionData.OUT_RESULT_5 = objTitle.JobDescription.OUT_RESULT_5
            objJobDescriptionData.PERMISSION_1 = objTitle.JobDescription.PERMISSION_1
            objJobDescriptionData.PERMISSION_2 = objTitle.JobDescription.PERMISSION_2
            objJobDescriptionData.PERMISSION_3 = objTitle.JobDescription.PERMISSION_3
            objJobDescriptionData.PERMISSION_4 = objTitle.JobDescription.PERMISSION_4
            objJobDescriptionData.PERMISSION_5 = objTitle.JobDescription.PERMISSION_5
            objJobDescriptionData.PERMISSION_6 = objTitle.JobDescription.PERMISSION_6
            objJobDescriptionData.FILE_NAME = objTitle.JobDescription.FILE_NAME
            objJobDescriptionData.UPLOAD_FILE = objTitle.JobDescription.UPLOAD_FILE
            objJobDescriptionData.TITLE_ID = objTitleData.ID
            Context.HU_JOB_DESCRIPTION.AddObject(objJobDescriptionData)
            Context.SaveChanges(log)
            UpdatePositionByID(objTitle)

            If objTitle.EFFECTIVE_DATE <= DateTime.Now Then
                Try
                    Using q As New DataAccess.QueryData()
                        Dim params As New Dictionary(Of String, Object)
                        q.ExecuteStoreProcedureNonQueryTask("PKG_OMS_BUSINESS.JOB_AUTO_UPDATE_TITLE_TEMP", params)
                    End Using
                Catch ex As Exception

                End Try
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function UpdatePositionByID(ByVal obj As TitleDTO,
                                    Optional ByVal OrgIDDefault As Decimal = 1,
                                    Optional ByVal IsDissolveDefault As Decimal = 0) As Boolean
        'im lstOrg As New List(Of Decimal)
        Try
            Dim objTitle = (From p In Context.HU_TITLE_ACTIVITIES Where p.ID = obj.ID).FirstOrDefault
            'Nếu vị trí thêm mới là Pos trưởng
            If objTitle IsNot Nothing AndAlso objTitle.ISOWNER IsNot Nothing AndAlso objTitle.ISOWNER = True Then
                'update QLPD và QLTT của tất cả các Pos cùng đơn vị bằng Pos trưởng
                Dim lstTitle = (From t In Context.HU_TITLE_ACTIVITIES Where t.ORG_ID = obj.ORG_ID And t.ACTFLG = "A" And t.ISOWNER = 0).ToList()
                If lstTitle.Count > 0 Then
                    For Each item In lstTitle
                        Dim objData1 As New HU_TITLE_ACTIVITIES With {.ID = item.ID}
                        objData1 = (From p In Context.HU_TITLE_ACTIVITIES Where p.ID = item.ID).FirstOrDefault
                        If objData1 IsNot Nothing Then
                            objData1.LM = objTitle.ID
                            objData1.CSM = objTitle.ID

                            '// Ghi chậm
                            'objData1.EFFECTIVE_DATE = obj.EFFECTIVE_DATE
                            objData1.TYPE_ACTIVITIES = "UPDATE"
                            Context.SaveChanges()
                        End If
                    Next
                End If
                If objTitle.IS_PLAN = False Then
                    'Update QLTT và QLPD tất cả các Pos là trưởng phòng của tất cả các đơn vị con của đơn vị có Pos mới thêm
                    Dim query = From d In Context.HU_ORGANIZATION_OM Where d.PARENT_ID = obj.ORG_ID

                    Dim lstOrg As List(Of OrganizationDTO)
                    lstOrg = (From th In Context.HU_ORGANIZATION_OM
                              Where th.PARENT_ID = obj.ORG_ID
                              Select New OrganizationDTO With {.ID = th.ID}).ToList

                    If lstOrg.Count > 0 Then
                        For Each item1 In lstOrg
                            Dim lstTitleChild As New List(Of HU_TITLE_ACTIVITIES)
                            lstTitleChild = (From d In Context.HU_TITLE_ACTIVITIES Where d.ORG_ID = item1.ID And d.ISOWNER = -1 And d.ACTFLG = "A").ToList()
                            If lstTitleChild.Count > 0 Then
                                For Each item2 In lstTitleChild
                                    Dim objData1 As New HU_TITLE_ACTIVITIES With {.ID = item2.ID}
                                    objData1 = (From p In Context.HU_TITLE_ACTIVITIES Where p.ID = item2.ID).FirstOrDefault
                                    If objData1 IsNot Nothing Then
                                        objData1.LM = objTitle.ID
                                        objData1.CSM = objTitle.ID

                                        '// Ghi chậm
                                        'objData1.EFFECTIVE_DATE = obj.EFFECTIVE_DATE
                                        objData1.TYPE_ACTIVITIES = "UPDATE"
                                        Context.SaveChanges()
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetPositionID(ByVal ID As Decimal, ByVal log As UserLog, ByVal sLang As String,
                                  Optional ByVal IsReadWrite As Boolean = False,
                                    Optional ByVal OrgIDDefault As Decimal = 1,
                                    Optional ByVal IsDissolveDefault As Decimal = 0) As TitleDTO

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper(),
                                           .P_ORGID = OrgIDDefault,
                                           .P_ISDISSOLVE = IsDissolveDefault})
            End Using

            Dim query
            Dim query2
            If (IsReadWrite) Then
                query = (From p In Context.HU_TITLE
                         From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                         From t In Context.HU_JOB_BAND.Where(Function(f) f.ID = j.JOB_BAND_ID).DefaultIfEmpty
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                         From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                         From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                         From master In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                         From interim In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                         From emp_Lm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER).DefaultIfEmpty
                         From emp_csm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pd.MASTER).DefaultIfEmpty
                         From wt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORKING_TIME).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                         Where p.ID = ID
                         Select New TitleDTO With {
                                              .ID = p.ID,
                                              .CODE = p.CODE,
                                              .JOB_ID = p.JOB_ID,
                                              .JOB_BAND_NAME = If(sLang = "vi-VN", t.NAME_VN, t.NAME_EN),
                                              .NAME_EN = p.NAME_EN,
                                              .NAME_VN = p.NAME_VN,
                                              .ORG_ID = p.ORG_ID,
                                              .ORG_DESC = o.DESCRIPTION_PATH,
                                              .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                              .ORG_NAME = If(sLang = "vi-VN", o.NAME_VN, o.NAME_EN),
                                              .IS_UYBAN = o.UY_BAN,
                                              .IS_NONPHYSICAL = If(p.IS_NONPHYSICAL = -1, True, False),
                                              .IS_PLAN = If(p.IS_PLAN = -1, True, False),
                                              .IS_OWNER = If(p.ISOWNER = -1, True, False),
                                              .LM = p.LM,
                                              .LM_NAME = If(sLang = "vi-VN", tt.CODE & " - " & tt.NAME_VN, tt.CODE & " - " & tt.NAME_EN),
                                              .EMP_LM = If(sLang = "vi-VN", emp_Lm.EMPLOYEE_CODE & " - " & emp_Lm.FULLNAME_VN, emp_Lm.EMPLOYEE_CODE & " - " & emp_Lm.FULLNAME_EN),
                                              .CSM = p.CSM,
                                              .CSM_NAME = If(sLang = "vi-VN", pd.CODE & " - " & pd.NAME_VN, pd.CODE & " - " & pd.NAME_EN),
                                              .EMP_CSM = If(sLang = "vi-VN", emp_csm.EMPLOYEE_CODE & " - " & emp_csm.FULLNAME_VN, emp_csm.EMPLOYEE_CODE & " - " & emp_csm.FULLNAME_EN),
                                              .TITLE_GROUP_ID = j.PHAN_LOAI_ID,
                                              .REMARK = p.REMARK,
                                              .INTERIM = p.INTERIM,
                                              .INTERIM_NAME = If(sLang = "vi-VN", interim.EMPLOYEE_CODE & " - " & interim.FULLNAME_VN, interim.EMPLOYEE_CODE & " - " & interim.FULLNAME_EN),
                                              .JOB_SPEC = p.JOB_SPEC,
                                              .CREATED_DATE = p.CREATED_DATE,
                                              .COST_CENTER = p.COST_CENTER,
                                              .ACTFLG = p.ACTFLG,
                                              .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                              .MASTER = p.MASTER,
                                              .MASTER_NAME = If(sLang = "vi-VN", master.EMPLOYEE_CODE & " - " & master.FULLNAME_VN, master.EMPLOYEE_CODE & " - " & master.FULLNAME_EN),
                                              .LEVEL_ID = p.LEVEL_ID,
                                              .FILENAME = p.FILENAME,
                                              .UPLOADFILE = p.UPLOADFILE,
                                              .WORKING_TIME = p.WORKING_TIME,
                                              .WORKING_TIME_NAME = wt.NAME_VN}).FirstOrDefault

                query2 = (From p In Context.HU_JOB_DESCRIPTION
                          From c In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER).DefaultIfEmpty
                          From l In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE).DefaultIfEmpty
                          From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TDCM).DefaultIfEmpty
                          Where p.TITLE_ID = ID
                          Select New JobDescriptionDTO With {
                                              .ID = p.ID,
                                              .COMPUTER = p.COMPUTER,
                                              .COMPUTER_NAME = c.NAME_VN,
                                              .DETAIL_RESPONSIBILITY_1 = p.DETAIL_RESPONSIBILITY_1,
                                              .DETAIL_RESPONSIBILITY_2 = p.DETAIL_RESPONSIBILITY_2,
                                              .DETAIL_RESPONSIBILITY_3 = p.DETAIL_RESPONSIBILITY_3,
                                              .DETAIL_RESPONSIBILITY_4 = p.DETAIL_RESPONSIBILITY_4,
                                              .DETAIL_RESPONSIBILITY_5 = p.DETAIL_RESPONSIBILITY_5,
                                              .FILE_NAME = p.FILE_NAME,
                                              .INTERNAL_1 = p.INTERNAL_1,
                                              .INTERNAL_2 = p.INTERNAL_2,
                                              .INTERNAL_3 = p.INTERNAL_3,
                                              .JOB_TARGET_1 = p.JOB_TARGET_1,
                                              .JOB_TARGET_2 = p.JOB_TARGET_2,
                                              .JOB_TARGET_3 = p.JOB_TARGET_3,
                                              .JOB_TARGET_4 = p.JOB_TARGET_4,
                                              .JOB_TARGET_5 = p.JOB_TARGET_5,
                                              .JOB_TARGET_6 = p.JOB_TARGET_6,
                                              .LANGUAGE = p.LANGUAGE,
                                              .LANGUAGE_NAME = l.NAME_VN,
                                              .MAJOR_NAME = p.MAJOR,
                                              .OUTSIDE_1 = p.OUTSIDE_1,
                                              .OUTSIDE_2 = p.OUTSIDE_2,
                                              .OUTSIDE_3 = p.OUTSIDE_3,
                                              .OUT_RESULT_1 = p.OUT_RESULT_1,
                                              .OUT_RESULT_2 = p.OUT_RESULT_2,
                                              .OUT_RESULT_3 = p.OUT_RESULT_3,
                                              .OUT_RESULT_4 = p.OUT_RESULT_4,
                                              .OUT_RESULT_5 = p.OUT_RESULT_5,
                                              .PERMISSION_1 = p.PERMISSION_1,
                                              .PERMISSION_2 = p.PERMISSION_2,
                                              .PERMISSION_3 = p.PERMISSION_3,
                                              .PERMISSION_4 = p.PERMISSION_4,
                                              .PERMISSION_5 = p.PERMISSION_5,
                                              .PERMISSION_6 = p.PERMISSION_6,
                                              .RESPONSIBILITY_1 = p.RESPONSIBILITY_1,
                                              .RESPONSIBILITY_2 = p.RESPONSIBILITY_2,
                                              .RESPONSIBILITY_3 = p.RESPONSIBILITY_3,
                                              .RESPONSIBILITY_4 = p.RESPONSIBILITY_4,
                                              .RESPONSIBILITY_5 = p.RESPONSIBILITY_5,
                                              .SUPPORT_SKILL = p.SUPPORT_SKILL,
                                              .TDCM = p.TDCM,
                                              .TDCM_NAME = t.NAME_VN,
                                              .TITLE_ID = p.TITLE_ID,
                                              .UPLOAD_FILE = p.UPLOAD_FILE,
                                              .WORK_EXP = p.WORK_EXP}).FirstOrDefault

                query.JobDescription = query2
            Else
                query = (From p In Context.HU_TITLE
                         From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                         From t In Context.HU_JOB_BAND.Where(Function(f) f.ID = j.JOB_BAND_ID).DefaultIfEmpty
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                         From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                         From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                         From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                         From master In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                         From interim In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                         From emp_Lm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER).DefaultIfEmpty
                         From emp_csm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pd.MASTER).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                         Where p.ID = ID
                         Select New TitleDTO With {
                                              .ID = p.ID,
                                              .CODE = p.CODE,
                                              .JOB_ID = p.JOB_ID,
                                              .JOB_BAND_NAME = If(sLang = "vi-VN", t.NAME_VN, t.NAME_EN),
                                              .NAME_EN = p.NAME_EN,
                                              .NAME_VN = p.NAME_VN,
                                              .ORG_ID = p.ORG_ID,
                                              .ORG_NAME = If(sLang = "vi-VN", o.NAME_VN, o.NAME_EN),
                                              .IS_UYBAN = o.UY_BAN,
                                              .IS_NONPHYSICAL = If(p.IS_NONPHYSICAL = -1, True, False),
                                              .IS_PLAN = If(p.IS_PLAN = -1, True, False),
                                              .IS_OWNER = If(p.ISOWNER = -1, True, False),
                                              .LM = p.LM,
                                              .LM_NAME = If(sLang = "vi-VN", tt.CODE & " - " & tt.NAME_VN, tt.CODE & " - " & tt.NAME_EN),
                                              .EMP_LM = If(sLang = "vi-VN", emp_Lm.EMPLOYEE_CODE & " - " & emp_Lm.FULLNAME_VN, emp_Lm.EMPLOYEE_CODE & " - " & emp_Lm.FULLNAME_EN),
                                              .CSM = p.CSM,
                                              .CSM_NAME = If(sLang = "vi-VN", pd.CODE & " - " & pd.NAME_VN, pd.CODE & " - " & pd.NAME_EN),
                                              .EMP_CSM = If(sLang = "vi-VN", emp_csm.EMPLOYEE_CODE & " - " & emp_csm.FULLNAME_VN, emp_csm.EMPLOYEE_CODE & " - " & emp_csm.FULLNAME_EN),
                                              .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                              .TITLE_GROUP_NAME = If(sLang = "vi-VN", ot.NAME_VN, ot.NAME_EN),
                                              .REMARK = p.REMARK,
                                              .INTERIM = p.INTERIM,
                                              .INTERIM_NAME = If(sLang = "vi-VN", interim.EMPLOYEE_CODE & " - " & interim.FULLNAME_VN, interim.EMPLOYEE_CODE & " - " & interim.FULLNAME_EN),
                                              .JOB_SPEC = p.JOB_SPEC,
                                              .CREATED_DATE = p.CREATED_DATE,
                                              .COST_CENTER = p.COST_CENTER,
                                              .ACTFLG = p.ACTFLG,
                                              .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                              .MASTER = p.MASTER,
                                              .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                              .MASTER_NAME = If(sLang = "vi-VN", master.EMPLOYEE_CODE & " - " & master.FULLNAME_VN, master.EMPLOYEE_CODE & " - " & master.FULLNAME_EN),
                                              .LEVEL_ID = p.LEVEL_ID,
                                              .FILENAME = p.FILENAME,
                                              .UPLOADFILE = p.UPLOADFILE}).FirstOrDefault

                query2 = (From p In Context.HU_JOB_DESCRIPTION
                          From c In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER).DefaultIfEmpty
                          From l In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE).DefaultIfEmpty
                          From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TDCM).DefaultIfEmpty
                          Where p.TITLE_ID = ID
                          Select New JobDescriptionDTO With {
                                              .ID = p.ID,
                                              .COMPUTER = p.COMPUTER,
                                              .COMPUTER_NAME = c.NAME_VN,
                                              .DETAIL_RESPONSIBILITY_1 = p.DETAIL_RESPONSIBILITY_1,
                                              .DETAIL_RESPONSIBILITY_2 = p.DETAIL_RESPONSIBILITY_2,
                                              .DETAIL_RESPONSIBILITY_3 = p.DETAIL_RESPONSIBILITY_3,
                                              .DETAIL_RESPONSIBILITY_4 = p.DETAIL_RESPONSIBILITY_4,
                                              .DETAIL_RESPONSIBILITY_5 = p.DETAIL_RESPONSIBILITY_5,
                                              .FILE_NAME = p.FILE_NAME,
                                              .INTERNAL_1 = p.INTERNAL_1,
                                              .INTERNAL_2 = p.INTERNAL_2,
                                              .INTERNAL_3 = p.INTERNAL_3,
                                              .JOB_TARGET_1 = p.JOB_TARGET_1,
                                              .JOB_TARGET_2 = p.JOB_TARGET_2,
                                              .JOB_TARGET_3 = p.JOB_TARGET_3,
                                              .JOB_TARGET_4 = p.JOB_TARGET_4,
                                              .JOB_TARGET_5 = p.JOB_TARGET_5,
                                              .JOB_TARGET_6 = p.JOB_TARGET_6,
                                              .LANGUAGE = p.LANGUAGE,
                                              .LANGUAGE_NAME = l.NAME_VN,
                                              .MAJOR_NAME = p.MAJOR,
                                              .OUTSIDE_1 = p.OUTSIDE_1,
                                              .OUTSIDE_2 = p.OUTSIDE_2,
                                              .OUTSIDE_3 = p.OUTSIDE_3,
                                              .OUT_RESULT_1 = p.OUT_RESULT_1,
                                              .OUT_RESULT_2 = p.OUT_RESULT_2,
                                              .OUT_RESULT_3 = p.OUT_RESULT_3,
                                              .OUT_RESULT_4 = p.OUT_RESULT_4,
                                              .OUT_RESULT_5 = p.OUT_RESULT_5,
                                              .PERMISSION_1 = p.PERMISSION_1,
                                              .PERMISSION_2 = p.PERMISSION_2,
                                              .PERMISSION_3 = p.PERMISSION_3,
                                              .PERMISSION_4 = p.PERMISSION_4,
                                              .PERMISSION_5 = p.PERMISSION_5,
                                              .PERMISSION_6 = p.PERMISSION_6,
                                              .RESPONSIBILITY_1 = p.RESPONSIBILITY_1,
                                              .RESPONSIBILITY_2 = p.RESPONSIBILITY_2,
                                              .RESPONSIBILITY_3 = p.RESPONSIBILITY_3,
                                              .RESPONSIBILITY_4 = p.RESPONSIBILITY_4,
                                              .RESPONSIBILITY_5 = p.RESPONSIBILITY_5,
                                              .SUPPORT_SKILL = p.SUPPORT_SKILL,
                                              .TDCM = p.TDCM,
                                              .TDCM_NAME = t.NAME_VN,
                                              .TITLE_ID = p.TITLE_ID,
                                              .UPLOAD_FILE = p.UPLOAD_FILE,
                                              .WORK_EXP = p.WORK_EXP}).FirstOrDefault

                query.JobDescription = query2
            End If

            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetPrintJD(ByVal ID As Decimal) As TitleDTO

        Try
            Dim query
            Dim query2
            query = (From p In Context.HU_TITLE
                     From j In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                     From t In Context.HU_JOB_BAND.Where(Function(f) f.ID = j.JOB_BAND_ID).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     From l In Context.HU_LOCATION.Where(Function(f) f.ORG_ID = p.ORG_ID).DefaultIfEmpty
                     From tt In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                     From pd In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                     From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                     From master In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                     From interim In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.INTERIM).DefaultIfEmpty
                     From emp_Lm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER).DefaultIfEmpty
                     From emp_csm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pd.MASTER).DefaultIfEmpty
                     From wt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORKING_TIME).DefaultIfEmpty
                     Where p.ID = ID
                     Select New TitleDTO With {
                                          .ID = p.ID,
                                          .CODE = p.CODE,
                                          .JOB_ID = p.JOB_ID,
                                          .JOB_BAND_NAME = t.NAME_VN,
                                          .NAME_EN = p.NAME_EN,
                                          .NAME_VN = p.NAME_VN,
                                          .ORG_ID = p.ORG_ID,
                                          .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                          .ORG_NAME = o.NAME_VN,
                                          .IS_NONPHYSICAL = If(p.IS_NONPHYSICAL = -1, True, False),
                                          .IS_PLAN = If(p.IS_PLAN = -1, True, False),
                                          .IS_OWNER = If(p.ISOWNER = -1, True, False),
                                          .LM = p.LM,
                                          .LM_NAME = tt.NAME_VN,
                                          .EMP_LM = emp_Lm.FULLNAME_VN,
                                          .CSM = p.CSM,
                                          .CSM_NAME = pd.NAME_VN,
                                          .EMP_CSM = emp_csm.FULLNAME_VN,
                                          .TITLE_GROUP_ID = j.PHAN_LOAI_ID,
                                          .REMARK = p.REMARK,
                                          .INTERIM = p.INTERIM,
                                          .INTERIM_NAME = interim.FULLNAME_VN,
                                          .JOB_SPEC = p.JOB_SPEC,
                                          .CREATED_DATE = p.CREATED_DATE,
                                          .COST_CENTER = p.COST_CENTER,
                                          .ACTFLG = p.ACTFLG,
                                          .COLOR = If(p.ACTFLG = "A", "#070101", "#969696"),
                                          .MASTER = p.MASTER,
                                          .MASTER_NAME = master.FULLNAME_VN,
                                          .LEVEL_ID = p.LEVEL_ID,
                                          .FILENAME = p.FILENAME,
                                          .UPLOADFILE = p.UPLOADFILE,
                                          .WORKING_TIME = p.WORKING_TIME,
                                          .WORKING_TIME_NAME = wt.NAME_VN,
                                          .FILE_LOGO = l.FILE_LOGO,
                                          .ATTACH_FILE_LOGO = l.ATTACH_FILE_LOGO}).FirstOrDefault

            query2 = (From p In Context.HU_JOB_DESCRIPTION
                      From c In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER).DefaultIfEmpty
                      From l In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE).DefaultIfEmpty
                      From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TDCM).DefaultIfEmpty
                      Where p.TITLE_ID = ID
                      Select New JobDescriptionDTO With {
                                          .ID = p.ID,
                                          .COMPUTER = p.COMPUTER,
                                          .COMPUTER_NAME = c.NAME_VN,
                                          .DETAIL_RESPONSIBILITY_1 = p.DETAIL_RESPONSIBILITY_1,
                                          .DETAIL_RESPONSIBILITY_2 = p.DETAIL_RESPONSIBILITY_2,
                                          .DETAIL_RESPONSIBILITY_3 = p.DETAIL_RESPONSIBILITY_3,
                                          .DETAIL_RESPONSIBILITY_4 = p.DETAIL_RESPONSIBILITY_4,
                                          .DETAIL_RESPONSIBILITY_5 = p.DETAIL_RESPONSIBILITY_5,
                                          .FILE_NAME = p.FILE_NAME,
                                          .INTERNAL_1 = p.INTERNAL_1,
                                          .INTERNAL_2 = p.INTERNAL_2,
                                          .INTERNAL_3 = p.INTERNAL_3,
                                          .JOB_TARGET_1 = p.JOB_TARGET_1,
                                          .JOB_TARGET_2 = p.JOB_TARGET_2,
                                          .JOB_TARGET_3 = p.JOB_TARGET_3,
                                          .JOB_TARGET_4 = p.JOB_TARGET_4,
                                          .JOB_TARGET_5 = p.JOB_TARGET_5,
                                          .JOB_TARGET_6 = p.JOB_TARGET_6,
                                          .LANGUAGE = p.LANGUAGE,
                                          .LANGUAGE_NAME = l.NAME_VN,
                                          .MAJOR_NAME = p.MAJOR,
                                          .OUTSIDE_1 = p.OUTSIDE_1,
                                          .OUTSIDE_2 = p.OUTSIDE_2,
                                          .OUTSIDE_3 = p.OUTSIDE_3,
                                          .OUT_RESULT_1 = p.OUT_RESULT_1,
                                          .OUT_RESULT_2 = p.OUT_RESULT_2,
                                          .OUT_RESULT_3 = p.OUT_RESULT_3,
                                          .OUT_RESULT_4 = p.OUT_RESULT_4,
                                          .OUT_RESULT_5 = p.OUT_RESULT_5,
                                          .PERMISSION_1 = p.PERMISSION_1,
                                          .PERMISSION_2 = p.PERMISSION_2,
                                          .PERMISSION_3 = p.PERMISSION_3,
                                          .PERMISSION_4 = p.PERMISSION_4,
                                          .PERMISSION_5 = p.PERMISSION_5,
                                          .PERMISSION_6 = p.PERMISSION_6,
                                          .RESPONSIBILITY_1 = p.RESPONSIBILITY_1,
                                          .RESPONSIBILITY_2 = p.RESPONSIBILITY_2,
                                          .RESPONSIBILITY_3 = p.RESPONSIBILITY_3,
                                          .RESPONSIBILITY_4 = p.RESPONSIBILITY_4,
                                          .RESPONSIBILITY_5 = p.RESPONSIBILITY_5,
                                          .SUPPORT_SKILL = p.SUPPORT_SKILL,
                                          .TDCM = p.TDCM,
                                          .TDCM_NAME = t.NAME_VN,
                                          .TITLE_ID = p.TITLE_ID,
                                          .UPLOAD_FILE = p.UPLOAD_FILE,
                                          .WORK_EXP = p.WORK_EXP}).FirstOrDefault
            query.JobDescription = query2
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetPositions(ByVal _filter As TitleDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        Try
            Dim query = From p In Context.HU_TITLE
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        Select New TitleDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_EN = p.NAME_EN,
                                   .NAME_VN = p.NAME_VN,
                                   .REMARK = p.REMARK,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                   .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                   .TITLE_GROUP_NAME = group.NAME_VN}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOrgTreeApp(ByVal sLang As String, ByVal log As UserLog) As List(Of OrganizationDTO)

        Dim lst As List(Of OrganizationDTO) = New List(Of OrganizationDTO)
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_BUSINESS.GET_ORG_TREE_APP",
                                         New With {.P_LANGUAGE = sLang,
                                                   .P_USERNAME = log.Username,
                                                   .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of OrganizationDTO)()
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOrgOMByID(ByVal _Id As Decimal) As OrganizationDTO
        Try
            Dim CompanyLevel = GetCompanyLevel(_Id)

            Dim Id As Decimal
            If _Id < 0 Then
                Id = 0 - _Id
            Else
                Id = _Id
            End If
            Dim query = (From p In Context.HU_ORGANIZATION
                         From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ID).DefaultIfEmpty
                         From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = o.ORG_ID2).DefaultIfEmpty
                         Where p.ID = Id
                         Select New OrganizationDTO With {.ID = p.ID,
                                                         .CODE = p.CODE,
                                                         .NAME_VN = p.NAME_VN,
                                                         .ORG_NAME2 = o2.NAME_VN,
                                                         .NAME_EN = p.NAME_EN,
                                                            .ORG_LEVEL_NAME = CompanyLevel,
                                                         .INFOR_1 = o2.SHORT_NAME}).FirstOrDefault
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AutoGenCodeHuTile(ByVal tableName As String, ByVal colName As String) As String
        Try
            '// Chặn ký tự ko phải chữ hoặc số
            If (Not tableName.All(Function(f) Char.IsLetterOrDigit(f) Or f = "_" Or f = ".") Or Not colName.All(Function(f) Char.IsLetterOrDigit(f) Or f = "_" Or f = ".")) Then
                Return "00000"
            End If

            '// SQL bên dưới có nguy cơ SQL Injection
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & "00000') FROM " & tableName & " WHERE " & colName & " LIKE '" & "0%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return "00001"
            End If
            Dim number = Decimal.Parse(str)
            number = number + 1
            Dim lastChar = Format(number, "00000")
            Return lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CheckIsOwner(ByVal OrgId As Decimal) As Boolean
        Try
            If OrgId > 0 Then
                Dim exits = From t In Context.HU_TITLE_ACTIVITIES
                            Where t.IS_PLAN = 0 And t.ISOWNER = -1 And t.ACTFLG = "A" And t.ORG_ID = OrgId
                If exits.Count > 0 Then
                    Return True
                End If
                Return False
            Else
                Dim exits = From t In Context.HU_TITLE_ACTIVITIES
                            Where t.IS_PLAN = -1 And t.ISOWNER = -1 And t.ACTFLG = "A" And t.ORG_ID = OrgId
                If exits.Count > 0 Then
                    Return True
                End If
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ModifyTitleById(ByVal obj As TitleDTO, ByVal OrgRight As Decimal, ByVal Address As Decimal, ByVal log As UserLog,
                                   Optional ByVal OrgIDDefault As Decimal = 1,
                                   Optional ByVal IsDissolveDefault As Decimal = 0) As Boolean

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper(),
                                           .P_ORGID = OrgIDDefault,
                                           .P_ISDISSOLVE = IsDissolveDefault})
            End Using
            '// Check quyen du lieu
            Dim objChk As List(Of Decimal) = (From p In Context.HU_TITLE
                                              From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                                              Where p.ID = obj.ID
                                              Select chosen.ORG_ID).ToList()
            If OrgRight > 0 Then
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.TITLE_ID = obj.ID).FirstOrDefault
                If emp IsNot Nothing Then
                    emp.ORG_ID = OrgRight
                End If
            End If
            If (objChk.Count < 1) Then
                Return False
            End If

            objChk = (From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = OrgRight And f.USERNAME = log.Username.ToUpper())
                      Select chosen.ORG_ID).ToList()
            If (objChk.Count < 1) Then
                Return False
            End If

            Dim sDate As DateTime = Date.Now
            Dim lstOrg As New List(Of Decimal)
            Dim lstTitle As New List(Of HU_TITLE)
            Dim OrgIdRight As Decimal
            If OrgRight < 0 Then
                OrgIdRight = 0 - OrgRight
            Else
                OrgIdRight = OrgRight
            End If
            If obj.IS_OWNER = True Then
                'Nếu vị trí trước điều chuyển ngoài kho
                If obj.IS_PLAN_LEFT = 0 Then
                    'Lấy Pos là trưởng phòng của đơn vị cha nếu có
                    Dim OrgParentLeft = (From o In Context.HU_ORGANIZATION Where o.ID = obj.ORG_ID
                                         Select o.PARENT_ID).FirstOrDefault
                    If OrgParentLeft IsNot Nothing Then
                        Dim TitleId = (From t In Context.HU_TITLE
                                       Where t.ORG_ID = OrgParentLeft And t.ISOWNER = -1 And t.ACTFLG = "A" And t.IS_PLAN = 0
                                       Select t.ID).FirstOrDefault
                        If TitleId <> 0 Then
                            'update QLTT, QLPD của tất cả các Pos cùng đơn vị có Pos chuyển đi bằng cha
                            Dim lstTitleChildLeft = (From d In Context.HU_TITLE
                                                     Where (d.LM = obj.ID Or d.CSM = obj.ID) _
                                                     And d.ACTFLG = "A").ToList()
                            If lstTitleChildLeft.Count > 0 Then
                                For Each obj5 In lstTitleChildLeft
                                    If obj5.LM = obj.ID And obj5.CSM = obj.ID Then
                                        Dim objData7 As New HU_TITLE With {.ID = obj5.ID}
                                        objData7 = (From p In Context.HU_TITLE Where p.ID = obj5.ID).FirstOrDefault
                                        If objData7 IsNot Nothing Then
                                            'objData7.LM = TitleId
                                            'objData7.CSM = TitleId
                                            objData7.TYPE_ACTIVITIES = "UPDATE"
                                            objData7.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If
                                    End If
                                    If obj5.LM = obj.ID Then
                                        Dim objData8 As New HU_TITLE With {.ID = obj5.ID}
                                        objData8 = (From p In Context.HU_TITLE Where p.ID = obj5.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData8)
                                        If objData8 IsNot Nothing Then
                                            'objData8.LM = TitleId
                                            objData8.TYPE_ACTIVITIES = "UPDATE"
                                            objData8.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If

                                        Dim objData8HU As New HU_TITLE With {.ID = obj5.ID}
                                        objData8HU = (From p In Context.HU_TITLE Where p.ID = obj5.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData8)
                                        If objData8HU IsNot Nothing Then
                                            'objData8HU.LM = TitleId
                                            Context.SaveChanges(log)
                                        End If

                                    End If
                                    If obj5.CSM = obj.ID Then
                                        Dim objData9 As New HU_TITLE With {.ID = obj5.ID}
                                        objData9 = (From p In Context.HU_TITLE Where p.ID = obj5.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData9)
                                        If objData9 IsNot Nothing Then
                                            'objData9.CSM = TitleId
                                            objData9.TYPE_ACTIVITIES = "UPDATE"
                                            objData9.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If

                                        Dim objData9HU As New HU_TITLE With {.ID = obj5.ID}
                                        objData9HU = (From p In Context.HU_TITLE Where p.ID = obj5.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData9)
                                        If objData9HU IsNot Nothing Then
                                            'objData9HU.CSM = TitleId
                                            Context.SaveChanges(log)
                                        End If
                                    End If
                                Next
                            End If

                        Else
                            'Nếu Pos cha k có thì update QLTT, QLPD của tất cả các Pos cùng đơn vị có Pos chuyển đi bằng null
                            Dim lstTitleChildLeft = (From d In Context.HU_TITLE
                                                     Where (d.LM = obj.ID Or d.CSM = obj.ID) _
                                                     And d.ACTFLG = "A").ToList()
                            If lstTitleChildLeft.Count > 0 Then
                                For Each obj7 In lstTitleChildLeft
                                    If obj7.LM = obj.ID And obj7.CSM = obj.ID Then
                                        Dim objData13 As New HU_TITLE With {.ID = obj7.ID}
                                        objData13 = (From p In Context.HU_TITLE Where p.ID = obj7.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData7)
                                        If objData13 IsNot Nothing Then
                                            'objData13.LM = Nothing
                                            'objData13.CSM = Nothing
                                            objData13.TYPE_ACTIVITIES = "UPDATE"
                                            objData13.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If
                                        'Update HU_TITLE
                                        Dim objData13HU As New HU_TITLE With {.ID = obj7.ID}
                                        objData13HU = (From p In Context.HU_TITLE Where p.ID = obj7.ID).FirstOrDefault
                                        If objData13HU IsNot Nothing Then
                                            'objData13HU.LM = Nothing
                                            'objData13HU.CSM = Nothing
                                            Context.SaveChanges(log)
                                        End If

                                    End If
                                    If obj7.LM = obj.ID Then
                                        Dim objData14 As New HU_TITLE With {.ID = obj7.ID}
                                        objData14 = (From p In Context.HU_TITLE Where p.ID = obj7.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData8)
                                        If objData14 IsNot Nothing Then
                                            'objData14.LM = Nothing
                                            objData14.TYPE_ACTIVITIES = "UPDATE"
                                            objData14.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If

                                        Dim objData14HU As New HU_TITLE With {.ID = obj7.ID}
                                        objData14HU = (From p In Context.HU_TITLE Where p.ID = obj7.ID).FirstOrDefault
                                        If objData14HU IsNot Nothing Then
                                            'objData14HU.LM = Nothing
                                            Context.SaveChanges(log)
                                        End If

                                    End If
                                    If obj7.CSM = obj.ID Then
                                        Dim objData15 As New HU_TITLE With {.ID = obj7.ID}
                                        objData15 = (From p In Context.HU_TITLE Where p.ID = obj7.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData9)
                                        If objData15 IsNot Nothing Then
                                            'objData15.CSM = Nothing
                                            objData15.TYPE_ACTIVITIES = "UPDATE"
                                            objData15.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If

                                        Dim objData15HU As New HU_TITLE With {.ID = obj7.ID}
                                        objData15HU = (From p In Context.HU_TITLE Where p.ID = obj7.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData9)
                                        If objData15HU IsNot Nothing Then
                                            'objData15HU.CSM = Nothing
                                            Context.SaveChanges(log)
                                        End If

                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
                'check xem đơn vị có Pos chuyển đến có đơn vị cha hay k?
                Dim OrgParent = (From o In Context.HU_ORGANIZATION Where o.ID = OrgIdRight
                                 Select o.PARENT_ID).FirstOrDefault
                'Nếu có Org cha
                If OrgParent IsNot Nothing Then
                    Dim TitleId = (From t In Context.HU_TITLE
                                   Where t.ORG_ID = OrgParent And t.ISOWNER = -1 And t.ACTFLG = "A" And t.IS_PLAN = 0
                                   Select t.ID).FirstOrDefault
                    If TitleId <> 0 Then
                        Dim objData As New HU_TITLE With {.ID = obj.ID}
                        objData = (From p In Context.HU_TITLE Where p.ID = obj.ID).FirstOrDefault
                        'Context.HU_TITLE.Attach(objData)
                        objData.ORG_ID = OrgIdRight
                        objData.IS_PLAN = obj.IS_PLAN
                        objData.WORK_LOCATION = Address
                        'objData.LM = TitleId
                        'objData.CSM = TitleId
                        objData.TYPE_ACTIVITIES = "UPDATE"
                        objData.EFFECTIVE_DATE = sDate
                        Context.SaveChanges(log)

                    Else
                        Dim objData As New HU_TITLE With {.ID = obj.ID}
                        objData = (From p In Context.HU_TITLE Where p.ID = obj.ID).FirstOrDefault
                        'Context.HU_TITLE.Attach(objData)
                        objData.ORG_ID = OrgIdRight
                        objData.IS_PLAN = obj.IS_PLAN
                        objData.WORK_LOCATION = Address
                        'objData.LM = Nothing
                        'objData.CSM = Nothing
                        objData.TYPE_ACTIVITIES = "UPDATE"
                        objData.EFFECTIVE_DATE = sDate
                        Context.SaveChanges(log)

                    End If
                End If
                If obj.ORG_ID <> OrgRight Then
                    'update QLPD và QLTT của tất cả các Pos cùng đơn vị có Pos chuyển đến
                    lstTitle = (From t In Context.HU_TITLE Where t.ORG_ID = OrgIdRight And t.ISOWNER = 0 And t.ACTFLG = "A").ToList()
                    If lstTitle.Count > 0 Then
                        For Each it1 In lstTitle
                            Dim objData1 As New HU_TITLE With {.ID = it1.ID}
                            objData1 = (From p In Context.HU_TITLE Where p.ID = it1.ID).FirstOrDefault
                            'Context.HU_TITLE.Attach(objData1)
                            If objData1 IsNot Nothing Then
                                'objData1.LM = obj.ID
                                'objData1.CSM = obj.ID
                                objData1.TYPE_ACTIVITIES = "UPDATE"
                                objData1.EFFECTIVE_DATE = sDate
                                Context.SaveChanges(log)
                            End If

                            Dim objData1HU As New HU_TITLE With {.ID = it1.ID}
                            objData1HU = (From p In Context.HU_TITLE Where p.ID = it1.ID).FirstOrDefault
                            'Context.HU_TITLE.Attach(objData1)
                            If objData1HU IsNot Nothing Then
                                'objData1HU.LM = obj.ID
                                'objData1HU.CSM = obj.ID
                                Context.SaveChanges(log)
                            End If
                        Next
                    End If
                    If obj.IS_PLAN = False Then
                        'Update tất cả các Pos là trưởng phòng của tất cả các đơn vị con của đơn vị có Pos chuyển đến
                        lstOrg = (From o In Context.HU_ORGANIZATION
                                  Where o.PARENT_ID = OrgIdRight
                                  Select o.ID).ToList()
                        If lstOrg.Count > 0 Then
                            For Each it In lstOrg
                                Dim lstTitleChild As New List(Of HU_TITLE)
                                lstTitleChild = (From d In Context.HU_TITLE Where d.ORG_ID = it And d.ISOWNER = -1 And d.ACTFLG = "A").ToList()
                                If lstTitleChild.Count > 0 Then
                                    For Each it2 In lstTitleChild
                                        Dim objData2 As New HU_TITLE With {.ID = it2.ID}
                                        objData2 = (From p In Context.HU_TITLE Where p.ID = it2.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData2)
                                        If objData2 IsNot Nothing Then
                                            'objData2.LM = obj.ID
                                            'objData2.CSM = obj.ID
                                            objData2.TYPE_ACTIVITIES = "UPDATE"
                                            objData2.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If

                                        Dim objData2HU As New HU_TITLE With {.ID = it2.ID}
                                        objData2HU = (From p In Context.HU_TITLE Where p.ID = it2.ID).FirstOrDefault
                                        'Context.HU_TITLE.Attach(objData2)
                                        If objData2HU IsNot Nothing Then
                                            'objData2HU.LM = obj.ID
                                            'objData2HU.CSM = obj.ID
                                            Context.SaveChanges(log)
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                Else
                    'Điều chuyển từ kế hoạch ra ngoài và đó là trưởng thì update lại QLTT,QLTT các vị trí = trưởng chuyển đến
                    If obj.IS_OWNER = True And obj.IS_PLAN = False Then
                        lstTitle = (From t In Context.HU_TITLE Where t.ORG_ID = OrgIdRight And t.ISOWNER = 0 And t.ACTFLG = "A").ToList()
                        If lstTitle.Count > 0 Then
                            For Each it1 In lstTitle
                                Dim objData1 As New HU_TITLE With {.ID = it1.ID}
                                objData1 = (From p In Context.HU_TITLE Where p.ID = it1.ID).FirstOrDefault
                                If objData1 IsNot Nothing Then
                                    'objData1.LM = obj.ID
                                    'objData1.CSM = obj.ID
                                    objData1.TYPE_ACTIVITIES = "UPDATE"
                                    objData1.EFFECTIVE_DATE = sDate
                                    Context.SaveChanges(log)
                                End If

                                Dim objData1HU As New HU_TITLE With {.ID = it1.ID}
                                objData1HU = (From p In Context.HU_TITLE Where p.ID = it1.ID).FirstOrDefault
                                If objData1HU IsNot Nothing Then
                                    'objData1HU.LM = obj.ID
                                    'objData1HU.CSM = obj.ID
                                    Context.SaveChanges(log)
                                End If
                            Next
                        End If
                    End If

                    'Điều chuyển trưởng vào kho thì update lại QLTT, QLTT của tất cả các vị trí = trưởng cao hơn
                    If obj.IS_OWNER = True And obj.IS_PLAN = True Then
                        Dim OrgParentA = (From o In Context.HU_ORGANIZATION Where o.ID = obj.ORG_ID
                                          Select o.PARENT_ID).FirstOrDefault
                        If OrgParentA IsNot Nothing Then
                            Dim TitleLeftParent = (From t In Context.HU_TITLE Where t.ORG_ID = OrgParentA And t.ISOWNER = -1 And t.IS_PLAN = 0 And t.ACTFLG = "A"
                                                   Select t.ID).FirstOrDefault
                            If TitleLeftParent <> 0 Then
                                'Update QLTT, QLPD của tất cả các Pos có nó là QLTT hoặc QLPD bằng Pos trưởng Org cha
                                lstTitle = (From t In Context.HU_TITLE Where t.ORG_ID = OrgIdRight And t.ISOWNER = 0 And t.ACTFLG = "A").ToList()
                                If lstTitle.Count > 0 Then
                                    For Each it1 In lstTitle
                                        Dim objData1 As New HU_TITLE With {.ID = it1.ID}
                                        objData1 = (From p In Context.HU_TITLE Where p.ID = it1.ID).FirstOrDefault
                                        If objData1 IsNot Nothing Then
                                            'objData1.LM = TitleLeftParent
                                            'objData1.CSM = TitleLeftParent
                                            objData1.TYPE_ACTIVITIES = "UPDATE"
                                            objData1.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If

                                        Dim objData1HU As New HU_TITLE With {.ID = it1.ID}
                                        objData1HU = (From p In Context.HU_TITLE Where p.ID = it1.ID).FirstOrDefault
                                        If objData1HU IsNot Nothing Then
                                            'objData1HU.LM = TitleLeftParent
                                            'objData1HU.CSM = TitleLeftParent
                                            Context.SaveChanges(log)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                'Nếu trước điều chuyển là thường ngoài kho
                If obj.IS_PLAN_LEFT = False Then
                    'Lấy ra trưởng của nó
                    Dim TitleParentLeft = (From d In Context.HU_TITLE Where d.ORG_ID = obj.ORG_ID And d.ISOWNER = -1 And d.IS_PLAN = 0 And d.ACTFLG = "A"
                                           Select d.ID).FirstOrDefault
                    'Nếu có Pos trưởng
                    If TitleParentLeft <> 0 Then
                        'Update QLTT, QLPD của tất cả các Pos có nó là QLTT hoặc QLPD bằng Pos trưởng
                        Dim lstTitleLeft = (From d In Context.HU_TITLE
                                            Where (d.LM = obj.ID Or d.CSM = obj.ID) _
                                            And d.ACTFLG = "A").ToList()
                        If lstTitleLeft.Count > 0 Then
                            For Each obj4 In lstTitleLeft
                                If obj4.LM = obj.ID And obj4.CSM = obj.ID Then
                                    Dim objData4 As New HU_TITLE With {.ID = obj4.ID}
                                    objData4 = (From p In Context.HU_TITLE Where p.ID = obj4.ID).FirstOrDefault
                                    'Context.HU_TITLE.Attach(objData4)
                                    If objData4 IsNot Nothing Then
                                        'objData4.LM = TitleParentLeft
                                        'objData4.CSM = TitleParentLeft
                                        objData4.TYPE_ACTIVITIES = "UPDATE"
                                        objData4.EFFECTIVE_DATE = sDate
                                        Context.SaveChanges(log)
                                    End If

                                    Dim objData4HU As New HU_TITLE With {.ID = obj4.ID}
                                    objData4HU = (From p In Context.HU_TITLE Where p.ID = obj4.ID).FirstOrDefault
                                    'Context.HU_TITLE.Attach(objData4)
                                    If objData4HU IsNot Nothing Then
                                        'objData4HU.LM = TitleParentLeft
                                        'objData4HU.CSM = TitleParentLeft
                                        Context.SaveChanges(log)
                                    End If

                                End If
                                If obj4.LM = obj.ID Then
                                    Dim objData5 As New HU_TITLE With {.ID = obj4.ID}
                                    objData5 = (From p In Context.HU_TITLE Where p.ID = obj4.ID).FirstOrDefault
                                    'Context.HU_TITLE.Attach(objData5)
                                    If objData5 IsNot Nothing Then
                                        'objData5.LM = TitleParentLeft
                                        objData5.TYPE_ACTIVITIES = "UPDATE"
                                        objData5.EFFECTIVE_DATE = sDate
                                        Context.SaveChanges(log)
                                    End If

                                    Dim objData5HU As New HU_TITLE With {.ID = obj4.ID}
                                    objData5HU = (From p In Context.HU_TITLE Where p.ID = obj4.ID).FirstOrDefault
                                    'Context.HU_TITLE.Attach(objData5)
                                    If objData5HU IsNot Nothing Then
                                        'objData5HU.LM = TitleParentLeft
                                        Context.SaveChanges(log)
                                    End If

                                End If
                                If obj4.CSM = obj.ID Then
                                    Dim objData6 As New HU_TITLE With {.ID = obj4.ID}
                                    objData6 = (From p In Context.HU_TITLE Where p.ID = obj4.ID).FirstOrDefault
                                    'Context.HU_TITLE.Attach(objData6)
                                    If objData6 IsNot Nothing Then
                                        'objData6.CSM = TitleParentLeft
                                        objData6.TYPE_ACTIVITIES = "UPDATE"
                                        objData6.EFFECTIVE_DATE = sDate
                                        Context.SaveChanges(log)
                                    End If

                                    Dim objData6HU As New HU_TITLE With {.ID = obj4.ID}
                                    objData6HU = (From p In Context.HU_TITLE Where p.ID = obj4.ID).FirstOrDefault
                                    'Context.HU_TITLE.Attach(objData6)
                                    If objData6HU IsNot Nothing Then
                                        'objData6HU.CSM = TitleParentLeft
                                        Context.SaveChanges(log)
                                    End If
                                End If
                            Next
                        End If
                    Else
                        'Nếu k có Pos trưởng thì lấy Pos trưởng Org cha
                        Dim OrgParent = (From o In Context.HU_ORGANIZATION Where o.ID = obj.ORG_ID
                                         Select o.PARENT_ID).FirstOrDefault
                        If OrgParent IsNot Nothing Then
                            Dim TitleLeftParent = (From t In Context.HU_TITLE Where t.ORG_ID = OrgParent And t.ISOWNER = -1 And t.IS_PLAN = 0 And t.ACTFLG = "A"
                                                   Select t.ID).FirstOrDefault
                            If TitleLeftParent <> 0 Then
                                'Update QLTT, QLPD của tất cả các Pos có nó là QLTT hoặc QLPD bằng Pos trưởng Org cha
                                Dim lstTitleLeft = (From d In Context.HU_TITLE
                                                    Where (d.LM = obj.ID Or d.CSM = obj.ID) _
                                                    And d.ACTFLG = "A").ToList()
                                If lstTitleLeft.Count > 0 Then
                                    For Each obj6 In lstTitleLeft
                                        If obj6.LM = obj.ID And obj6.CSM = obj.ID Then
                                            Dim objData10 As New HU_TITLE With {.ID = obj6.ID}
                                            objData10 = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData4)
                                            If objData10 IsNot Nothing Then
                                                'objData10.LM = TitleLeftParent
                                                'objData10.CSM = TitleLeftParent
                                                objData10.TYPE_ACTIVITIES = "UPDATE"
                                                objData10.EFFECTIVE_DATE = sDate
                                                Context.SaveChanges(log)
                                            End If

                                            Dim objData10HU As New HU_TITLE With {.ID = obj6.ID}
                                            objData10HU = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData4)
                                            If objData10HU IsNot Nothing Then
                                                'objData10HU.LM = TitleLeftParent
                                                'objData10HU.CSM = TitleLeftParent
                                                Context.SaveChanges(log)
                                            End If

                                        End If
                                        If obj6.LM = obj.ID Then
                                            Dim objData11 As New HU_TITLE With {.ID = obj6.ID}
                                            objData11 = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData5)
                                            If objData11 IsNot Nothing Then
                                                'objData11.LM = TitleLeftParent
                                                objData11.TYPE_ACTIVITIES = "UPDATE"
                                                objData11.EFFECTIVE_DATE = sDate
                                                Context.SaveChanges(log)
                                            End If

                                            Dim objData11HU As New HU_TITLE With {.ID = obj6.ID}
                                            objData11HU = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData5)
                                            If objData11HU IsNot Nothing Then
                                                'objData11HU.LM = TitleLeftParent
                                                Context.SaveChanges(log)
                                            End If

                                        End If
                                        If obj6.CSM = obj.ID Then
                                            Dim objData12 As New HU_TITLE With {.ID = obj6.ID}
                                            objData12 = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData6)
                                            If objData12 IsNot Nothing Then
                                                'objData12.CSM = TitleLeftParent
                                                objData12.TYPE_ACTIVITIES = "UPDATE"
                                                objData12.EFFECTIVE_DATE = sDate
                                                Context.SaveChanges(log)
                                            End If

                                            Dim objData12HU As New HU_TITLE With {.ID = obj6.ID}
                                            objData12HU = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData6)
                                            If objData12HU IsNot Nothing Then
                                                'objData12HU.CSM = TitleLeftParent
                                                Context.SaveChanges(log)
                                            End If

                                        End If
                                    Next
                                End If
                            Else
                                'Nếu Pos trưởng Org cha k có thì Update QLTT, QLPD của tất cả các Pos có nó là QLTT hoặc QLPD bằng null
                                Dim lstTitleLeft = (From d In Context.HU_TITLE
                                                    Where (d.LM = obj.ID Or d.CSM = obj.ID) _
                                                    And d.ACTFLG = "A").ToList()
                                If lstTitleLeft.Count > 0 Then
                                    For Each obj6 In lstTitleLeft
                                        If obj6.LM = obj.ID And obj6.CSM = obj.ID Then
                                            Dim objData10 As New HU_TITLE With {.ID = obj6.ID}
                                            objData10 = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData4)
                                            If objData10 IsNot Nothing Then
                                                'objData10.LM = Nothing
                                                'objData10.CSM = Nothing
                                                objData10.TYPE_ACTIVITIES = "UPDATE"
                                                objData10.EFFECTIVE_DATE = sDate
                                                Context.SaveChanges(log)
                                            End If

                                            Dim objData10HU As New HU_TITLE With {.ID = obj6.ID}
                                            objData10HU = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData4)
                                            If objData10HU IsNot Nothing Then
                                                'objData10HU.LM = Nothing
                                                'objData10HU.CSM = Nothing
                                                Context.SaveChanges(log)
                                            End If

                                        End If
                                        If obj6.LM = obj.ID Then
                                            Dim objData11 As New HU_TITLE With {.ID = obj6.ID}
                                            objData11 = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData5)
                                            If objData11 IsNot Nothing Then
                                                'objData11.LM = Nothing
                                                objData11.TYPE_ACTIVITIES = "UPDATE"
                                                objData11.EFFECTIVE_DATE = sDate
                                                Context.SaveChanges(log)
                                            End If

                                            Dim objData11HU As New HU_TITLE With {.ID = obj6.ID}
                                            objData11HU = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData5)
                                            If objData11HU IsNot Nothing Then
                                                'objData11HU.LM = Nothing
                                                Context.SaveChanges(log)
                                            End If

                                        End If
                                        If obj6.CSM = obj.ID Then
                                            Dim objData12 As New HU_TITLE With {.ID = obj6.ID}
                                            objData12 = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData6)
                                            If objData12 IsNot Nothing Then
                                                'objData12.CSM = Nothing
                                                objData12.TYPE_ACTIVITIES = "UPDATE"
                                                objData12.EFFECTIVE_DATE = sDate
                                                Context.SaveChanges(log)
                                            End If

                                            Dim objData12HU As New HU_TITLE With {.ID = obj6.ID}
                                            objData12HU = (From p In Context.HU_TITLE Where p.ID = obj6.ID).FirstOrDefault
                                            'Context.HU_TITLE.Attach(objData6)
                                            If objData12HU IsNot Nothing Then
                                                'objData12HU.CSM = Nothing
                                                Context.SaveChanges(log)
                                            End If

                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If
                'Lấy Pos trưởng của đơn vị sau điều chuyển 
                Dim TitleId = (From d In Context.HU_TITLE
                               Where d.ORG_ID = OrgIdRight And d.ISOWNER = -1 And d.IS_PLAN = 0 And d.ACTFLG = "A"
                               Select d.ID).FirstOrDefault
                'Nếu có Pos trưởng thì QLTT, QLPD của nó là Pos trường
                If TitleId <> 0 Then
                    Dim objData As New HU_TITLE With {.ID = obj.ID}
                    objData = (From p In Context.HU_TITLE Where p.ID = obj.ID).FirstOrDefault
                    If objData IsNot Nothing Then
                        'Context.HU_TITLE.Attach(objData)
                        objData.ORG_ID = OrgIdRight
                        objData.IS_PLAN = obj.IS_PLAN
                        objData.WORK_LOCATION = Address
                        'objData.LM = TitleId
                        'objData.CSM = TitleId
                        objData.TYPE_ACTIVITIES = "UPDATE"
                        objData.EFFECTIVE_DATE = sDate
                        Context.SaveChanges(log)
                    End If
                Else
                    'Nếu k có Pos trường thì lấy Pos trưởng của Org cha
                    Dim OrgRightParent = (From o In Context.HU_ORGANIZATION Where o.ID = OrgIdRight
                                          Select o.PARENT_ID).FirstOrDefault
                    If OrgRightParent IsNot Nothing Then
                        Dim TitleIdParent = (From d In Context.HU_TITLE
                                             Where d.ORG_ID = OrgRightParent And d.ISOWNER = -1 And d.IS_PLAN = 0 And d.ACTFLG = "A"
                                             Select d.ID).FirstOrDefault
                        'Nếu có Pos trưởng Org cha thì QLTT, QLPD của nó là Pos trưởng Org cha
                        If TitleIdParent <> 0 Then
                            Dim objData As New HU_TITLE With {.ID = obj.ID}
                            objData = (From p In Context.HU_TITLE Where p.ID = obj.ID).FirstOrDefault
                            If objData IsNot Nothing Then
                                'Context.HU_TITLE.Attach(objData)
                                objData.ORG_ID = OrgIdRight
                                objData.IS_PLAN = obj.IS_PLAN
                                objData.WORK_LOCATION = Address
                                'objData.LM = TitleIdParent
                                'objData.CSM = TitleIdParent
                                objData.TYPE_ACTIVITIES = "UPDATE"
                                objData.EFFECTIVE_DATE = sDate
                                Context.SaveChanges(log)
                            End If
                        Else
                            'Nếu k có Pos trưởng Org cha thì QLTT, QLPD của nó là null
                            Dim objData As New HU_TITLE With {.ID = obj.ID}
                            objData = (From p In Context.HU_TITLE Where p.ID = obj.ID).FirstOrDefault
                            'Context.HU_TITLE.Attach(objData)
                            If objData IsNot Nothing Then
                                objData.ORG_ID = OrgIdRight
                                objData.IS_PLAN = obj.IS_PLAN
                                objData.WORK_LOCATION = Address
                                'objData.LM = Nothing
                                'objData.CSM = Nothing
                                objData.TYPE_ACTIVITIES = "UPDATE"
                                objData.EFFECTIVE_DATE = sDate
                                Context.SaveChanges(log)
                            End If
                        End If
                    Else
                        'Nếu k có Pos trưởng Org cha thì QLTT, QLPD của nó là null
                        Dim objData As New HU_TITLE With {.ID = obj.ID}
                        objData = (From p In Context.HU_TITLE Where p.ID = obj.ID).FirstOrDefault
                        'Context.HU_TITLE.Attach(objData)
                        If objData IsNot Nothing Then
                            objData.ORG_ID = OrgIdRight
                            objData.IS_PLAN = obj.IS_PLAN
                            objData.WORK_LOCATION = Address
                            'objData.LM = Nothing
                            'objData.CSM = Nothing
                            objData.TYPE_ACTIVITIES = "UPDATE"
                            objData.EFFECTIVE_DATE = sDate
                            Context.SaveChanges(log)
                        End If
                    End If
                End If
            End If
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_OMS_BUSINESS.JOB_AUTO_UPDATE_TITLE_TEMP")
            End Using
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyTitleById")
            Throw ex
        End Try
    End Function
    Public Function GetPositionByOrgID(ByVal _filter As TitleDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "IS_OWNER") As List(Of TitleDTO)

        Try
            Dim str As String = "(vacant)"
            Dim str1 As String = " (in hiring process)"
            Dim str2 As String = " (concurrent)"
            Dim str3 As String = ""
            If _filter.ORG_ID_SEARCH < 0 Or _filter.ORG_ID2_SEARCH < 0 Then
                Dim orgId As Decimal? = 0
                If _filter.ORG_ID_SEARCH IsNot Nothing Then
                    orgId = 0 - _filter.ORG_ID_SEARCH
                End If
                If _filter.ORG_ID2_SEARCH IsNot Nothing Then
                    orgId = 0 - _filter.ORG_ID2_SEARCH
                End If

                Dim query = From t In Context.HU_TITLE
                            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = t.ORG_ID).DefaultIfEmpty
                            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.MASTER).DefaultIfEmpty
                            From a In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.INTERIM).DefaultIfEmpty
                            Where (t.ORG_ID = orgId Or orgId = 0) And t.ACTFLG = "A"
                            Select New TitleDTO With {
                                .ID = t.ID,
                                .CODE = t.CODE,
                                .NAME_VN = t.NAME_VN,
                                .NAME_EN = t.NAME_EN,
                                .ORG_ID = t.ORG_ID,
                                .ORG_NAME = o.CODE & " (FTE Plan)",
                                .MASTER = t.MASTER,
                                .MASTER_CODE = If(t.MASTER IsNot Nothing, e.EMPLOYEE_CODE, Nothing),
                                .REMARK = e.FULLNAME_VN,
                                 .HIRING_STATUS = t.HIRING_STATUS,
                                 .CONCURRENT = t.CONCURRENT,
                                .INTERIM = t.INTERIM,
                                .INTERIM_NAME = If(t.INTERIM Is Nothing And t.MASTER Is Nothing, str, If(t.INTERIM IsNot Nothing, a.EMPLOYEE_CODE & " - " & a.FULLNAME_VN, str3)),
                                .IS_OWNER = t.ISOWNER,
                                .IS_PLAN = t.IS_PLAN,
                                .FLAG = 1,
                                .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                .JOB_ID = t.JOB_ID,
                                .COST_CENTER = t.COST_CENTER,
                                .IS_NONPHYSICAL = t.IS_NONPHYSICAL,
                                .BOTH = If(t.ISOWNER = -1, 1, 0),
                                .MASTER_NAME = If(t.INTERIM Is Nothing And t.MASTER Is Nothing, str, If(t.HIRING_STATUS = -1, str1, If(t.CONCURRENT = -1, e.EMPLOYEE_CODE & " - " & e.FULLNAME_VN & str2, If(t.MASTER IsNot Nothing, e.EMPLOYEE_CODE & " - " & e.FULLNAME_VN, str3)))),
                                .COLOR = "#4BA838"}
                Dim lst = query
                If _filter.TEXTBOX_SEARCH <> "" Then
                    lst = lst.Where(Function(f) f.CODE.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper) Or
                                            f.NAME_VN.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper) Or
                                            f.MASTER_NAME.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper) Or
                                            f.MASTER_CODE.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper))
                End If
                If _filter.TEXTBOX2_SEARCH <> "" Then
                    lst = lst.Where(Function(f) f.CODE.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper) Or
                                            f.NAME_VN.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper) Or
                                            f.MASTER_NAME.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper) Or
                                            f.MASTER_CODE.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper))
                End If
                If _filter.IS_INTERIM IsNot Nothing Then
                    lst = lst.Where(Function(f) f.INTERIM IsNot Nothing)
                End If
                If _filter.IS_MASTER IsNot Nothing Then
                    lst = lst.Where(Function(f) f.MASTER IsNot Nothing)
                End If
                If _filter.IS_CONCURRENTLY IsNot Nothing Then
                    lst = lst.Where(Function(f) f.CONCURRENT IsNot Nothing)
                End If
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
                Return lst.ToList
            Else
                Dim orgId As Decimal? = 0
                If _filter.ORG_ID_SEARCH IsNot Nothing Then
                    orgId = _filter.ORG_ID_SEARCH
                End If
                If _filter.ORG_ID2_SEARCH IsNot Nothing Then
                    orgId = _filter.ORG_ID2_SEARCH
                End If
                Dim query = From t In Context.HU_TITLE
                            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = t.ORG_ID).DefaultIfEmpty
                            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.MASTER).DefaultIfEmpty
                            From a In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.INTERIM).DefaultIfEmpty
                            Where (t.ORG_ID = orgId Or orgId = 0) And t.ACTFLG = "A"
                            Select New TitleDTO With {
                              .ID = t.ID,
                                .CODE = t.CODE,
                                .NAME_VN = t.NAME_VN,
                                .NAME_EN = t.NAME_EN,
                                .ORG_ID = t.ORG_ID,
                                .ORG_NAME = If(t.IS_PLAN = -1, o.CODE & " (FTE Plan)", o.CODE),
                                .MASTER = t.MASTER,
                                .MASTER_CODE = If(t.MASTER IsNot Nothing, e.EMPLOYEE_CODE, Nothing),
                                .REMARK = e.FULLNAME_VN,
                                .HIRING_STATUS = t.HIRING_STATUS,
                                .CONCURRENT = t.CONCURRENT,
                                .INTERIM = t.INTERIM,
                                .INTERIM_NAME = If(t.INTERIM Is Nothing And t.MASTER Is Nothing, str, If(t.INTERIM IsNot Nothing, a.EMPLOYEE_CODE & " - " & a.FULLNAME_VN, str3)),
                                .IS_OWNER = t.ISOWNER,
                                .IS_PLAN = t.IS_PLAN,
                                .FLAG = 1,
                                .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                .JOB_ID = t.JOB_ID,
                                .COST_CENTER = t.COST_CENTER,
                                .IS_NONPHYSICAL = t.IS_NONPHYSICAL,
                                .BOTH = If(t.ISOWNER = -1, 1, 0),
                                .MASTER_NAME = If(t.INTERIM Is Nothing And t.MASTER Is Nothing, str, If(t.HIRING_STATUS = -1, str1, If(t.CONCURRENT = -1, e.EMPLOYEE_CODE & " - " & e.FULLNAME_VN & str2, If(t.MASTER IsNot Nothing, e.EMPLOYEE_CODE & " - " & e.FULLNAME_VN, str3)))),
                                .COLOR = If(t.HIRING_STATUS = -1, "#0000FF", If(t.IS_PLAN = -1, "#4BA838", If(t.MASTER.HasValue AndAlso t.CONCURRENT = -1, "#4BA838", "#070101")))}
                Dim lst = query
                If _filter.TEXTBOX_SEARCH <> "" Then
                    lst = lst.Where(Function(f) f.CODE.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper) Or
                                            f.NAME_VN.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper) Or
                                            f.MASTER_NAME.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper) Or
                                            f.MASTER_CODE.ToUpper.Contains(_filter.TEXTBOX_SEARCH.ToUpper))
                End If
                If _filter.TEXTBOX2_SEARCH <> "" Then
                    lst = lst.Where(Function(f) f.CODE.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper) Or
                                            f.NAME_VN.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper) Or
                                            f.MASTER_NAME.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper) Or
                                            f.MASTER_CODE.ToUpper.Contains(_filter.TEXTBOX2_SEARCH.ToUpper))
                End If
                If _filter.IS_INTERIM IsNot Nothing Then
                    lst = lst.Where(Function(f) f.INTERIM IsNot Nothing)
                End If
                If _filter.IS_MASTER IsNot Nothing Then
                    lst = lst.Where(Function(f) f.MASTER IsNot Nothing)
                End If
                If _filter.IS_CONCURRENTLY IsNot Nothing Then
                    lst = lst.Where(Function(f) f.CONCURRENT IsNot Nothing)
                End If
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
                Return lst.ToList
            End If
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPositionByOrgID")
            Throw ex
        End Try

    End Function
    Public Function GetTitleByTitleID(ByVal Id As Decimal) As TitleDTO

        Try

            If Id < 0 Then
                Dim TitleId = 0 - Id
                Dim query = (From t In Context.HU_TITLE
                             From o In Context.HU_ORGANIZATION_OM.Where(Function(f) f.ID = t.ORG_ID).DefaultIfEmpty
                             From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.MASTER).DefaultIfEmpty
                             Where t.ID = TitleId
                             Select New TitleDTO With {
                                 .ID = t.ID,
                                 .CODE = t.CODE,
                                 .NAME_VN = t.NAME_VN,
                                 .NAME_EN = t.NAME_EN,
                                 .ORG_ID = t.ORG_ID,
                                 .ORG_NAME = o.NAME_VN & " (FTE Plan)",
                                 .MASTER = t.MASTER,
                                 .MASTER_CODE = e.EMPLOYEE_CODE,
                                 .REMARK = e.FULLNAME_VN,
                                  .HIRING_STATUS = t.HIRING_STATUS,
                                  .CONCURRENT = t.CONCURRENT,
                                 .IS_OWNER = t.ISOWNER,
                                 .IS_PLAN = t.IS_PLAN,
                                 .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                 .COLOR = "#4BA838"}).FirstOrDefault

                query.MASTER_NAME = ReturnString(query.MASTER, query.HIRING_STATUS, query.REMARK, query.CONCURRENT)
                Return query
            Else
                Dim query = (From t In Context.HU_TITLE
                             From o In Context.HU_ORGANIZATION_OM.Where(Function(f) f.ID = t.ORG_ID).DefaultIfEmpty
                             From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = t.MASTER).DefaultIfEmpty
                             Where t.ID = Id
                             Select New TitleDTO With {
                                 .ID = t.ID,
                                 .CODE = t.CODE,
                                 .NAME_VN = t.NAME_VN,
                                 .NAME_EN = t.NAME_EN,
                                 .ORG_ID = t.ORG_ID,
                                 .ORG_NAME = If(t.IS_PLAN = -1, o.NAME_VN & "(FTE Plan)", o.NAME_VN),
                                 .MASTER = t.MASTER,
                                 .MASTER_CODE = e.EMPLOYEE_CODE,
                                 .REMARK = e.FULLNAME_VN,
                                  .HIRING_STATUS = t.HIRING_STATUS,
                                  .CONCURRENT = t.CONCURRENT,
                                 .IS_OWNER = t.ISOWNER,
                                 .IS_PLAN = t.IS_PLAN,
                                  .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                 .COLOR = If(t.HIRING_STATUS = -1, "#0000FF", If(t.MASTER.HasValue AndAlso t.CONCURRENT = -1, "#4BA838", "#070101"))}).FirstOrDefault

                query.MASTER_NAME = ReturnString(query.MASTER, query.HIRING_STATUS, query.REMARK, query.CONCURRENT)
                Return query
            End If

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitleByTitleID")
            Throw ex
        End Try

    End Function
    Public Function ReturnString(ByVal MASTER As Decimal?, ByVal HIRING_STATUS As Decimal?, ByVal MASTER_NAME As String, ByVal CONCURRENT As Decimal?) As String
        Try
            Dim str As String = " "
            If MASTER Is Nothing Then
                str = "(vacant)"
            ElseIf HIRING_STATUS = -1 Then
                str = "(in hiring process)"
            ElseIf MASTER IsNot Nothing And CONCURRENT <> -1 Then
                str = MASTER_NAME
            ElseIf MASTER IsNot Nothing And CONCURRENT = -1 Then
                str = MASTER_NAME & " (concurrent)"
            End If
            Return str
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ReturnString")
            Throw ex
        End Try
    End Function
    'Nhân bản
    Public Function InsertTitleNB(ByVal obj As TitleDTO, ByVal OrgRight As Decimal, ByVal Address As Decimal, ByVal log As UserLog,
                                    Optional ByVal OrgIDDefault As Decimal = 1,
                                    Optional ByVal IsDissolveDefault As Decimal = 0) As Boolean
        Dim objData As New HU_TITLE
        Dim sDate As DateTime = Date.Now
        Dim iCount As Integer = 0
        Dim OrgIdRight As Decimal
        Dim gID As Decimal
        If OrgRight < 0 Then
            OrgIdRight = 0 - OrgRight
        Else
            OrgIdRight = OrgRight
        End If

        Using cls As New DataAccess.QueryData
            cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                             New With {.P_USERNAME = log.Username.ToUpper(),
                                       .P_ORGID = OrgIDDefault,
                                       .P_ISDISSOLVE = IsDissolveDefault})
        End Using
        '// Check quyen du lieu
        Dim objChk As List(Of Decimal) = (From p In Context.HU_TITLE
                                          From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper())
                                          Where p.ID = obj.ID
                                          Select chosen.ORG_ID).ToList()
        If (objChk.Count < 1) Then
            Return False
        End If

        objChk = (From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = OrgRight And f.USERNAME = log.Username.ToUpper())
                  Select chosen.ORG_ID).ToList()
        If (objChk.Count < 1) Then
            Return False
        End If

        Try

            If obj.IS_OWNER = True Then
                Dim OrgParent = (From o In Context.HU_ORGANIZATION Where o.ID = OrgIdRight
                                 Select o.PARENT_ID).FirstOrDefault
                If OrgParent IsNot Nothing Then
                    Dim TitleId = (From t In Context.HU_TITLE
                                   Where t.ORG_ID = OrgParent And t.ISOWNER = -1 And t.ACTFLG = "A" And t.IS_PLAN = 0
                                   Select t.ID).FirstOrDefault
                    'Nếu Org cha có Pos trưởng thì QLTT và QLPD là Pos trưởng của Org cha
                    If TitleId <> 0 Then
                        objData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
                        objData.NAME_VN = obj.NAME_VN
                        objData.NAME_EN = obj.NAME_EN
                        objData.CODE = AutoGenCodeHuTile("HU_TITLE", "CODE")
                        objData.JOB_ID = obj.JOB_ID
                        objData.ACTFLG = "A"
                        objData.ORG_ID = OrgIdRight
                        objData.ISOWNER = obj.IS_OWNER
                        objData.IS_PLAN = obj.IS_PLAN
                        objData.WORK_LOCATION = Address
                        objData.HIRING_STATUS = obj.HIRING_STATUS
                        objData.LM = TitleId
                        objData.CSM = TitleId
                        objData.COST_CENTER = obj.COST_CENTER
                        objData.IS_NONPHYSICAL = obj.IS_NONPHYSICAL
                        objData.TYPE_ACTIVITIES = "ADDNEW"
                        objData.EFFECTIVE_DATE = sDate
                        Context.HU_TITLE.AddObject(objData)
                        Context.SaveChanges(log)
                        gID = objData.ID
                    Else
                        'Nếu Org cha k có trưởng thì QLTT và QLPD để null
                        objData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
                        objData.NAME_VN = obj.NAME_VN
                        objData.NAME_EN = obj.NAME_EN
                        objData.CODE = AutoGenCodeHuTile("HU_TITLE", "CODE")
                        objData.JOB_ID = obj.JOB_ID
                        objData.ACTFLG = "A"
                        objData.ORG_ID = OrgIdRight
                        objData.ISOWNER = obj.IS_OWNER
                        objData.IS_PLAN = obj.IS_PLAN
                        objData.WORK_LOCATION = Address
                        objData.HIRING_STATUS = obj.HIRING_STATUS
                        objData.LM = Nothing
                        objData.CSM = Nothing
                        objData.COST_CENTER = obj.COST_CENTER
                        objData.IS_NONPHYSICAL = obj.IS_NONPHYSICAL
                        objData.TYPE_ACTIVITIES = "ADDNEW"
                        objData.EFFECTIVE_DATE = sDate
                        Context.HU_TITLE.AddObject(objData)
                        Context.SaveChanges(log)
                        gID = objData.ID
                    End If
                    'update QLPD và QLTT của tất cả các Pos cùng đơn vị có Pos chuyển đến
                    Dim lstTitle = (From t In Context.HU_TITLE Where t.ORG_ID = OrgIdRight And t.ISOWNER = 0 And t.ACTFLG = "A").ToList()
                    If lstTitle.Count > 0 Then
                        For Each item In lstTitle
                            Dim objData1 As New HU_TITLE With {.ID = item.ID}
                            objData1 = (From p In Context.HU_TITLE Where p.ID = item.ID).FirstOrDefault
                            If objData1 IsNot Nothing Then
                                objData1.LM = gID
                                objData1.CSM = gID
                                objData1.TYPE_ACTIVITIES = "UPDATE"
                                objData1.EFFECTIVE_DATE = sDate
                                Context.SaveChanges(log)
                            End If
                        Next
                    End If
                    If obj.IS_PLAN = False Then
                        'Update QLTT và QLPD tất cả các Pos là trưởng phòng của tất cả các đơn vị con của đơn vị có Pos chuyển đến
                        Dim lstOrg = (From o In Context.HU_ORGANIZATION
                                      Where (o.PARENT_ID = OrgIdRight)
                                      Select o.ID).ToList()
                        If lstOrg.Count > 0 Then
                            For Each item1 As Decimal In lstOrg
                                Dim lstTitleChild As New List(Of HU_TITLE)
                                lstTitleChild = (From d In Context.HU_TITLE Where d.ORG_ID = item1 And d.ISOWNER = -1 And d.ACTFLG = "A").ToList()
                                If lstTitleChild.Count > 0 Then
                                    For Each item2 In lstTitleChild
                                        Dim objData2 As New HU_TITLE With {.ID = item2.ID}
                                        objData2 = (From p In Context.HU_TITLE Where p.ID = item2.ID).FirstOrDefault
                                        If objData2 IsNot Nothing Then
                                            objData2.LM = gID
                                            objData2.CSM = gID
                                            objData.TYPE_ACTIVITIES = "UPDATE"
                                            objData.EFFECTIVE_DATE = sDate
                                            Context.SaveChanges(log)
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                End If
            Else
                Dim TitleId = (From t In Context.HU_TITLE
                               Where t.ORG_ID = OrgIdRight And t.ISOWNER = -1 And t.ACTFLG = "A" And t.IS_PLAN = 0
                               Select t.ID).FirstOrDefault
                'Nếu có Pos trưởng thì QLTT,QLPD là Pos trưởng
                If TitleId <> 0 Then
                    objData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
                    objData.NAME_VN = obj.NAME_VN
                    objData.NAME_EN = obj.NAME_EN
                    objData.CODE = AutoGenCodeHuTile("HU_TITLE", "CODE")
                    objData.JOB_ID = obj.JOB_ID
                    objData.ACTFLG = "A"
                    objData.ORG_ID = OrgIdRight
                    objData.ISOWNER = obj.IS_OWNER
                    objData.IS_PLAN = obj.IS_PLAN
                    objData.WORK_LOCATION = Address
                    objData.HIRING_STATUS = obj.HIRING_STATUS
                    objData.LM = TitleId
                    objData.CSM = TitleId
                    objData.COST_CENTER = obj.COST_CENTER
                    objData.IS_NONPHYSICAL = obj.IS_NONPHYSICAL
                    objData.TYPE_ACTIVITIES = "ADDNEW"
                    objData.EFFECTIVE_DATE = sDate
                    Context.HU_TITLE.AddObject(objData)
                    Context.SaveChanges(log)
                    gID = objData.ID
                Else
                    'Nếu có k có Pos trưởng, thì QLTT và QLPD là Pos trưởng Org cha 
                    Dim OrgRightParent = (From o In Context.HU_ORGANIZATION Where o.ID = OrgIdRight
                                          Select o.PARENT_ID).FirstOrDefault
                    If OrgRightParent IsNot Nothing Then
                        Dim TitleIdParent = (From d In Context.HU_TITLE
                                             Where d.ORG_ID = OrgRightParent And d.ISOWNER = -1 And d.IS_PLAN = 0 And d.ACTFLG = "A"
                                             Select d.ID).FirstOrDefault
                        If TitleIdParent <> 0 Then
                            objData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
                            objData.NAME_VN = obj.NAME_VN
                            objData.NAME_EN = obj.NAME_EN
                            objData.CODE = AutoGenCodeHuTile("HU_TITLE", "CODE")
                            objData.JOB_ID = obj.JOB_ID
                            objData.ACTFLG = "A"
                            objData.ORG_ID = OrgIdRight
                            objData.ISOWNER = obj.IS_OWNER
                            objData.IS_PLAN = obj.IS_PLAN
                            objData.WORK_LOCATION = Address
                            objData.HIRING_STATUS = obj.HIRING_STATUS
                            objData.LM = TitleIdParent
                            objData.CSM = TitleIdParent
                            objData.COST_CENTER = obj.COST_CENTER
                            objData.IS_NONPHYSICAL = obj.IS_NONPHYSICAL
                            objData.TYPE_ACTIVITIES = "ADDNEW"
                            objData.EFFECTIVE_DATE = sDate
                            Context.HU_TITLE.AddObject(objData)
                            Context.SaveChanges(log)
                            gID = objData.ID
                        Else
                            'Nếu cha k có Pos trưởng thì QLTT,QLPD để null
                            objData.ID = Utilities.GetNextSequence(Context, Context.HU_TITLE.EntitySet.Name)
                            objData.NAME_VN = obj.NAME_VN
                            objData.NAME_EN = obj.NAME_EN
                            objData.CODE = AutoGenCodeHuTile("HU_TITLE", "CODE")
                            objData.JOB_ID = obj.JOB_ID
                            objData.ACTFLG = "A"
                            objData.ORG_ID = OrgIdRight
                            objData.ISOWNER = obj.IS_OWNER
                            objData.IS_PLAN = obj.IS_PLAN
                            objData.WORK_LOCATION = Address
                            objData.HIRING_STATUS = obj.HIRING_STATUS
                            objData.LM = Nothing
                            objData.CSM = Nothing
                            objData.COST_CENTER = obj.COST_CENTER
                            objData.IS_NONPHYSICAL = obj.IS_NONPHYSICAL
                            objData.TYPE_ACTIVITIES = "ADDNEW"
                            objData.EFFECTIVE_DATE = sDate
                            Context.HU_TITLE.AddObject(objData)
                            Context.SaveChanges(log)
                            gID = objData.ID
                        End If
                    End If
                End If
            End If
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_OMS_BUSINESS.JOB_AUTO_UPDATE_TITLE_TEMP")
            End Using
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertTitleNB")
            Throw ex
        End Try
    End Function
#End Region

#Region "JobBand"

    Public Function GetJobBand(ByVal _filter As JobBradDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobBradDTO)

        Try
            Dim query = From p In Context.HU_JOB_BAND
                        From O In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        Select New JobBradDTO With {
                            .ID = p.ID,
                            .NAME_EN = p.NAME_EN,
                            .NAME_VN = p.NAME_VN,
                            .LEVEL_FROM = p.LEVEL_FROM,
                            .LEVEL_TO = p.LEVEL_TO,
                            .CREATED_DATE = p.CREATED_DATE,
                            .ACTFLG = If(p.STATUS = -1, "Áp dụng", "Ngừng áp dụng"),
                            .COLOR = If(p.STATUS = -1, "#070101", "#969696"),
                            .STATUS = p.STATUS,
                            .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                            .TITLE_GROUP_NAME = O.NAME_VN
                        }

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetJobBandID(ByVal ID As Decimal) As JobBradDTO

        Try
            Dim query = (From p In Context.HU_JOB_BAND
                         From O In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                         Where p.ID = ID
                         Select New JobBradDTO With {
                                .ID = p.ID,
                                .NAME_EN = p.NAME_EN,
                                .NAME_VN = p.NAME_VN,
                                .LEVEL_FROM = p.LEVEL_FROM,
                                .LEVEL_TO = p.LEVEL_TO,
                                .CREATED_DATE = p.CREATED_DATE,
                                .ACTFLG = If(p.STATUS = -1, "Áp dụng", "Ngừng áp dụng"),
                                .COLOR = If(p.STATUS = -1, "#070101", "#969696"),
                                .STATUS = p.STATUS,
                                .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                .TITLE_GROUP_NAME = O.NAME_VN
                              }).SingleOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertJobBand(ByVal objTitle As JobBradDTO, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_JOB_BAND
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_JOB_BAND.EntitySet.Name)
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.LEVEL_FROM = objTitle.LEVEL_FROM
            objTitleData.LEVEL_TO = objTitle.LEVEL_TO
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.CREATED_DATE = Date.Now
            objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            Context.HU_JOB_BAND.AddObject(objTitleData)
            Context.SaveChanges()
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyJobBand(ByVal objTitle As JobBradDTO, ByRef gID As Decimal) As Boolean
        Dim objTitleData As HU_JOB_BAND
        Try
            objTitleData = (From p In Context.HU_JOB_BAND Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.LEVEL_FROM = objTitle.LEVEL_FROM
            objTitleData.LEVEL_TO = objTitle.LEVEL_TO
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.TITLE_GROUP_ID = objTitle.TITLE_GROUP_ID
            Context.SaveChanges()
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveJobBand(ByVal lstID As List(Of Decimal), ByVal sActive As Decimal) As Boolean
        Dim lstTitleData As List(Of HU_JOB_BAND)
        Try
            lstTitleData = (From p In Context.HU_JOB_BAND Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                lstTitleData(index).STATUS = sActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteJobBand(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstTitleData As List(Of HU_JOB_BAND)
        Try

            lstTitleData = (From p In Context.HU_JOB_BAND Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleData.Count - 1
                Context.HU_JOB_BAND.DeleteObject(lstTitleData(index))
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region
    Public Function ActiveOrgEmp(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As Decimal,
                                 ByVal log As UserLog) As Boolean
        Dim lstOrgTitleData As List(Of HU_ORGANIZATION_OM)
        Dim lstOrgTitleData_A As List(Of HU_ORGANIZATION_OM_ACTIVITIES)
        Dim objOrganizationData As New HU_ORGANIZATION
        Try

            lstOrgTitleData = (From p In Context.HU_ORGANIZATION_OM Where lstOrgTitle.Contains(p.ID)).ToList
            lstOrgTitleData_A = (From p In Context.HU_ORGANIZATION_OM_ACTIVITIES Where lstOrgTitle.Contains(p.ID)).ToList
            For index = 0 To lstOrgTitleData_A.Count - 1
                lstOrgTitleData_A(index).STATUS_ID = sActive
                lstOrgTitleData_A(index).MODIFIED_DATE = DateTime.Now
                lstOrgTitleData_A(index).MODIFIED_BY = log.Username
                lstOrgTitleData_A(index).MODIFIED_LOG = log.ComputerName
            Next
            For index = 0 To lstOrgTitleData.Count - 1
                lstOrgTitleData(index).STATUS_ID = sActive
                lstOrgTitleData(index).MODIFIED_DATE = DateTime.Now
                lstOrgTitleData(index).MODIFIED_BY = log.Username
                lstOrgTitleData(index).MODIFIED_LOG = log.ComputerName
                Dim COUNT As Decimal
                COUNT = (From p In Context.HU_ORGANIZATION Where lstOrgTitle.Contains(p.ID)).Count
                If COUNT = 0 And sActive = 8527 Then
                    objOrganizationData.ID = lstOrgTitleData(index).ID
                    objOrganizationData.CODE = lstOrgTitleData(index).CODE
                    objOrganizationData.NAME_VN = lstOrgTitleData(index).NAME_VN.Trim
                    objOrganizationData.NAME_EN = lstOrgTitleData(index).NAME_EN.Trim
                    objOrganizationData.COST_CENTER_CODE = lstOrgTitleData(index).COST_CENTER_CODE
                    objOrganizationData.PARENT_ID = lstOrgTitleData(index).PARENT_ID
                    'objOrganizationData.STATUS_ID = lstOrgTitleData(index).STATUS_ID
                    objOrganizationData.ACTFLG = "A"
                    'objOrganizationData.CAP = lstOrgTitleData(index).CAP
                    'objOrganizationData.NHOMDUAN = lstOrgTitleData(index).NHOMDUAN
                    'objOrganizationData.EFFECTDATE = lstOrgTitleData(index).EFFECTDATE
                    objOrganizationData.HIERARCHICAL_PATH = lstOrgTitleData(index).HIERARCHICAL_PATH
                    objOrganizationData.DESCRIPTION_PATH = lstOrgTitleData(index).DESCRIPTION_PATH
                    objOrganizationData.FOUNDATION_DATE = lstOrgTitleData(index).FOUNDATION_DATE
                    objOrganizationData.DISSOLVE_DATE = lstOrgTitleData(index).DISSOLVE_DATE
                    objOrganizationData.ADDRESS = lstOrgTitleData(index).ADDRESS
                    objOrganizationData.FAX = lstOrgTitleData(index).FAX
                    objOrganizationData.MOBILE = lstOrgTitleData(index).MOBILE
                    objOrganizationData.PROVINCE_NAME = lstOrgTitleData(index).PROVINCE_NAME
                    objOrganizationData.DATE_BUSINESS = lstOrgTitleData(index).DATE_BUSINESS
                    objOrganizationData.PIT_NO = lstOrgTitleData(index).PIT_NO
                    'objOrganizationData.ORD_NO = objOrganization.ORD_NO
                    objOrganizationData.REPRESENTATIVE_ID = lstOrgTitleData(index).REPRESENTATIVE_ID
                    Context.HU_ORGANIZATION.AddObject(objOrganizationData)

                    'Các user đã được phần quyền ở đơn vị cha thì tự động phân quyền vào đơn vị con
                    Using cls As New DataAccess.QueryData
                        cls.ExecuteStore("PKG_PROFILE_BUSINESS.UPDATE_ORG_ACCESS",
                                                 New With {.P_ORG_ID = objOrganizationData.ID,
                                                           .P_PARENT_ID = objOrganizationData.PARENT_ID})
                    End Using
                ElseIf COUNT >= 1 And sActive = 8527 Then
                    Dim objOrgData As New HU_ORGANIZATION With {.ID = lstOrgTitleData(index).ID}
                    objOrgData = (From p In Context.HU_ORGANIZATION Where lstOrgTitle.Contains(p.ID)).FirstOrDefault
                    objOrgData.ID = lstOrgTitleData(index).ID
                    objOrgData.FOUNDATION_DATE = lstOrgTitleData(index).FOUNDATION_DATE
                    objOrgData.DISSOLVE_DATE = lstOrgTitleData(index).DISSOLVE_DATE
                    objOrgData.ADDRESS = lstOrgTitleData(index).ADDRESS
                    objOrgData.ACTFLG = "A"
                    objOrgData.FAX = lstOrgTitleData(index).FAX
                    objOrgData.MOBILE = lstOrgTitleData(index).MOBILE
                    'objOrgData.STATUS_ID = lstOrgTitleData(index).STATUS_ID
                    objOrgData.PROVINCE_NAME = lstOrgTitleData(index).PROVINCE_NAME
                    objOrgData.COST_CENTER_CODE = lstOrgTitleData(index).COST_CENTER_CODE
                    objOrgData.DATE_BUSINESS = lstOrgTitleData(index).DATE_BUSINESS
                    objOrgData.PIT_NO = lstOrgTitleData(index).PIT_NO
                    'objOrgData.ORD_NO = lstOrgTitleData(index).ORD_NO
                    objOrgData.REPRESENTATIVE_ID = lstOrgTitleData(index).REPRESENTATIVE_ID
                ElseIf COUNT >= 1 And sActive = 8526 Then
                    objOrganizationData.ID = lstOrgTitleData(index).ID
                    Context.HU_ORGANIZATION.Attach(objOrganizationData)
                    objOrganizationData.ACTFLG = "I"
                    ' objOrganizationData.STATUS_ID = lstOrgTitleData(index).STATUS_ID
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#Region "Bao cao jobPosition"
    Public Function JobPossitionRptHist(ByVal RptMonth As Date, ByVal sLang As String, ByVal log As UserLog) As List(Of JobPositinTreeDTO)

        Dim lst As List(Of JobPositinTreeDTO) = New List(Of JobPositinTreeDTO)
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_REPORT.GET_JOBPOSITION_CHART_HIST",
                                         New With {.P_LANGUAGE = sLang,
                                                   .P_MONTH = RptMonth,
                                                   .P_USERNAME = log.Username.ToUpper,
                                                   .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of JobPositinTreeDTO)()
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    'Public Function GetJobPosTree(ByVal sLang As String, ByVal log As UserLog) As List(Of JobPositinTreeDTO)

    '    Dim lst As List(Of JobPositinTreeDTO) = New List(Of JobPositinTreeDTO)
    '    Try

    '        Using cls As New DataAccess.QueryData
    '            Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_REPORT.GET_JOB_POS_TREE",
    '                                     New With {.P_LANGUAGE = sLang,
    '                                               .P_USERNAME = log.Username.ToUpper,
    '                                               .P_CUR = cls.OUT_CURSOR})
    '            lst = dtData.ToList(Of JobPositinTreeDTO)()
    '        End Using
    '        Return lst
    '    Catch ex As Exception
    '        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
    '        Throw ex
    '    End Try
    'End Function

    Public Function GetJobChileTree(ByVal job_Id As Decimal, ByVal sLang As String) As List(Of JobChildTreeDTO)

        Dim lst As List(Of JobChildTreeDTO) = New List(Of JobChildTreeDTO)
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_REPORT.GET_JOB_CHILD_TREE",
                                           New With {.P_LANGUAGE = sLang,
                                                     .P_JOB_ID = job_Id,
                                                     .P_CUR = cls.OUT_CURSOR})

                lst = dtData.ToList(Of JobChildTreeDTO)()
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function



    Public Function GetJobPosTreePortal(ByVal EmployeeId As String, ByVal sLang As String, ByVal log As UserLog) As List(Of JobPositinTreeDTO)

        Dim lst As List(Of JobPositinTreeDTO) = New List(Of JobPositinTreeDTO)
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_REPORT.GET_JOB_POS_TREE_PORTAL",
                                         New With {.P_EMPLOYEE_ID = EmployeeId,
                                                   .P_LANGUAGE = sLang,
                                                   .P_USERNAME = log.Username.ToUpper,
                                                   .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of JobPositinTreeDTO)()
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
    Public Function ModifyJobPosTreeList(ByVal objJobPositinTree As JobPositinTreeDTO,
                                   ByVal log As UserLog) As Boolean
        Dim objJobPositinTreeData As New RPT_JOB_POS_HIS With {.ID = objJobPositinTree.ID}
        Dim dateTime As String = "01-01-" & Date.Now.Year
        Dim createDate As DateTime = Convert.ToDateTime(dateTime)
        Try

            objJobPositinTreeData = (From p In Context.RPT_JOB_POS_HIS Where p.ID = objJobPositinTree.ID And p.PARENT_ID = objJobPositinTree.PARENT_ID And p.CREATED_DATE = createDate).FirstOrDefault
            If objJobPositinTreeData IsNot Nothing Then
                objJobPositinTreeData.LY_FTE_V2 = objJobPositinTree.LY_FTE
                Context.SaveChanges(log)
            Else

                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_OMS_BUSINESS.INSERT_RPT_JOB_POS_HIS",
                                               New With {.P_ID = objJobPositinTree.ID,
                                                         .P_PARENT_ID = objJobPositinTree.PARENT_ID,
                                                         .P_NAME_EN = objJobPositinTree.ORG_NAME,
                                                         .P_NAME_VN = objJobPositinTree.ORG_NAME,
                                                         .P_LY_FTE_V2 = objJobPositinTree.LY_FTE,
                                                         .P_CREATE_DATE = createDate})
                End Using
            End If

            'gID = objOrganizationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetjobPosition(ByVal _filter As JobPositionDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             ByVal Language As String,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobPositionDTO)

        Try
            Dim query = (From p In Context.HU_JOB_POSITION
                         From a In Context.HU_JOB.Where(Function(f) f.ID = p.JOB_ID).DefaultIfEmpty
                         Select New JobPositionDTO With {
                                    .ID = p.ID,
                                    .NAME = p.NAME,
                                    .NAME_EN = p.NAME_EN,
                                    .JOB_ID = p.JOB_ID,
                                    .CREATED_DATE = p.CREATED_DATE,
                                    .JOB_NAME = a.CODE + " - " + If(Language = "vi-VN", a.NAME_VN, a.NAME_EN),
                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertjobPosition(ByVal objjob As JobPositionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objjobData As New HU_JOB_POSITION
        Dim iCount As Integer = 0
        Try
            objjobData.ID = Utilities.GetNextSequence(Context, Context.HU_JOB_POSITION.EntitySet.Name)
            objjobData.JOB_ID = objjob.JOB_ID
            objjobData.NAME = objjob.NAME
            objjobData.NAME_EN = objjob.NAME_EN
            objjobData.ACTFLG = objjob.ACTFLG
            Context.HU_JOB_POSITION.AddObject(objjobData)

            Context.SaveChanges(log)
            gID = objjobData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyjobPosition(ByVal objjob As JobPositionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objjobData As HU_JOB_POSITION
        Try
            objjobData = (From p In Context.HU_JOB_POSITION Where p.ID = objjob.ID).FirstOrDefault
            objjobData.JOB_ID = objjob.JOB_ID
            objjobData.NAME = objjob.NAME
            objjobData.NAME_EN = objjob.NAME_EN

            Context.SaveChanges(log)
            gID = objjobData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActivejobPosition(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstjobData As List(Of HU_JOB_POSITION)
        Try
            lstjobData = (From p In Context.HU_JOB_POSITION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstjobData.Count - 1
                lstjobData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeletejobPosition(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstjobData As List(Of HU_JOB_POSITION)
        Try

            lstjobData = (From p In Context.HU_JOB_POSITION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstjobData.Count - 1
                Context.HU_JOB_POSITION.DeleteObject(lstjobData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function OrgChartRpt(ByVal Language As String, ByVal log As UserLog) As List(Of OrgChartRptDTO)

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_REPORT.GET_ORGANIZATION_CHART",
                                        New With {.P_LANGUAGE = Language,
                                                  .P_USERNAME = log.Username.ToUpper(),
                                                  .PV_CUR = cls.OUT_CURSOR})

                Return dtData.ToList(Of OrgChartRptDTO)()
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetJobPosTree(ByVal sLang As String, ByVal log As UserLog) As List(Of JobPositinTreeDTO)

        Dim lst As List(Of JobPositinTreeDTO) = New List(Of JobPositinTreeDTO)
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_OMS_REPORT.GET_JOB_POS_TREE",
                                         New With {.P_LANGUAGE = sLang,
                                                   .P_USERNAME = log.Username.ToUpper,
                                                   .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of JobPositinTreeDTO)()
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetLinkByCode(ByVal Code As String) As FolderDTO
        Try
            Dim obj = (From p In Context.SE_CONFIG Where p.CODE = Code
                       Select New FolderDTO With {
                            .LINK = p.VALUE
                        }).FirstOrDefault
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetFileNameCode(ByVal UserID As String) As String
        Try
            Dim userInf = (From u In Context.SE_USER Where u.ID = UserID).FirstOrDefault()
            Dim employeeID = userInf.EMPLOYEE_ID
            Dim employeeCode = (From p In Context.HU_EMPLOYEE Where p.ID = employeeID).FirstOrDefault().EMPLOYEE_CODE
            Return employeeCode
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function AddFileUpload(ByVal _File As FileUploadDTO) As Decimal
        Try
            Dim check = (From p In Context.HU_USERFILES Where p.NAME.ToUpper.Equals(_File.NAME.ToUpper) And p.FOLDER_ID = _File.FOLDER_ID).Count
            If check > 0 Then
                Return 1
            Else
                Dim objUserFile As New HU_USERFILES
                objUserFile.ID = Utilities.GetNextSequence(Context, Context.HU_USERFILES.EntitySet.Name)
                objUserFile.NAME = _File.NAME
                objUserFile.FOLDER_ID = _File.FOLDER_ID
                objUserFile.FILE_NAME = _File.FILE_NAME
                objUserFile.DESCRIPTION = _File.DESCRIPTION
                objUserFile.CREATED_BY = _File.CREATED_BY
                objUserFile.LINK = _File.LINK
                objUserFile.CREATED_DATE = DateTime.Now()
                Context.HU_USERFILES.AddObject(objUserFile)
            End If
            Context.SaveChanges()
            Return 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function AddFileUpload_mb(ByVal _File As FileUploadDTO) As Decimal
        Try
            Dim check = (From p In Context.HU_USERFILES Where p.NAME.ToUpper.Equals(_File.NAME.ToUpper) And p.FOLDER_ID = _File.FOLDER_ID).Count
            If check > 0 Then
                Return 1
            Else
                Dim objUserFile As New HU_USERFILES
                objUserFile.ID = Utilities.GetNextSequence(Context, Context.HU_USERFILES.EntitySet.Name)
                objUserFile.NAME = _File.NAME
                objUserFile.FOLDER_ID = _File.FOLDER_ID
                objUserFile.FILE_NAME = _File.FILE_NAME
                objUserFile.DESCRIPTION = _File.DESCRIPTION
                objUserFile.CREATED_BY = _File.CREATED_BY
                objUserFile.LINK = _File.LINK
                objUserFile.CREATED_DATE = DateTime.Now()
                Context.HU_USERFILES.AddObject(objUserFile)

                Dim check1 = (From p In Context.HU_EMPLOYEE_EDIT Where p.ID = _File.ID).Count
                If check1 > 0 Then
                    Dim objjobData As HU_EMPLOYEE_EDIT
                    objjobData = (From p In Context.HU_EMPLOYEE_EDIT Where p.ID = _File.ID).FirstOrDefault
                    If _File.COLUMNS = "FILE_CMND" Then
                        objjobData.FILE_CMND = _File.NAME
                    End If
                    If _File.COLUMNS = "FILE_CMND_BACK" Then
                        objjobData.FILE_CMND_BACK = _File.NAME
                    End If
                    If _File.COLUMNS = "FILE_ADDRESS" Then
                        objjobData.FILE_ADDRESS = _File.NAME
                    End If
                    If _File.COLUMNS = "FILE_OTHER" Then
                        objjobData.FILE_OTHER = _File.NAME
                    End If


                End If
            End If
            Context.SaveChanges()
            Return 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "ReminderList"
    Public Function GetReminderList(ByVal _filter As ReminderListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReminderListDTO)
        Try
            Dim query = From p In Context.HU_REMINDER
                        From s In Context.SE_REMINDER.Where(Function(f) f.TYPE = p.ID)
                        Select New ReminderListDTO With {
                                       .ID = p.ID,
                                       .REMINDER_NAME = p.REMINDER_NAME,
                                       .VALUE = s.VALUE,
                                       .USERNAME = s.USERNAME,
                                       .STATUS = p.STATUS,
                                       .STATUS_NAME = If(p.STATUS = 1, "Áp dụng", "Ngừng áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE}
            Dim lst = query
            If Not _filter.IS_LOAD_ALL.HasValue Then
                lst = lst.Where(Function(p) p.USERNAME.ToLower = log.Username.ToLower)
            Else
                lst = lst.Where(Function(p) p.USERNAME.ToLower = "ADMIN".ToLower)
            End If
            If _filter.ID > 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.VALUE <> "" Then
                lst = lst.Where(Function(p) p.VALUE <> "0")
            End If
            If _filter.STATUS.HasValue Then
                lst = lst.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.REMINDER_NAME <> "" Then
                lst = lst.Where(Function(p) p.REMINDER_NAME.ToUpper.Contains(_filter.REMINDER_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyReminderList(ByVal objReminderList As ReminderListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objReminderListData As New HU_REMINDER With {.ID = objReminderList.ID}
        Try
            objReminderListData = (From p In Context.HU_REMINDER Where p.ID = objReminderList.ID).FirstOrDefault
            objReminderListData.REMINDER_NAME = objReminderList.REMINDER_NAME
            Context.SaveChanges(log)
            gID = objReminderListData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ActiveReminderList(ByVal lstID As List(Of Decimal), ByVal sActive As Decimal,
                                   ByVal log As UserLog) As Boolean
        Dim lstReminderListData As List(Of HU_REMINDER)
        lstReminderListData = (From p In Context.HU_REMINDER Where lstID.Contains(p.ID)).ToList
        For index = 0 To lstReminderListData.Count - 1
            lstReminderListData(index).STATUS = sActive
        Next
        Dim lstSEReminderListData As List(Of SE_REMINDER)
        If sActive = 0 Then
            lstSEReminderListData = (From p In Context.SE_REMINDER Where lstID.Contains(p.TYPE)).ToList
            For index = 0 To lstSEReminderListData.Count - 1
                lstSEReminderListData(index).VALUE = 0
            Next
        End If
        Context.SaveChanges(log)
        Return True
    End Function
#End Region
End Class
