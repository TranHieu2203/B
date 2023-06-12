Imports Performance.PerformanceBusiness

Partial Class PerformanceRepository

#Region "Criteria"

    Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstCriteria = rep.GetCriteria(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstCriteria = rep.GetCriteria(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertCriteria(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateCriteria(objCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyCriteria(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveCriteria(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCriteria(ByVal lstCriteria As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteCriteria(lstCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetPE_Criteria_HTCH(ByVal _filter As PE_Criteria_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Criteria_HTCHDTO)
        Dim lstCriteria As List(Of PE_Criteria_HTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstCriteria = rep.GetPE_Criteria_HTCH(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPE_Criteria_HTCH(ByVal _filter As PE_Criteria_HTCHDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Criteria_HTCHDTO)
        Dim lstCriteria As List(Of PE_Criteria_HTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstCriteria = rep.GetPE_Criteria_HTCH(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertPE_Criteria_HTCH(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidatePE_Criteria_HTCH(objCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyPE_Criteria_HTCH(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function DeletePE_Criteria_HTCH(ByVal lstCriteria As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeletePE_Criteria_HTCH(lstCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GET_CRITERIAL_DATA_IMPORT() As DataSet
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GET_CRITERIAL_DATA_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function IMPORT_CRITERIAL_DATA(ByVal P_DOCXML As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.IMPORT_CRITERIAL_DATA(Me.Log, P_DOCXML)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region
#Region "Classification"

    Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetClassification(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetClassification(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertClassification(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateClassification(objClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyClassification(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveClassification(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CalKPI(ByVal param As ParamDTO) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CalKPI(param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function CalKPI_Result(ByVal param As ParamDTO) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CalKPI_Result(param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteClassification(ByVal lstClassification As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteClassification(lstClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Classification HTCH"

    Public Function GetClassificationHTCH(ByVal _filter As ClassificationHTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationHTCHDTO)
        Dim lstClassification As List(Of ClassificationHTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetClassificationHTCH(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassificationHTCH(ByVal _filter As ClassificationHTCHDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationHTCHDTO)
        Dim lstClassification As List(Of ClassificationHTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetClassificationHTCH(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertClassificationHTCH(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateClassificationHTCH(objClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyClassificationHTCH(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveClassificationHTCH(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveClassificationHTCH(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function DeleteClassificationHTCH(ByVal lstClassification As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteClassificationHTCH(lstClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ObjectGroup"

    Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)
        Dim lstObjectGroup As List(Of ObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstObjectGroup = rep.GetObjectGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstObjectGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)
        Dim lstObjectGroup As List(Of ObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstObjectGroup = rep.GetObjectGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstObjectGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertObjectGroup(objObjectGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateObjectGroup(ByVal objObjectGroup As ObjectGroupDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateObjectGroup(objObjectGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyObjectGroup(objObjectGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActiveObjectGroup(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteObjectGroup(ByVal lstObjectGroup As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteObjectGroup(lstObjectGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Period"

    Public Function GetPeriod(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Dim lstPeriod As List(Of PeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriod(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPeriod(ByVal _filter As PeriodDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Dim lstPeriod As List(Of PeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriod(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPeriodById(ByVal _filter As PeriodDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Dim lstPeriod As List(Of PeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriodById(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertPeriod(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePeriod(ByVal objPeriod As PeriodDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidatePeriod(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyPeriod(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ActivePeriod(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeletePeriod(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Period HTCH"

    Public Function GetPeriodHTCH(ByVal _filter As PE_Period_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)
        Dim lstPeriod As List(Of PE_Period_HTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriodHTCH(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPeriodHTCH(ByVal _filter As PE_Period_HTCHDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)
        Dim lstPeriod As List(Of PE_Period_HTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriodHTCH(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPeriodHTCHById(ByVal _filter As PE_Period_HTCHDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)
        Dim lstPeriod As List(Of PE_Period_HTCHDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstPeriod = rep.GetPeriodHTCHById(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertPeriodHTCH(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidatePeriodHTCH(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyPeriodHTCH(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function DeletePeriodHTCH(ByVal lstPeriod As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeletePeriodHTCH(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class
