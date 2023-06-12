Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class ProfileStoreProcedure
    Public Function Location_GetInfo(ByVal id As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.READ_LOCATION", New List(Of Object)(New Object() {id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Location_GetList() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.READ_LOCATION_LIST", New List(Of Object)(New Object() {Nothing}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Location_GetListCombobox() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.READ_LIST_LOCATION", New List(Of Object)(New Object() {Nothing}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách Location là công ty ký hợp đồng
    Public Function Location_GetListIsContract() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.READ_LIST_LOCATION_IS_CONTRACT", New List(Of Object)(New Object() {Nothing}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Location_Insert(ByVal locationVn As String,
                                    ByVal locationEn As String,
                                    ByVal province As String,
                                    ByVal isContract As Int32,
                                    ByVal address As String,
                                    ByVal workAddress As String,
                                    ByVal phone As String,
                                    ByVal fax As String,
                                    ByVal officePlace As String,
                                    ByVal website As String,
                                    ByVal accountNumber As String,
                                    ByVal bankId As Int32,
                                    ByVal taxCode As String,
                                    ByVal taxDate As String,
                                    ByVal taxPlace As String,
                                    ByVal lawAgentId As String,
                                    ByVal lawAgentTitle As String,
                                    ByVal signupAgentId As String,
                                    ByVal signupAgentTitle As String,
                                    ByVal chiefAccountantId As String,
                                    ByVal chiefAccountantTitle As String,
                                    ByVal authorizationNumber As String,
                                    ByVal businessNumber As String,
                                    ByVal businessName As String,
                                    ByVal note As String,
                                    ByVal actFlg As Int32,
                                    ByVal authorizationSigDate As String,
                                    ByVal businessRegDate As String,
                                    ByVal createdBy As String,
                                    ByVal createdLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.CREATE_LOCATION", New List(Of Object)(New Object() {locationVn,
                                                                                                                    locationEn,
                                                                                                                    province,
                                                                                                                    isContract,
                                                                                                                    address,
                                                                                                                    workAddress,
                                                                                                                    phone,
                                                                                                                    fax,
                                                                                                                    officePlace,
                                                                                                                    website,
                                                                                                                    accountNumber,
                                                                                                                    bankId,
                                                                                                                    taxCode,
                                                                                                                    taxDate,
                                                                                                                    taxPlace,
                                                                                                                    lawAgentId,
                                                                                                                    lawAgentTitle,
                                                                                                                    signupAgentId,
                                                                                                                    signupAgentTitle,
                                                                                                                    chiefAccountantId,
                                                                                                                    chiefAccountantTitle,
                                                                                                                    authorizationNumber,
                                                                                                                    businessNumber,
                                                                                                                    businessName,
                                                                                                                    note,
                                                                                                                    actFlg,
                                                                                                                    authorizationSigDate,
                                                                                                                    businessRegDate,
                                                                                                                    createdBy,
                                                                                                                    createdLog,
                                                                                                                    OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function Location_Update(ByVal id As Int32,
                                    ByVal locationVn As String,
                                    ByVal locationEn As String,
                                    ByVal province As String,
                                    ByVal isContract As Int32,
                                    ByVal address As String,
                                    ByVal workAddress As String,
                                    ByVal phone As String,
                                    ByVal fax As String,
                                    ByVal officePlace As String,
                                    ByVal website As String,
                                    ByVal accountNumber As String,
                                    ByVal bankId As Int32,
                                    ByVal taxCode As String,
                                    ByVal taxDate As String,
                                    ByVal taxPlace As String,
                                    ByVal lawAgentId As String,
                                    ByVal lawAgentTitle As String,
                                    ByVal signupAgentId As String,
                                    ByVal signupAgentTitle As String,
                                    ByVal chiefAccountantId As String,
                                    ByVal chiefAccountantTitle As String,
                                    ByVal authorizationNumber As String,
                                    ByVal businessNumber As String,
                                    ByVal businessName As String,
                                    ByVal note As String,
                                    ByVal actFlg As Int32,
                                    ByVal authorizationSigDate As String,
                                    ByVal businessRegDate As String,
                                    ByVal modifiedBy As String,
                                    ByVal modifiedLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.UPDATE_LOCATION", New List(Of Object)(New Object() {id,
                                                                                                                    locationVn,
                                                                                                                    locationEn,
                                                                                                                    province,
                                                                                                                    isContract,
                                                                                                                    address,
                                                                                                                    workAddress,
                                                                                                                    phone,
                                                                                                                    fax,
                                                                                                                    officePlace,
                                                                                                                    website,
                                                                                                                    accountNumber,
                                                                                                                    bankId,
                                                                                                                    taxCode,
                                                                                                                    taxDate,
                                                                                                                    taxPlace,
                                                                                                                    lawAgentId,
                                                                                                                    lawAgentTitle,
                                                                                                                    signupAgentId,
                                                                                                                    signupAgentTitle,
                                                                                                                    chiefAccountantId,
                                                                                                                    chiefAccountantTitle,
                                                                                                                    authorizationNumber,
                                                                                                                    businessNumber,
                                                                                                                    businessName,
                                                                                                                    note,
                                                                                                                    actFlg,
                                                                                                                    authorizationSigDate,
                                                                                                                    businessRegDate,
                                                                                                                    modifiedBy,
                                                                                                                    modifiedLog,
                                                                                                                    OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function Location_UpdateStatus(ByVal id As String, ByVal flag As Int32, ByVal modifiedBy As String, ByVal modifiedLog As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.UPDATE_LOCATION_STATUS", New List(Of Object)(New Object() {id, flag, modifiedBy, modifiedLog, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function Location_Delete(ByVal id As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.DELETE_LOCATION", New List(Of Object)(New Object() {id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function BankID_HasExist(ByVal bankID As String) As Int32
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.HAS_LOCATION_BANKID", New List(Of Object)(New Object() {bankID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
End Class