Namespace ProfileBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IHU_WorkingBusiness
        <OperationContract()>
        Function GetWorkings() As List(Of String)
    End Interface
End Namespace


