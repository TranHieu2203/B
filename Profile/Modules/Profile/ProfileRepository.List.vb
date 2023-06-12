Imports Framework.UI
Imports Profile.ProfileBusiness

Partial Public Class ProfileRepository
    Public Function GetjobPosition(ByVal _filter As JobPositionDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal Language As String,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobPositionDTO)
        Dim lstjob As List(Of JobPositionDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstjob = rep.GetjobPosition(_filter, PageIndex, PageSize, Total, Common.Common.SystemLanguage.Name, Sorts)
                Return lstjob
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertjobPosition(ByVal objjob As JobPositionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertjobPosition(objjob, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyjobPosition(ByVal objjob As JobPositionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyjobPosition(objjob, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCompanyLevel(ByVal orgID As Decimal) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCompanyLevel(orgID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActivejobPosition(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActivejobPosition(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletejobPosition(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeletejobPosition(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#Region "JobBand"
    Public Function GetJobBand(ByVal _filter As JobBradDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobBradDTO)
        Using rep As New ProfileBusinessClient
            Try
                Dim lst = rep.GetJobBand(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertJobBand(ByVal objTitle As JobBradDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try

                Return rep.InsertJobBand(objTitle, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifJobBand(ByVal objTitle As JobBradDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifJobBand(objTitle, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetJobBandID(ByVal sID As Decimal) As JobBradDTO
        Using rep As New ProfileBusinessClient
            Try

                Dim lst = rep.GetJobBandID(sID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveJobBand(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try

                Return rep.ActiveJobBand(lstID, sActive)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteJobBand(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try

                Return rep.DeleteJobBand(lstID)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

#End Region
#Region "Title"

    Public Function GetPositions(ByVal _filter As TitleDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPositions(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPositions(ByVal _filter As TitleDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPositions(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPossition(ByVal objTitle As TitleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertPossition(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePosition(ByVal objTitle As TitleDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidatePosition(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePosition_ACTIVITIES(ByVal objTitle As TitleDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidatePosition_ACTIVITIES(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPossition(ByVal objTitle As TitleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyPossition(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActivePositions(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByRef Status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActivePositions(lstID, sActive, Status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckOwnerExist(ByVal objTitle As TitleDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckOwnerExist(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeletePositions(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeletePositions(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetPositionByID(ByVal sID As Decimal) As List(Of ProfileBusiness.TitleDTO)
        Dim lstTitle As List(Of ProfileBusiness.TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPositionByID(sID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPositionID(ByVal ID As Decimal) As Profile.ProfileBusiness.TitleDTO
        Dim lstTitle As Profile.ProfileBusiness.TitleDTO

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPositionID(ID, Me.Log, Common.Common.SystemLanguage.Name, True)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPrintJD(ByVal ID As Decimal) As Profile.ProfileBusiness.TitleDTO
        Dim lstTitle As Profile.ProfileBusiness.TitleDTO

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPrintJD(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetOrgTreeApp() As List(Of OrganizationDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgTreeApp(Common.Common.SystemLanguage.Name, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetOrgOMByID(ByVal _Id As Decimal) As OrganizationDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgOMByID(_Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function AutoGenCodeHuTile(ByVal tableName As String, ByVal colName As String) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.AutoGenCodeHuTile(tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckIsOwner(ByVal OrgID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckIsOwner(OrgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyTitleById(ByVal obj As TitleDTO, ByVal OrgRight As Decimal, ByVal Address As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTitleById(obj, OrgRight, Address, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPositionByOrgID(ByVal _filter As TitleDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "IS_OWNER") As List(Of TitleDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetPositionByOrgID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetTitleByTitleID(ByVal Id As Decimal) As TitleDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTitleByTitleID(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region
#Region "ngach, bac, thang luong"

    Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalaryGroupCombo(dateValue, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function GetSalaryRankCombo(ByVal SalaryLevel As Decimal, ByVal isBlank As Boolean) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalaryRankCombo(SalaryLevel, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function GetSalaryLevelCombo(ByVal SalaryGroup As Decimal, ByVal isBlank As Boolean) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalaryLevelCombo(SalaryGroup, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Function GetSalaryLevelComboNotByGroup(ByVal isBlank As Boolean) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalaryLevelComboNotByGroup(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "List"

    Public Function GetTreeOrgByID(ByVal ID As Decimal) As OrganizationTreeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTreeOrgByID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#Region "RelationshipList"
    Function GetRelationshipGroupList() As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetRelationshipGroupList()
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetRelationshipList(ByVal _filter As RelationshipListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO)
        Dim lstRelationshipList As List(Of RelationshipListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstRelationshipList = rep.GetRelationshipList(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstRelationshipList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetRelationshipList(ByVal _filter As RelationshipListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO)
        Dim lstRelationshipList As List(Of RelationshipListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstRelationshipList = rep.GetRelationshipList(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstRelationshipList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertRelationshipList(objRelationshipList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateRelationshipList(ByVal objRelationshipList As RelationshipListDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateRelationshipList(objRelationshipList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyRelationshipList(objRelationshipList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveRelationshipList(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveRelationshipList(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteRelationshipList(ByVal lstRelationshipList As List(Of RelationshipListDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_Relationship_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteRelationshipList(lstRelationshipList, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "contract procedure"
    Public Function GetContractProcedure(ByVal _filter As ContractProcedureDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractProcedureDTO)
        Dim lstTitle As List(Of ContractProcedureDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetContractProcedure(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetContractProcedure(ByVal _filter As ContractProcedureDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractProcedureDTO)
        Dim lstTitle As List(Of ContractProcedureDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetContractProcedure(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteContractProcedure(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteContractProcedure(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Delete_List_Contract(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Delete_List_Contract(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertContractProcedure(ByVal objContractProcedure As ContractProcedureDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertContractProcedure(objContractProcedure, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function checkContractProcedure(ByVal objContractProcedure As ContractProcedureDTO) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.checkContractProcedure(objContractProcedure)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "org_brand"
    Public Function GetOrgLevel(ByVal org_id As Decimal) As OrganizationDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgLevel(org_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetOrgBrand(ByVal _filter As OrgBrandDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgBrandDTO)
        Dim lstTitle As List(Of OrgBrandDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetOrgBrand(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetOrgBrand(ByVal _filter As OrgBrandDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgBrandDTO)
        Dim lstTitle As List(Of OrgBrandDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetOrgBrand(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteOrgBrand(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteOrgBrand(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertOrgBrand(ByVal objOrgBrand As OrgBrandDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOrgBrand(objOrgBrand, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function checkOrgBrand(ByVal objOrgBrand As OrgBrandDTO) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.checkOrgBrand(objOrgBrand)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "org_Pause"
    Public Function GetOrgLevel_Pause(ByVal org_id As Decimal) As OrganizationDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgLevel_Pause(org_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetOrgPause(ByVal _filter As ORG_PAUSEDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ORG_PAUSEDTO)
        Dim lstTitle As List(Of ORG_PAUSEDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetOrgPause(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetOrgPause(ByVal _filter As ORG_PAUSEDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ORG_PAUSEDTO)
        Dim lstTitle As List(Of ORG_PAUSEDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetOrgPause(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteOrgPause(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteOrgPause(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertOrgPause(ByVal objOrgBrand As ORG_PAUSEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOrgPause(objOrgBrand, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function checkOrgPause(ByVal objOrgBrand As ORG_PAUSEDTO) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.checkOrgPause(objOrgBrand)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Title"

    Public Function GetTitle(ByVal _filter As TitleDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitle(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitle(ByVal _filter As TitleDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitle(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTitle(ByVal objTitle As TitleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTitle(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateTitle(ByVal objTitle As TitleDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateTitle(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTitle(ByVal objTitle As TitleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTitle(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateManager(ByVal lstID As List(Of Decimal), ByVal objTitle As TitleDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateManager(lstID, objTitle, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveTitle(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveTitle(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTitle(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTitle(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetTitleByID(ByVal sID As Decimal) As List(Of ProfileBusiness.TitleDTO)
        Dim lstTitle As List(Of ProfileBusiness.TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitleByID(sID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTitleID(ByVal ID As Decimal) As Profile.ProfileBusiness.TitleDTO
        Dim lstTitle As Profile.ProfileBusiness.TitleDTO

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitleID(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "Chucvu"

    Public Function GetChucvu(ByVal _filter As ChucvuDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvu(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetChucvu(ByVal _filter As ChucvuDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvu(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertChucvu(ByVal objTitle As ChucvuDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertChucvu(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateChucvuCode(ByVal objTitle As ChucvuDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateChucvuCode(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyChucvu(ByVal objTitle As ChucvuDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyChucvu(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveChucvu(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveChucvu(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteChucvu(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteChucvu(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChucvuByID(ByVal sID As Decimal) As List(Of ProfileBusiness.ChucvuDTO)
        Dim lstTitle As List(Of ProfileBusiness.ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuByID(sID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetChucvuID(ByVal ID As Decimal) As Profile.ProfileBusiness.ChucvuDTO
        Dim lstTitle As Profile.ProfileBusiness.ChucvuDTO

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuID(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region


#Region "Chucvu Bld"

    Public Function GetChucvuBld(ByVal _filter As ChucvuDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuBld(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetChucvuBld(ByVal _filter As ChucvuDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuBld(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertChucvuBld(ByVal objTitle As ChucvuDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertChucvuBld(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateChucvuBldCode(ByVal objTitle As ChucvuDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateChucvuBldCode(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyChucvuBld(ByVal objTitle As ChucvuDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyChucvuBld(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveChucvuBld(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveChucvuBld(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteChucvuBld(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteChucvuBld(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChucvuBldByID(ByVal sID As Decimal) As List(Of ProfileBusiness.ChucvuDTO)
        Dim lstTitle As List(Of ProfileBusiness.ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuBldByID(sID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleBLDsByOrg(ByVal org_id As Decimal, Optional ByVal emp_id As Decimal? = Nothing) As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ProfileBusiness.ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitleBLDsByOrg(org_id, emp_id)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetChucvuBldID(ByVal ID As Decimal) As Profile.ProfileBusiness.ChucvuDTO
        Dim lstTitle As Profile.ProfileBusiness.ChucvuDTO

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuBldID(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "Chucvu Tbl"

    Public Function GetChucvuTbl(ByVal _filter As ChucvuDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuTbl(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetChucvuTbl(ByVal _filter As ChucvuDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)
        Dim lstTitle As List(Of ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuTbl(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertChucvuTbl(ByVal objTitle As ChucvuDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertChucvuTbl(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateChucvuTblCode(ByVal objTitle As ChucvuDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateChucvuTblCode(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyChucvuTbl(ByVal objTitle As ChucvuDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyChucvuTbl(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveChucvuTbl(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveChucvuTbl(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteChucvuTbl(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteChucvuTbl(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChucvuTblByID(ByVal sID As Decimal) As List(Of ProfileBusiness.ChucvuDTO)
        Dim lstTitle As List(Of ProfileBusiness.ChucvuDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuTblByID(sID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetChucvuTblID(ByVal ID As Decimal) As Profile.ProfileBusiness.ChucvuDTO
        Dim lstTitle As Profile.ProfileBusiness.ChucvuDTO

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetChucvuTblID(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "TitleConcurrent"
    Public Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO,
                            ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)
        Dim lstTitleConcurrent As List(Of TitleConcurrentDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitleConcurrent = rep.GetTitleConcurrent1(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstTitleConcurrent
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)
        Dim lstTitleConcurrent As List(Of TitleConcurrentDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitleConcurrent = rep.GetTitleConcurrent1(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
                Return lstTitleConcurrent
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)
        Dim lstTitleConcurrent As List(Of TitleConcurrentDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitleConcurrent = rep.GetTitleConcurrent(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitleConcurrent
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)
        Dim lstTitleConcurrent As List(Of TitleConcurrentDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitleConcurrent = rep.GetTitleConcurrent(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitleConcurrent
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTitleConcurrent(objTitleConcurrent, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTitleConcurrent(objTitleConcurrent, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTitleConcurrent(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTitleConcurrent(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Document"
    Public Function GetAll_Document(ByVal _filter As DocumentDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DocumentDTO)
        Dim lstTitle As List(Of DocumentDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetAll_Document(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetAll_Document(ByVal _filter As DocumentDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DocumentDTO)
        Dim lstTitle As List(Of DocumentDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetAll_Document(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ActiveDocument(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveDocument(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteDocument(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteDocument(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Check_Exit_Document(ByVal objDocument As DocumentDTO) As Decimal
        Dim count As Decimal
        Using rep As New ProfileBusinessClient
            Try
                count = rep.Check_Exit_Document(objDocument)
                Return count
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyDocument(ByVal objTitle As DocumentDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyDocument(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertDocument(ByVal objTitle As DocumentDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertDocument(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "bhld item"

    Public Function GetBHLDItem(ByVal _filter As BHLDItemDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BHLDItemDTO)
        Dim lstContractType As List(Of BHLDItemDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstContractType = rep.GetBHLDItem(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstContractType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetBHLDItem(ByVal _filter As BHLDItemDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BHLDItemDTO)
        Dim lstContractType As List(Of BHLDItemDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstContractType = rep.GetBHLDItem(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstContractType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function checkOrderNum(ByVal id As Decimal, ByVal num As Decimal) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.checkOrderNum(id, num)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertBHLDItem(ByVal objContractType As BHLDItemDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertBHLDItem(objContractType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateBHLDItem(ByVal objContractType As BHLDItemDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateBHLDItem(objContractType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyBHLDItem(ByVal objContractType As BHLDItemDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyBHLDItem(objContractType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveBHLDItem(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveBHLDItem(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteBHLDItem(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteBHLDItem(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "ContractType"

    Public Function GetContractType(ByVal _filter As ContractTypeDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO)
        Dim lstContractType As List(Of ContractTypeDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstContractType = rep.GetContractType(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstContractType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetContractType(ByVal _filter As ContractTypeDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO)
        Dim lstContractType As List(Of ContractTypeDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstContractType = rep.GetContractType(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstContractType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertContractType(ByVal objContractType As ContractTypeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertContractType(objContractType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateContractType(ByVal objContractType As ContractTypeDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateContractType(objContractType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyContractType(ByVal objContractType As ContractTypeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyContractType(objContractType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveContractType(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveContractType(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteContractType(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteContractType(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "WelfareList"

    Public Function GetWelfareList(ByVal _filter As WelfareListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO)
        Dim lstWelfareList As List(Of WelfareListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWelfareList = rep.GetWelfareList(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstWelfareList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWelfareList(ByVal _filter As WelfareListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO)
        Dim lstWelfareList As List(Of WelfareListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWelfareList = rep.GetWelfareList(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
                Return lstWelfareList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWelfareList(ByVal objWelfareList As WelfareListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWelfareList(objWelfareList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateWelfareList(ByVal objWelfareList As WelfareListDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateWelfareList(objWelfareList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWelfareList(ByVal objWelfareList As WelfareListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWelfareList(objWelfareList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveWelfareList(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveWelfareList(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWelfareList(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWelfareList(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AllowanceList"

    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS asc") As List(Of AllowanceListDTO)
        Dim lstAllowanceList As List(Of AllowanceListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstAllowanceList = rep.GetAllowanceList(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAllowanceList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)
        Dim lstAllowanceList As List(Of AllowanceListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstAllowanceList = rep.GetAllowanceList(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAllowanceList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertAllowanceList(objAllowanceList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAllowanceList(ByVal objAllowanceList As AllowanceListDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateAllowanceList(objAllowanceList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyAllowanceList(objAllowanceList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAllowanceList(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveAllowanceList(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAllowanceList(ByVal lstAllowanceList As List(Of AllowanceListDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_ALLOWANCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteAllowanceList(lstAllowanceList, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Danh muc tham so he thong"
    Public Function ValidateOtherList(ByVal objOtherList As OtherListDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_OTHER_LIST_" & IIf(True, "Blank", "NoBlank"))
                Return rep.ValidateOtherList(objOtherList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Nation- Danh muc Quoc gia"
    Public Function GetNation(ByVal _filter As NationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO)
        Dim lstNation As List(Of NationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstNation = rep.GetNation(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstNation
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetNation(ByVal _filter As NationDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO)
        Dim lstTitle As List(Of NationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetNation(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function InsertNation(ByVal objNation As NationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertNation(objNation, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyNation(ByVal objNation As NationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyNation(objNation, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateNation(ByVal objNation As NationDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateNation(objNation)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveNation(ByVal lstNation As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveNation(lstNation, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteNation(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_NATION_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteNation(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Province- Danh muc Tinh Thanh"
    Public Function GetProvinceByNationID(ByVal sNationID As Decimal, ByVal sACTFLG As String) As List(Of ProvinceDTO)
        Dim lstProvince As List(Of ProvinceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstProvince = rep.GetProvinceByNationID(sNationID, sACTFLG)
                Return lstProvince
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetProvinceByNationCode(ByVal sNationCode As String, ByVal sACTFLG As String) As List(Of ProvinceDTO)
        Dim lstProvince As List(Of ProvinceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstProvince = rep.GetProvinceByNationCode(sNationCode, sACTFLG)
                Return lstProvince
            Catch ex As Exception
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvince(ByVal _filter As ProvinceDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO)
        Dim lstProvince As List(Of ProvinceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstProvince = rep.GetProvince(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstProvince
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvince(ByVal _filter As ProvinceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO)
        Dim lstProvince As List(Of ProvinceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstProvince = rep.GetProvince(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstProvince
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertProvince(ByVal objProvince As ProvinceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertProvince(objProvince, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyProvince(ByVal objProvince As ProvinceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyProvince(objProvince, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateProvince(ByVal objProvince As ProvinceDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateProvince(objProvince)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveProvince(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveProvince(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteProvince(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_PROVINCE_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteProvince(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "District- Danh muc Quan Huyen"
    Public Function GetDistrictByProvinceID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of DistrictDTO)
        Dim lstDistrict As List(Of DistrictDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDistrict = rep.GetDistrictByProvinceID(sProvinceID, sACTFLG)
                Return lstDistrict
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDistrict(ByVal _filter As DistrictDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO)
        Dim lstDistrict As List(Of DistrictDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDistrict = rep.GetDistrict(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDistrict
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDistrict(ByVal _filter As DistrictDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO)
        Dim lstProvince As List(Of DistrictDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstProvince = rep.GetDistrict(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstProvince
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertDistrict(ByVal objDistrict As DistrictDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & objDistrict.PROVINCE_ID & "_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & objDistrict.PROVINCE_ID & "_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertDistrict(objDistrict, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateDistrict(ByVal objData As DistrictDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateDistrict(objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyDistrict(ByVal objDistrict As DistrictDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & objDistrict.PROVINCE_ID & "_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & objDistrict.PROVINCE_ID & "_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyDistrict(objDistrict, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveDistrict(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal provinceID As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveDistrict(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteDistrict(ByVal lstDecimals As List(Of Decimal), ByRef strError As String, ByVal provinceID As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteDistrict(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Ward danh mục xã phường"
    Public Function GetWardByDistrictID(ByVal sDistrictID As Decimal, ByVal sACTFLG As String) As List(Of Ward_DTO)
        Dim lstDistrict As List(Of Ward_DTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDistrict = rep.GetWardByDistrictID(sDistrictID, sACTFLG)
                Return lstDistrict
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWard(ByVal _filter As Ward_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO)
        Dim lstDistrict As List(Of Ward_DTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDistrict = rep.GetWard(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDistrict
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWard(ByVal _filter As Ward_DTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO)
        Dim lstDistrict As List(Of Ward_DTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDistrict = rep.GetWard(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstDistrict
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateWard(ByVal objData As Ward_DTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateWard(objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertWard(ByVal objWard As Ward_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWard(objWard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWard(ByVal objWard As Ward_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWard(objWard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveWard(ByVal lstWard As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try

                Return rep.ActiveWard(lstWard, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWard(ByVal lstDecimals As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWard(lstDecimals, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Bank- Danh muc Ngan hang"

    Public Function GetBank(ByVal _filter As BankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO)
        Dim lstBank As List(Of BankDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstBank = rep.GetBank(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstBank
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetBank(ByVal _filter As BankDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO)
        Dim lstBank As List(Of BankDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstBank = rep.GetBank(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstBank
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertBank(ByVal objBank As BankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertBank(objBank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyBank(ByVal objBank As BankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyBank(objBank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateBank(ByVal objBank As BankDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateBank(objBank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveBank(ByVal lstBank As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveBank(lstBank, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteBank(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_BANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteBank(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "BankBranch- Danh muc Chi nhanh Ngan hang"

    Public Function GetBankBranchByBankID(ByVal sBank_ID As Decimal) As List(Of BankBranchDTO)
        Dim lstBankBranch As List(Of BankBranchDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstBankBranch = rep.GetBankBranchByBankID(sBank_ID)
                Return lstBankBranch
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetBankBranch(ByVal _filter As BankBranchDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO)
        Dim lstBankBranch As List(Of BankBranchDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstBankBranch = rep.GetBankBranch(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstBankBranch
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetBankBranch(ByVal _filter As BankBranchDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO)
        Dim lstBankBranch As List(Of BankBranchDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstBankBranch = rep.GetBankBranch(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstBankBranch
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertBankBranch(ByVal objBankBranch As BankBranchDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_BANK_BRANCH_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_BANK_BRANCH_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertBankBranch(objBankBranch, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyBankBranch(ByVal objBankBranch As BankBranchDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_BANK_BRANCH_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_BANK_BRANCH_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyBankBranch(objBankBranch, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateBankBranch(ByVal objBankBranch As BankBranchDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateBankBranch(objBankBranch)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveBankBranch(ByVal lstBankBranch As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveBankBranch(lstBankBranch, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteBankBranch(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteBankBranch(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Asset - Danh muc tai san cap phat"

    Public Function GetAsset(ByVal _filter As AssetDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO)
        Dim lstAsset As List(Of AssetDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstAsset = rep.GetAsset(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAsset
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAsset(ByVal _filter As AssetDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO)
        Dim lstAsset As List(Of AssetDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstAsset = rep.GetAsset(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAsset
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAsset(ByVal objAsset As AssetDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertAsset(objAsset, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAsset(ByVal objAsset As AssetDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyAsset(objAsset, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAsset(ByVal objAsset As AssetDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateAsset(objAsset)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAsset(ByVal lstAsset As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveAsset(lstAsset, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAsset(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteAsset(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function load_DropdownList_by_Parent(ByVal sParentID As Decimal, ByVal lstDTO As IEnumerable, ByVal sACTFLG As String) As IEnumerable
        Using rep As New ProfileBusinessClient
            Try
                lstDTO = rep.GetProvinceByNationID(sParentID, sACTFLG)
                Return lstDTO
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "StaffRank"

    Public Function GetStaffRank(ByVal _filter As StaffRankDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO)
        Dim lstStaffRank As List(Of StaffRankDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstStaffRank = rep.GetStaffRank(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstStaffRank
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStaffRank(ByVal _filter As StaffRankDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO)
        Dim lstStaffRank As List(Of StaffRankDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstStaffRank = rep.GetStaffRank(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstStaffRank
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertStaffRank(ByVal objStaffRank As StaffRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertStaffRank(objStaffRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateStaffRank(ByVal objStaffRank As StaffRankDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ValidateStaffRank(objStaffRank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyStaffRank(ByVal objStaffRank As StaffRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyStaffRank(objStaffRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveStaffRank(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveStaffRank(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteStaffRank(ByVal lstStaffRank As List(Of StaffRankDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_HU_STAFF_RANK_LIST_" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteStaffRank(lstStaffRank, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Danh mục bảo hộ lao động"
    Public Function GetLabourProtection(ByVal _filter As LabourProtectionDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "ID desc") As List(Of LabourProtectionDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetLabourProtection(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertLabourProtection(ByVal objTitle As LabourProtectionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertLabourProtection(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyLabourProtection(ByVal objTitle As LabourProtectionDTO,
                                ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyLabourProtection(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveLabourProtection(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveLabourProtection(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteLabourProtection(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteLabourProtection(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateLabourProtection(ByVal objLabourProtection As LabourProtectionDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                CacheManager.ClearValue("HU_LabourProtection" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("HU_LabourProtection" & IIf(False, "Blank", "NoBlank"))
                Return rep.ValidateLabourProtection(objLabourProtection)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Danh mục thông tin lương"
    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetPeriodbyYear(year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "DM Khen thưởng (CommendList)"
    Public Function GetCommendList(ByVal _filter As CommendListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO)
        Dim lstCommendList As List(Of CommendListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommendList = rep.GetCommendList(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCommendList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendList(ByVal _filter As CommendListDTO,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO)
        Dim lstTitle As List(Of CommendListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetCommendList(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendListID(ByVal ID As Decimal) As List(Of CommendListDTO)
        Dim lstCommendList As List(Of CommendListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommendList = rep.GetCommendListID(ID)
                Return lstCommendList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetListCommendList(ByVal actflg As String) As List(Of CommendListDTO)
        Dim lstCommendList As List(Of CommendListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommendList = rep.GetListCommendList(actflg)
                Return lstCommendList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertCommendList(ByVal objCommendList As CommendListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCommendList(objCommendList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCommendList(ByVal objCommendList As CommendListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCommendList(objCommendList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveCommendList(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveCommendList(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCommendList(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCommendList(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidateCommendList(ByVal _validate As CommendListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCommendList(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Function GetCommendCode(ByVal id As Decimal) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommendCode(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return String.Empty
    End Function
#End Region
#Region "DM Kỷ luật (DisciplineList)"
    Public Function GetDisciplineList(ByVal _filter As DisciplineListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineListDTO)
        Dim lstDisciplineList As List(Of DisciplineListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDisciplineList = rep.GetDisciplineList(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDisciplineList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetDisciplineList(ByVal _filter As DisciplineListDTO,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineListDTO)
        Dim lstTitle As List(Of DisciplineListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetDisciplineList(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetDisciplineListID(ByVal ID As Decimal) As List(Of DisciplineListDTO)
        Dim lstDisciplineList As List(Of DisciplineListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDisciplineList = rep.GetDisciplineListID(ID)
                Return lstDisciplineList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetListDisciplineList(ByVal actflg As String) As List(Of DisciplineListDTO)
        Dim lstDisciplineList As List(Of DisciplineListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDisciplineList = rep.GetListDisciplineList(actflg)
                Return lstDisciplineList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertDisciplineList(ByVal objDisciplineList As DisciplineListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertDisciplineList(objDisciplineList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyDisciplineList(ByVal objDisciplineList As DisciplineListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyDisciplineList(objDisciplineList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveDisciplineList(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveDisciplineList(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteDisciplineList(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteDisciplineList(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidateDisciplineList(ByVal _validate As DisciplineListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateDisciplineList(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Function GetDisciplineCode(ByVal id As Decimal) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDisciplineCode(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return String.Empty
    End Function
#End Region
#Region "Commend_Level - Cấp khen thưởng"
    Public Function GetCommendLevel(ByVal _filter As CommendLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO)
        Dim lstCommendLevel As List(Of CommendLevelDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommendLevel = rep.GetCommendLevel(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCommendLevel
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendLevel(ByVal _filter As CommendLevelDTO,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO)
        Dim lstCommendLevel As List(Of CommendLevelDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommendLevel = rep.GetCommendLevel(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCommendLevel
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendLevelID(ByVal ID As Decimal) As CommendLevelDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommendLevelID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertCommendLevel(ByVal objCommendLevel As CommendLevelDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCommendLevel(objCommendLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidateCommendLevel(ByVal _validate As CommendLevelDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCommendLevel(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyCommendLevel(ByVal objCommendLevel As CommendLevelDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCommendLevel(objCommendLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActiveCommendLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveCommendLevel(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function DeleteCommendLevel(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCommendLevel(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Work Place"

    Public Function GetWorkPlace(ByVal _filter As WorkPlaceDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkPlaceDTO)
        Dim lstWP As List(Of WorkPlaceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWP = rep.GetWorkPlace(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstWP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWorkPlace(ByVal _filter As WorkPlaceDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkPlaceDTO)
        Dim lstWP As List(Of WorkPlaceDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWP = rep.GetWorkPlace(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstWP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWorkPlace(ByVal objWorkPlace As WorkPlaceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorkPlace(objWorkPlace, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateWorkPlace(ByVal _validate As WorkPlaceDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateWorkPlace(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorkPlace(ByVal objWorkPlace As WorkPlaceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorkPlace(objWorkPlace, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveWorkPlace(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveWorkPlace(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWorkPlace(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWorkPlace(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetWorkPlaceID(ByVal ID As Decimal) As WorkPlaceDTO
        Dim WP As ProfileBusiness.WorkPlaceDTO

        Using rep As New ProfileBusinessClient
            Try
                WP = rep.GetWorkPlaceID(ID)
                Return WP
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#End Region

#Region " In hop dong hang loat"
    Public Function GetCheckContractTypeID(ByVal listID As String) As DataTable
        Dim lstContractType As DataTable

        Using rep As New ProfileBusinessClient
            Try
                lstContractType = rep.GetCheckContractTypeID(listID)
                Return lstContractType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


#End Region

#Region "Location"

    Public Function GetLocationID(ByVal ID As Decimal) As LocationDTO
        Dim query As LocationDTO
        Using rep As New ProfileBusinessClient
            Try
                query = rep.GetLocationID(ID)
                Return query
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetLocation(ByVal sACT As String, ByVal lstOrgID As List(Of Decimal)) As List(Of LocationDTO)
        Dim query As List(Of LocationDTO)
        Using rep As New ProfileBusinessClient
            Try
                query = rep.GetLocation(sACT, lstOrgID)
                Return query
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetLocation_V2(ByVal _filter As LocationDTO,
                                   ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer, ByVal _param As ProfileBusiness.ParamDTO,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LocationDTO)
        Dim lstWorking As List(Of LocationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetLocation_V2(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetLocation_V2(ByVal _filter As LocationDTO, ByVal _param As ProfileBusiness.ParamDTO,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LocationDTO)
        Dim lstWorking As List(Of LocationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetLocation_V2(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertLocation(ByVal objLocation As LocationDTO, ByRef gID As Decimal) As Boolean
        Dim result As Boolean
        Using rep As New ProfileBusinessClient
            Try
                result = rep.InsertLocation(objLocation, Me.Log, gID)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyLocation(ByVal objLocation As LocationDTO, ByRef gID As Decimal) As Boolean
        Dim result As Boolean
        Using rep As New ProfileBusinessClient
            Try
                result = rep.ModifyLocation(objLocation, Me.Log, gID)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveLocation(ByVal lstLocation As List(Of LocationDTO), ByVal sActive As String) As Boolean
        Dim result As Boolean
        Using rep As New ProfileBusinessClient
            Try
                result = rep.ActiveLocation(lstLocation, sActive, Me.Log)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveLocationID(ByVal lstLocation As LocationDTO, ByVal sActive As String) As Boolean
        Dim result As Boolean
        Using rep As New ProfileBusinessClient
            Try
                result = rep.ActiveLocationID(lstLocation, sActive, Me.Log)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteLocationID(ByVal lstlocation As Decimal) As Boolean
        Dim result As Boolean
        Using rep As New ProfileBusinessClient
            Try
                result = rep.DeleteLocationID(lstlocation, Me.Log)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region
#Region "danh mục người ký"
    Public Function GET_HU_SIGNER(ByVal _filter As SignerDTO, ByVal _param As ParamDTO) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_HU_SIGNER(_filter, _param, Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function
    ''THÊM
    Public Function INSERT_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Return rep.INSERT_HU_SIGNER(PA)
            End Using
        Catch ex As Exception

        End Try
    End Function
    ''SỬA
    Public Function UPDATE_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Return rep.UPDATE_HU_SIGNER(PA)
            End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function CHECK_EXIT(ByVal P_ID As String, ByVal idemp As Decimal, ByVal ORG_ID As Decimal, ByVal title_name As String) As Decimal
        Try
            Using rep As New ProfileBusinessClient
                Return rep.CHECK_EXIT(P_ID, idemp, ORG_ID, title_name)
            End Using
        Catch ex As Exception

        End Try
    End Function
    Public Function DeactiveAndActiveSigner(ByVal lstID As String, ByVal sActive As Decimal)
        Try
            Using rep As New ProfileBusinessClient
                Return rep.DeactiveAndActiveSigner(lstID, sActive)
            End Using
        Catch ex As Exception

        End Try
    End Function
    Public Function DeleteSigner(ByVal lstID As String)
        Try
            Using rep As New ProfileBusinessClient
                Return rep.DeleteSigner(lstID)
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region
    Public Function GetNameOrg(ByVal org_id As Decimal) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetNameOrg(org_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetMaxId() As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetMaxId()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_LOCATION_EXITS(ByVal P_ID As Decimal?, ByVal ORG_ID As Decimal) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Return rep.CHECK_LOCATION_EXITS(P_ID, ORG_ID)
            End Using
        Catch ex As Exception

        End Try
    End Function

#Region "Signer Setup"
    Public Function GetSingerSetups(ByVal _filter As SignerSetupDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Dim lst = rep.GetSingerSetups(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSingerSetups(ByVal _filter As SignerSetupDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSingerSetups(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListSignerSetup(ByVal _filter As SignerSetupDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SignerSetupDTO)
        Using rep As New ProfileBusinessClient
            Try
                Dim lst = rep.GetListSignerSetup(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListSignerSetup(ByVal _filter As SignerSetupDTO, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SignerSetupDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListSignerSetup(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function InsertSignerSetup(ByVal objSignSet As SignerSetupDTO, ByVal _lst_Type As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertSignerSetup(objSignSet, Me.Log, _lst_Type)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSignSet(ByVal _validate As SignerSetupDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateSignSet(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySignerSetup(ByVal objSignSet As SignerSetupDTO, ByVal _lst_Type As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifySignerSetup(objSignSet, Me.Log, _lst_Type)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveSignerSetup(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveSignerSetup(lstID, sActive, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSignerSetup(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteSignerSetup(lstID, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

    Public Function GET_EXPORT_ORG_BARND() As DataSet
        Try
            Using rep As New ProfileBusinessClient
                Return rep.GET_EXPORT_ORG_BARND()
            End Using
        Catch ex As Exception
        End Try
    End Function

    Public Function IMPORT_HU_ORG_BARND(ByVal P_DOCXML As String) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Return rep.IMPORT_HU_ORG_BARND(P_DOCXML, Me.Log.Username.ToUpper)
            End Using
        Catch ex As Exception
        End Try
    End Function

#Region "ReminderList"

    Public Function GetReminderList(ByVal _filter As ReminderListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReminderListDTO)
        Dim lstReminderList As List(Of ReminderListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstReminderList = rep.GetReminderList(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstReminderList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetReminderList(ByVal _filter As ReminderListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReminderListDTO)
        Dim lstReminderList As List(Of ReminderListDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstReminderList = rep.GetReminderList(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
                Return lstReminderList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function ModifyReminderList(ByVal obj As ReminderListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyReminderList(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActiveReminderList(ByVal lstID As List(Of Decimal), ByVal sActive As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveReminderList(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
End Class
