Imports System.IO
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities


Public Class CommonProgramsRepository
    Inherits CommonRepositoryBase

    Private rep As New HistaffFrameworkRepository

#Region "ThanhNT_TVCHCM Programs"

    'Ham kiem tra program + user co the chay duoc hay khong
    Public Function CheckRunProgram(ByVal programId As String, ByVal userName As String) As Decimal
        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_RUN_PROGRAM",
                                                   New List(Of Object)(New Object() {programId, userName, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)
            Return rs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'hàm lấy thông tin giá trị mặc định của tham số
    Public Function GetDefaultValue(ByVal defaultValueId As Decimal) As Object
        Try
            Dim ds = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_DEFAULT_VALUE", New List(Of Object)({defaultValueId}))

            Dim rs = ds.Tables(0).Rows(0)(0)  'lay gia tri mac dinh
            Return rs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub CreateNewFolder(ByVal url As String)
        If Not New DirectoryInfo(url).Exists Then
            'Thực hiện tạo folder theo mid
            Dim newFolder As DirectoryInfo
            newFolder = New DirectoryInfo(url)
            newFolder.Create()
        End If
    End Sub


    Public Sub GetTemplateInfo(ByVal programID As Decimal, ByRef fileUrl As String, ByRef fileOut_name As String, ByRef Mid As String)
        Dim pr As New List(Of List(Of Object))

        pr = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_CUR = OUT_CURSOR})
        Dim dsTemplate = rep.ExecuteStore("PKG_HCM_SYSTEM.PRR_GET_TEMPLATE", pr)

        fileUrl = If(dsTemplate.Tables(0).Rows(0).Item("TEMPLATE_URL").ToString = "", "", dsTemplate.Tables(0).Rows(0).Item("TEMPLATE_URL"))
        fileOut_name = If(dsTemplate.Tables(0).Rows(0).Item("FILE_OUT_NAME").ToString = "", "", dsTemplate.Tables(0).Rows(0).Item("FILE_OUT_NAME"))
        Mid = If(dsTemplate.Tables(0).Rows(0).Item("MID").ToString = "", "", dsTemplate.Tables(0).Rows(0).Item("MID"))
    End Sub

    'Get status description
    Public Function StatusString(ByVal stt As String) As String
        Try
            Dim objpara As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_STATUS_DESCRIPTION",
                                                   New List(Of Object)(New Object() {stt, OUT_STRING}))
            Dim rs = objpara(0).ToString
            Return rs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'get all data for load parameter
    Public Function GetAllParameters(ByVal programId As Decimal) As DataTable
        Dim ds = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_PARAMETER_IN_REQUEST", New List(Of Object)({programId}))
        If ds Is Nothing Then
            Return New DataTable
        End If
        If ds.Tables Is Nothing Then
            Return New DataTable
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all data for sql statement
    Public Function GetResultWithSQLStatement(ByVal selectSql As String, ByVal fromSql As String, ByVal whereSql As String) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_GET_RESULT_IN_QUERY", New List(Of Object)({selectSql, fromSql, whereSql}))
        If ds Is Nothing Then
            Return New DataTable
        End If
        If ds.Tables Is Nothing Then
            Return New DataTable
        Else
            Return ds.Tables(0)
        End If
    End Function




    'get all program type 
    Public Function FillComboboxProgramType() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_PROGRAM_TYPE", Nothing)
        If ds Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        End If
        If ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all program type 
    Public Function FillComboboxProgramName() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_PROGRAM_NAME", Nothing)
        If ds Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        End If
        If ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function


    ' get all template type in
    Public Function FillComboboxTemplateTypeIn() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_TEMPLATE_TYPE_IN", Nothing)
        If ds Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        End If
        If ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all template type out
    Public Function FillComboboxTemplateTypeOut() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_TEMPLATE_TYPE_OUT", Nothing)
        If ds Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        End If
        If ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all module in system
    Public Function FillComboboxModules() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_MODULES", Nothing)
        If ds Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        End If
        If ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all function group in system
    Public Function FillComboboxFunctionGroup() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_FUNCTION_GROUP", Nothing)
        If ds Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        End If
        If ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all function group in system
    Public Function FillComboboxValueSet() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_VALUE_SET", Nothing)
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all value set type in system
    Public Function FillComboboxValueSetType() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_VALUESET_TYPE", Nothing)
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(String))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function


    'get all value set type "None" in system
    Public Function FillComboboxValueSetTypeNone() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_VS_TYPE_NONE", Nothing)
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(String))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    'get all function group in system
    Public Function FillComboboxDataType() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_DATA_TYPE", Nothing)
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(String))
            dtb.Columns.Add("NAME", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    Public Function FillGridViewProgramParameters(ByVal programCode As String) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_PROGRAM_PARAMETERS", New List(Of Object)({programCode}))
        Return ds.Tables(0)
    End Function

    Public Function FillGridViewPrograms(ByVal programCode As String, ByVal programName As String, ByVal storeIn As String,
                                         ByVal storeOut As String, ByVal templateName As String, ByVal moduleName As Decimal, ByVal groupName As Decimal) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_PROGRAMS", New List(Of Object)({programCode, programName, storeIn, storeOut, templateName, moduleName, groupName}))
        Return ds.Tables(0)
    End Function

    Public Function FillGridViewValueSets(ByVal vsName As String, ByVal vsType As String) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_FLEX_VALUE_SET", New List(Of Object)({vsName, vsType}))
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("FLEX_VALUE_SET_ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE_SET_NAME", GetType(String))
            dtb.Columns.Add("DESCRIPTION", GetType(String))
            dtb.Columns.Add("TYPE_ID", GetType(String))
            dtb.Columns.Add("TYPE", GetType(String))
            dtb.Columns.Add("DATA_TYPE", GetType(Decimal))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function


    Public Function Insert_Requests(ByVal newRequest As REQUEST_DTO, ByVal program As DataSet, ByVal param As List(Of PARAMETER_DTO), ByVal isUpdateState As Decimal) As Decimal
        'isUpdateState = 0 : khong update trang thai cua request 
        'isUpdateState = 1:  mac dinh la update 
        'Insert into AD_REQUESTS_QUEUES
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CREATE_AD_REQUESTS_QUEUES",
                                                   New List(Of Object)(New Object() {newRequest.REQUEST_ID,
                                                                                    newRequest.PROGRAM_ID,
                                                                                    newRequest.PHASE_CODE,
                                                                                    newRequest.STATUS_CODE,
                                                                                    newRequest.START_DATE,
                                                                                    newRequest.END_DATE,
                                                                                    newRequest.ACTUAL_START_DATE,
                                                                                    newRequest.ACTUAL_COMPLETE_DATE,
                                                                                    100,
                                                                                    program.Tables(0).Rows(0)("NLS_LANGUAGE").ToString,
                                                                                    program.Tables(0).Rows(0)("NLS_TERRITORY").ToString,
                                                                                    program.Tables(0).Rows(0)("PRINTER").ToString,
                                                                                    program.Tables(0).Rows(0)("ORIENTATION").ToString,
                                                                                    program.Tables(0).Rows(0)("PERMISSION").ToString,
                                                                                    newRequest.CREATED_BY,
                                                                                    newRequest.CREATED_DATE,
                                                                                    newRequest.MODIFIED_BY,
                                                                                    newRequest.MODIFIED_DATE,
                                                                                    newRequest.CREATED_LOG,
                                                                                    newRequest.MODIFIED_LOG & param.Count.ToString,
                                                                                    program.Tables(0).Rows(0)("STORE_EXECUTE_IN").ToString,
                                                                                    program.Tables(0).Rows(0)("STORE_EXECUTE_OUT").ToString,
                                                                                    program.Tables(0).Rows(0)("PRIORITY"),
                                                                                    "Thông tin quá trình xử lý ",
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_NAME").ToString,
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_TYPE_IN").ToString,
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_TYPE_OUT").ToString,
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_URL").ToString,
                                                                                    OUT_NUMBER}))
        Dim requestID = Decimal.Parse(obj(0).ToString)
        'Insert into AD_REQUESTS
        Dim obj2 As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CREATE_AD_REQUESTS",
                                                   New List(Of Object)(New Object() {requestID,
                                                                                    newRequest.PROGRAM_ID,
                                                                                    newRequest.PHASE_CODE,
                                                                                    newRequest.STATUS_CODE,
                                                                                    newRequest.START_DATE,
                                                                                    newRequest.END_DATE,
                                                                                    newRequest.ACTUAL_START_DATE,
                                                                                    newRequest.ACTUAL_COMPLETE_DATE,
                                                                                    100,
                                                                                    program.Tables(0).Rows(0)("NLS_LANGUAGE").ToString,
                                                                                    program.Tables(0).Rows(0)("NLS_TERRITORY").ToString,
                                                                                    program.Tables(0).Rows(0)("PRINTER").ToString,
                                                                                    program.Tables(0).Rows(0)("ORIENTATION").ToString,
                                                                                    program.Tables(0).Rows(0)("PERMISSION").ToString,
                                                                                    newRequest.CREATED_BY,
                                                                                    newRequest.CREATED_DATE,
                                                                                    newRequest.MODIFIED_BY,
                                                                                    newRequest.MODIFIED_DATE,
                                                                                    newRequest.CREATED_LOG,
                                                                                    newRequest.MODIFIED_LOG & param.Count.ToString,
                                                                                    program.Tables(0).Rows(0)("STORE_EXECUTE_IN").ToString,
                                                                                    program.Tables(0).Rows(0)("STORE_EXECUTE_OUT").ToString,
                                                                                    program.Tables(0).Rows(0)("PRIORITY"),
                                                                                    "Thông tin quá trình xử lý ",
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_NAME").ToString,
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_TYPE_IN").ToString,
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_TYPE_OUT").ToString,
                                                                                    program.Tables(0).Rows(0)("TEMPLATE_URL").ToString,
                                                                                    OUT_NUMBER}))



        If param.Count > 0 Then
            'Insert parameters into table AD_REQUESTS_PARAMETERS
            For Each pr As PARAMETER_DTO In param
                Dim objpara As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CREATE_AD_REQUEST_PARAMETERS",
                                                   New List(Of Object)(New Object() {requestID,
                                                                                    pr.PARAMETER_NAME,
                                                                                    pr.VALUE,
                                                                                    pr.SEQUENCE,
                                                                                    -1,
                                                                                    pr.REPORT_FIELD,
                                                                                    isUpdateState,
                                                                                    OUT_NUMBER}))
                Dim check = Decimal.Parse(objpara(0).ToString)
                If check = 0 Then
                    Return -1
                Else
                    Continue For
                End If
            Next
        End If

        Return requestID
    End Function

    'Public Function Insert_AD_REQUEST_PARAMETER(ByVal newParameter As PARAMETERS_DTO) As Decimal
    '    Dim obj2 As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CREATE_AD_REQUEST_PARAMETERS",
    '                                               New List(Of Object)(New Object() {
    '                                                                                newParameter.REQUEST_ID,
    '                                                                                newParameter.PARAMTER_NAME,
    '                                                                                newParameter.VALUE,
    '                                                                                newParameter.SEQUENCE,
    '                                                                                newParameter.ID,
    '                                                                                newParameter.REPORT_FIELD,
    '                                                                                OUT_NUMBER}))

    '    Dim rs = Decimal.Parse(obj(0).ToString)
    '    Return rs
    'End Function

    Public Function Insert_Update_AD_Program_Parameters(ByVal parameter As PARAMETERS_DTO) As Decimal
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_DESCR_FLEX_COL",
                                                   New List(Of Object)(New Object() {parameter.FLEX_VALUE_SET_ID,
                                                                                    parameter.DESCRIPTIVE_FLEXFIELD_NAME,
                                                                                    parameter.APPLICATION_COLUMN_NAME,
                                                                                    parameter.LABEL_COLUMN_NAME,
                                                                                    parameter.SEQUENCE,
                                                                                    parameter.APPLICATION_ID,
                                                                                    parameter.DESCRIPTIVE_FLEX_COLUMN_ID,
                                                                                    parameter.DESCRIPTION,
                                                                                    parameter.TYPE_FIELD,
                                                                                    parameter.IS_REQUIRE,
                                                                                    OUT_NUMBER}))

        Dim rs = Decimal.Parse(obj(0).ToString)
        Return rs
    End Function

    Public Function Delete_AD_Desc_Flex_Col(ByVal parameterID As Decimal) As Decimal
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.DELETE_AD_DESC_FLEX_COL",
                                                   New List(Of Object)(New Object() {parameterID, OUT_NUMBER}))

        Dim rs = Decimal.Parse(obj(0).ToString)
        Return rs
    End Function

    Public Function Insert_Update_AD_Programs(ByVal program As AD_PROGRAM_DTO) As Decimal
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_PROGRAMS",
                                                   New List(Of Object)(New Object() {program.PROGRAM_ID, program.CODE, program.NAME, program.PROGRAM_TYPE, program.DESCRIPTION,
                                                                                    program.STORE_EXECUTE_IN, program.STORE_EXECUTE_OUT, program.TEMPLATE_NAME, program.TEMPLATE_TYPE_IN, program.TEMPLATE_TYPE_OUT,
                                                                                    program.TEMPLATE_URL, program.PRIORITY, program.ORDERBY, program.ORIENTATION, program.STATUS, program.PERMISSION, program.CREATED_BY,
                                                                                    program.MODIFIED_BY, program.CREATED_LOG, program.MODIFIED_LOG, program.OUTPUTFILE_SIZE, program.NLS_LANGUAGE, program.NLS_TERRITORY,
                                                                                    program.PRINTER, program.FILE_OUT_NAME, program.MODULE_ID, program.GROUP_ID, program.PROGRAM_TYPE_ID, program.SQLLOADER_FILE, program.LINE_FROM, OUT_NUMBER}))

        Dim rs = Decimal.Parse(obj(0).ToString)
        Return rs
    End Function

    Public Function Delete_AD_Programs(ByVal programID As Decimal) As Decimal
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.DELETE_AD_PROGRAMS",
                                                   New List(Of Object)(New Object() {programID, OUT_NUMBER}))

        Dim rs = Decimal.Parse(obj(0).ToString)
        Return rs
    End Function

    Public Function GetResultWithSQLStatementDT(ByVal selectSql As String, ByVal fromSql As String, ByVal whereSql As String) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_RESULT_IN_QUERY_DT", New List(Of Object)({selectSql, fromSql, whereSql}))
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Return New DataTable
        Else
            Return ds.Tables(0)
        End If
    End Function

    Public Function GetGroupOrUserList(ByVal P_Type As Decimal, ByVal P_ID As Decimal) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_FUNCTION.GET_GROUP_OR_USER_LIST", New List(Of Object)({P_Type, P_ID}))
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Return New DataTable
        Else
            Return ds.Tables(0)
        End If
    End Function

    Public Function CopyUserFunction(ByVal User_ID_To As Decimal, ByVal User_ID_From As Decimal) As Boolean
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_FUNCTION.COPY_USER_FUNCTION", New List(Of Object)({User_ID_From, User_ID_To, Me.Log.Username, OUT_NUMBER}))

        If obj(0).ToString = "0" Then
            Return False
        Else
            Return True
        End If

    End Function
#End Region

    Public Function FillComboboxModules_Mid() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_ALL_MODULES_MID", _
                                                   New List(Of Object)(New Object() {}))
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("NAME", GetType(String))
            dtb.Columns.Add("CODE", GetType(String))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function
End Class
