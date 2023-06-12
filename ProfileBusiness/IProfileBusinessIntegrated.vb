' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.

Namespace ProfileBusiness.ServiceContracts

    <ServiceContract()>
    Public Interface IProfileBusinessIntegrated

        <OperationContract()>
        Function GetEmpAll(ByVal lastDate As Date) As DataTable

        <OperationContract()>
        Function GetOrgAll(ByVal lastDate As Date) As DataTable

        <OperationContract()>
        Function GetTitleAll(ByVal lastDate As Date) As DataTable

        <OperationContract()>
        Function GetTitleGroupAll(ByVal lastDate As Date) As DataTable

        <OperationContract()>
        Function GetStaffRankAll(ByVal lastDate As Date) As DataTable

    End Interface

End Namespace
