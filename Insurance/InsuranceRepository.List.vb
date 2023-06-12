Imports Insurance.InsuranceBusiness

Partial Class InsuranceRepository
    Inherits InsuranceRepositoryBase

#Region "Danh mục nơi khám chữa bệnh"
    Public Function GetINS_WHEREHEALTH(ByVal _filter As INS_WHEREHEALTHDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_WHEREHEALTHDTO)
        Dim lstDMVS As List(Of INS_WHEREHEALTHDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetINS_WHEREHEALTH(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetINS_WHEREEXPORT() As List(Of INS_WHEREHEALTHDTO)
        Dim lstDMVS As List(Of INS_WHEREHEALTHDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetINS_WHEREEXPORT()
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetStatuSo() As DataTable
        Dim lstDMVS As DataTable

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetStatuSo()
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetStatuHE() As DataTable
        Dim lstDMVS As DataTable

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetStatuHE()
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetHU_PROVINCE(Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "NAME_VN desc") As DataTable
        Dim lstDMVS As DataTable

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetHU_PROVINCE(PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetHU_DISTRICT(Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "NAME_VN desc") As DataTable
        Dim lstDMVS As DataTable

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetHU_DISTRICT(PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_WHEREHEALTH(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateINS_WHEREHEALTH(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_WHEREHEALTH(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveINS_WHEREHEALTH(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveINS_WHEREHEALTH(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_WHEREHEALTH(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_WHEREHEALTH(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetDistrictByIDPro(ByVal province_ID As Decimal) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetDistrictByIDPro(province_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Danh muc bien dong bao hiem"

    Public Function GetInsChangeType(ByVal _filter As INS_CHANGE_TYPEDTO,
                                    Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_CHANGE_TYPEDTO)
        Dim lstInsChangeType As List(Of INS_CHANGE_TYPEDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstInsChangeType = rep.GetInsChangeType(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstInsChangeType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertInsChangeType(objInsChangeType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateInsChangeType(objInsChangeType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyInsChangeType(objInsChangeType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveInsChangeType(ByVal lstInsChangeType As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveInsChangeType(lstInsChangeType, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteInsChangeType(ByVal lstInsChangeType As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteInsChangeType(lstInsChangeType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Chế độ bảo hiểm"
    Public Function GetEntitledType(ByVal _filter As INS_ENTITLED_TYPE_DTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_ENTITLED_TYPE_DTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetEntitledType(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertEntitledType(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO,
                                ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyEntitledType(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateEntitledType(ByVal _validate As INS_ENTITLED_TYPE_DTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateEntitledType(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveEntitledType(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveEntitledType(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteEntitledType(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteEntitledType(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Quy đinh đối tượng"
    Public Function GetSpecifiedObjects(ByVal _filter As INS_SPECIFIED_OBJECTS_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_SPECIFIED_OBJECTS_DTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetSpecifiedObjects(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertSpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertSpecifiedObjects(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function VALIDATE_SPECIFIED_OBJECTS(ByVal effect_date As Date, ByVal Foreigner As Boolean, ByVal id As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.VALIDATE_SPECIFIED_OBJECTS(effect_date, Foreigner, id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifySpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO,
                                ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifySpecifiedObjects(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ActiveSpecifiedObjects(ByVal lstID As List(Of Decimal), ByVal bActive As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveSpecifiedObjects(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSpecifiedObjects(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteSpecifiedObjects(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ObjectPayInsurrance(ByVal lstID As List(Of String), ByVal objName As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ObjectPayInsurrance(lstID, objName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Danh mục vùng đóng bảo hiểm"
    Public Function GetINS_REGION(ByVal _filter As INS_REGION_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REGION_DTO)
        Dim lstDMVS As List(Of INS_REGION_DTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetINS_REGION(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_REGION(ByVal objDMVS As INS_REGION_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_REGION(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateINS_REGION(ByVal objDMVS As INS_REGION_DTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateINS_REGION(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyINS_REGION(ByVal objDMVS As INS_REGION_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_REGION(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveINS_REGION(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveINS_REGION(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_REGION(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_REGION(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Danh mục Chi phí theo cấp"
    Public Function GetINS_COST_FOLLOW_LEVER(ByVal _filter As INS_COST_FOLLOW_LEVERDTO,
                                            Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_COST_FOLLOW_LEVERDTO)
        Dim lstDMVS As List(Of INS_COST_FOLLOW_LEVERDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstDMVS = rep.GetINS_COST_FOLLOW_LEVER(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstDMVS
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_COST_FOLLOW_LEVER(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateINS_COST_FOLLOW_LEVER(objDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_COST_FOLLOW_LEVER(objDMVS, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveINS_COST_FOLLOW_LEVER(ByVal lstDMVS As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveINS_COST_FOLLOW_LEVER(lstDMVS, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_COST_FOLLOW_LEVER(ByVal lstDMVS As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_COST_FOLLOW_LEVER(lstDMVS)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Danh mục nhóm hưởng chế độ bảo hiểm"
    Public Function GetINS_REGIMES(ByVal _filter As INS_GROUP_REGIMESDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "REGIMES_NAME desc") As List(Of INS_GROUP_REGIMESDTO)
        Dim lstREGIMES As List(Of INS_GROUP_REGIMESDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstREGIMES = rep.GetINS_REGIMES(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstREGIMES
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_REGIMES(ByVal lstREGIMES As INS_GROUP_REGIMESDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_REGIMES(lstREGIMES, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyINS_REGIMES(ByVal lstREGIMES As INS_GROUP_REGIMESDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_REGIMES(lstREGIMES, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveINS_REGIMES(ByVal lstREGIMES As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveINS_REGIMES(lstREGIMES, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_REGIMES(ByVal lstREGIMES As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_REGIMES(lstREGIMES)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Danh mục chương trình bảo hiểm"
    Public Function GetINS_Program(ByVal _filter As INS_LIST_PROGRAMDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CODE desc") As List(Of INS_LIST_PROGRAMDTO)
        Dim lstREGIMES As List(Of INS_LIST_PROGRAMDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstREGIMES = rep.GetINS_Program(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstREGIMES
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_Program(ByVal lstREGIMES As INS_LIST_PROGRAMDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_Program(lstREGIMES, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyINS_Program(ByVal lstREGIMES As INS_LIST_PROGRAMDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_Program(lstREGIMES, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveINS_Program(ByVal lstREGIMES As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ActiveINS_Program(lstREGIMES, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteINS_Program(ByVal lstREGIMES As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DeleteINS_Program(lstREGIMES)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Danh mục hợp đồng bảo hiểm sức khỏe"
    Public Function GetINS_LIST_CONTRACT(ByVal _filter As INS_LIST_CONTRACTDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "ID desc") As List(Of INS_LIST_CONTRACTDTO)
        Dim lstREGIMES As List(Of INS_LIST_CONTRACTDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstREGIMES = rep.GetINS_LIST_CONTRACT(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstREGIMES
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetINS_LIST_CONTRACT_PKG(ByVal CONTRACT_INS_NO As String,
                                             ByVal YEAR As Integer?,
                                             ByVal ORG_INSURANCE As Integer?,
                                             Optional ByVal PAGE As Integer = 0,
                                             Optional ByVal SIZE As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "ID desc") As List(Of INS_LIST_CONTRACTDTO)
        Dim lstREGIMES As List(Of INS_LIST_CONTRACTDTO)

        Using rep As New InsuranceBusinessClient
            Try
                lstREGIMES = rep.GetINS_LIST_CONTRACT_PKG(CONTRACT_INS_NO, YEAR, ORG_INSURANCE, PAGE, SIZE, Total, Sorts, Me.Log)
                Return lstREGIMES
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetINS_LIST_CONTRACT_BY_ID(ID As Decimal?) As INS_LIST_CONTRACTDTO
        Dim lstREGIMES As INS_LIST_CONTRACTDTO

        Using rep As New InsuranceBusinessClient
            Try
                lstREGIMES = rep.GetINS_LIST_CONTRACT_BY_ID(ID)
                Return lstREGIMES
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_LIST_CONTRACT(ByVal objTitle As INS_LIST_CONTRACTDTO,
                                ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.InsertINS_LIST_CONTRACT(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyINS_LIST_CONTRACT(ByVal objTitle As INS_LIST_CONTRACTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ModifyINS_LIST_CONTRACT(objTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DELETE_INS_LIST_CONTRACT(ByVal lstREGIMES As List(Of Decimal)) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.DELETE_INS_LIST_CONTRACT(lstREGIMES, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "System"
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.AutoGenCode(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As InsuranceCommonTABLE_NAME) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "System"

    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.ValidateCombobox(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

End Class
