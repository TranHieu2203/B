Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Recruitment.RecruitmentBusiness

Public Class RecruitmentStoreProcedure
    Private rep As New HistaffFrameworkRepository

#Region "Đợt tuyển dụng - (RC_STAGE)"

    'Cập nhật chi phí dự kiến
    Public Function STAGE_UpdateCostestimate(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.STAGE_UPDATE_COSTESTIMATE", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Lấy thông tin đợt tuyển dụng theo ID
    Public Function STAGE_GetByID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.STAGE_GET_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách đợt tuyển dụng theo: đơn vị & year
    Public Function STAGE_GetList(ByVal P_ORG_ID As Int32, ByVal P_YEAR As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.STAGE_GETLIST_BYFILTER", New List(Of Object)(New Object() {P_ORG_ID, P_YEAR}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Hiển thị ds đợt tuyển dụng trên combobox theo đơn vị
    Public Function STAGE_GetData_Combobox(ByVal P_ORG_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.STAGE_GETDATA_COMBOBOX", New List(Of Object)(New Object() {P_ORG_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới đợt tuyển dụng
    Public Function STAGE_AddNew(ByVal ORG_ID As Int32,
                                 ByVal YEAR As Int32,
                                 ByVal ORGANIZATIONNAME As String,
                                 ByVal TITLE As String,
                                 ByVal STARTDATE As Date,
                                 ByVal ENDDATE As Date,
                                 ByVal SOURCEOFREC_ID As String,
                                 ByVal REMARK As String,
                                 ByVal CREATE_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_STAGE", _
                                                   New List(Of Object)(New Object() {
                                                    ORG_ID, YEAR, ORGANIZATIONNAME, TITLE,
                                                    STARTDATE, ENDDATE, SOURCEOFREC_ID, REMARK,
                                                    CREATE_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật đợt tuyển dụng
    Public Function STAGE_Update(ByVal ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal YEAR As Int32,
                                 ByVal ORGANIZATIONNAME As String,
                                 ByVal TITLE As String,
                                 ByVal STARTDATE As Date,
                                 ByVal ENDDATE As Date,
                                 ByVal SOURCEOFREC_ID As String,
                                 ByVal REMARK As String,
                                 ByVal MODIFY_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_STAGE", _
                                                     New List(Of Object)(New Object() {
                                                     ID, ORG_ID, YEAR, ORGANIZATIONNAME, TITLE,
                                                     STARTDATE, ENDDATE, SOURCEOFREC_ID, REMARK,
                                                     MODIFY_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa đợt tuyển dụng
    Public Function STAGE_Delete(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_STAGE", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Manning - QL định biên"
    Public Function CheckExistRequest(ByVal objRequest As RequestDTO) As Integer
        Try
            Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.CHECK_EXIST_REQUEST", New List(Of Object)(New Object() {objRequest.ORG_ID, objRequest.TITLE_ID, objRequest.SEND_DATE, objRequest.EXPECTED_JOIN_DATE, OUT_NUMBER}))
            Return Integer.Parse(obj(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckExistRequestPortal(ByVal objRequest As RequestPortalDTO) As Integer
        Try
            Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.CHECK_EXIST_REQUEST", New List(Of Object)(New Object() {objRequest.ORG_ID, objRequest.TITLE_ID, objRequest.SEND_DATE, objRequest.EXPECTED_JOIN_DATE, OUT_NUMBER}))
            Return Integer.Parse(obj(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRecruitmentImport(ByVal user As String, ByVal _param As ParamDTO) As DataSet
        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.GET_RECRUITMENT_IMPORT", New List(Of Object)(New Object() {user, _param.ORG_ID, _param.IS_DISSOLVE, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCountEmpWorking(ByRef title_id As Decimal) As Integer
        Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GETCOUNT_EMPWORKING", New List(Of Object)(New Object() {title_id, OUT_NUMBER}))
        Return Integer.Parse(obj(0).ToString)
    End Function

    Public Function GetManningOrgId() As Integer
        Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_MANNING_ORG_ID", New List(Of Object)(New Object() {OUT_NUMBER}))
        Return Integer.Parse(obj(0).ToString)
    End Function

    Public Function GetOldManning(ByRef effectdate As Date) As Integer
        Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_OLD_MANNING", New List(Of Object)(New Object() {effectdate, OUT_NUMBER}))
        Return Integer.Parse(obj(0).ToString)
    End Function

    'Public Function GetListManning(ByVal obj As ManningOrgDTO) As DataTable
    '    Dim listManning As DataTable
    '    Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GETLIST_MANNING", New List(Of Object)(New Object() {obj.ORG_ID, OUT_CURSOR}))
    '    If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
    '        listManning = ds.Tables(0)
    '    End If
    '    Return listManning
    'End Function
    Public Function IMPORT_DINH_BIEN(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.IMPORT_DINH_BIEN", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        Return True
    End Function
    Public Function GetListManningByName(ByVal MANNING_ORG_ID As Integer,
                                      ByVal ORG_ID As Integer,
                                      ByVal YEAR As Integer,
                                      ByVal isExport As Integer,
                                      ByVal param As ManningOrgDTO,
                                     Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue) As DataTable
        Dim listManning As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GETLIST_MANNING_BY_NAME", New List(Of Object)(New Object() {MANNING_ORG_ID, ORG_ID, YEAR, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            If isExport = 1 Then
                Dim dt As DataTable
                dt = ds.Tables(0)
                If dt.Rows.Count = 0 Then
                    listManning = New DataTable
                    Return listManning
                End If
                'sap xep theo phong ban
                dt.DefaultView.Sort = "ORG_NAME ASC"
                listManning = dt.DefaultView.ToTable()
                Return listManning
            End If
            listManning = ds.Tables(0)
            If listManning.Rows.Count = 0 Then
                listManning = New DataTable
                Return listManning
            End If
            If param.CURRENT_MANNING IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("CURRENT_MANNING").ToString.ToUpper.Contains(param.CURRENT_MANNING.ToString.ToUpper)).CopyToDataTable
            End If
            If param.NEW_MANNING IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("NEW_MANNING").ToString.ToUpper.Contains(param.NEW_MANNING.ToString.ToUpper)).CopyToDataTable
            End If
            If param.NOTE IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("NOTE").ToString.ToUpper.Contains(param.NOTE.ToString.ToUpper)).CopyToDataTable
            End If
            If param.OLD_MANNING IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("OLD_MANNING").ToString.ToUpper.Contains(param.OLD_MANNING.ToString.ToUpper)).CopyToDataTable
            End If
            If param.MOBILIZE_COUNT_MANNING IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("MOBILIZE_COUNT_MANNING").ToString.ToUpper.Contains(param.MOBILIZE_COUNT_MANNING.ToString.ToUpper)).CopyToDataTable
            End If
            If param.ORG_NAME IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("ORG_NAME").ToString.ToUpper.Contains(param.ORG_NAME.ToString.ToUpper)).CopyToDataTable
            End If
            If param.TITLE_NAME IsNot Nothing Then
                listManning = (From p In listManning.AsEnumerable Where p("TITLE_NAME").ToString.ToUpper.Contains(param.TITLE_NAME.ToString.ToUpper)).CopyToDataTable
            End If
            Total = listManning.Rows.Count
            listManning = If(listManning.Rows.Count > 0, listManning.AsEnumerable.Skip(PageIndex * PageSize).Take(PageSize).CopyToDataTable, Nothing)
        End If
        Return listManning
    End Function

    Public Function GetManningOrgByID(ByVal MANNING_ORG_ID As Integer) As DataTable
        Dim listManning As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.MANNING_ORG_GET_BY_ID", New List(Of Object)(New Object() {MANNING_ORG_ID, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            listManning = ds.Tables(0)
        End If
        Return listManning
    End Function

    Public Function AddNewManningOrg(ByVal obj As ManningOrgDTO) As Int32
        Try
            'Dim rep As New DataAccess.QueryData
            Dim objAdd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_MANNING_ORG", _
                                                    New List(Of Object)(New Object() {obj.ORG_ID, obj.MANNING_NAME, obj.EFFECT_DATE, obj.NOTE, obj.OLD_MANNING, obj.CURRENT_MANNING,
                                                                                      obj.NEW_MANNING, obj.MOBILIZE_COUNT_MANNING, obj.STATUS, obj.YEAR, OUT_NUMBER}))

            Return Int32.Parse(objAdd(0).ToString())
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function AddNewManningTitle(ByVal P_ORG_ID As Int32, ByVal P_MANNING_ORG_ID As Int32) As Int32
        Try
            'Dim rep As New DataAccess.QueryData
            Dim objAdd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_MANNING_TITLE", _
                                                    New List(Of Object)(New Object() {P_ORG_ID, P_MANNING_ORG_ID, OUT_NUMBER}))

            Return Int32.Parse(objAdd(0).ToString())
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function UpdateManningOrg(ByVal obj As ManningOrgDTO) As Int32
        Dim objUpd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_MANNING_ORG", _
                                                   New List(Of Object)(New Object() {obj.ID, obj.MANNING_NAME, obj.EFFECT_DATE, obj.NOTE, obj.STATUS, OUT_NUMBER}))
        Return Int32.Parse(objUpd(0).ToString())
    End Function

    Public Function UpdateNewManningOrg(ByVal P_MANNING_ORG_ID As Int32) As Int32
        Dim objUpd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_SUMMARY_MANNING_ORG",
                                                   New List(Of Object)(New Object() {P_MANNING_ORG_ID, OUT_NUMBER}))
        Return Int32.Parse(objUpd(0).ToString())
    End Function

    Public Function UpdateNewManningTitle1(ByVal P_MANNING_ORG_ID As Int32) As Int32
        Dim objUpd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_SUMMARY_MANNING_TITLE",
                                                   New List(Of Object)(New Object() {P_MANNING_ORG_ID, OUT_NUMBER}))
        Return Int32.Parse(objUpd(0).ToString())
    End Function

    Public Function UpdateNewManningTitle(ByVal obj As ManningTitleDTO) As Int32
        Dim objUpd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_NEW_MANNING_TITLE", _
                                                   New List(Of Object)(New Object() {obj.ID, obj.NEW_MANNING, obj.NOTE, obj.MOBILIZE_COUNT_MANNING, OUT_NUMBER}))
        Return Int32.Parse(objUpd(0).ToString())
    End Function
    Public Function DELETE_RECORD_IMPORT(ByVal P_ID As String) As Boolean
        Dim objUpd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_RECORD_IMPORT", _
                                                   New List(Of Object)(New Object() {P_ID, OUT_NUMBER}))
        Return True
    End Function
    Public Function DeleteManning(ByVal P_ID As String, ByVal P_MANNING_ORG_ID As Integer) As Boolean
        Dim objUpd As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_MANNING", _
                                                   New List(Of Object)(New Object() {P_ID, P_MANNING_ORG_ID, OUT_NUMBER}))
        Return True
    End Function

    Public Function LoadInfoManningByOrg(ByVal listOrgId As Integer,
                               ByVal title As Integer) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.LOAD_INFO_MANNING_EMPBYORG", New List(Of Object)(New Object() {listOrgId, title}))
        If ds Is Nothing Or ds.Tables Is Nothing Then
            Dim dtb As New DataTable
            dtb.Columns.Add("ID", GetType(Integer))
            dtb.Columns.Add("ORG_NAME", GetType(String))
            dtb.Columns.Add("TITLE_NAME", GetType(String))
            dtb.Columns.Add("NAME", GetType(String))
            dtb.Columns.Add("EFFECT_DATE", GetType(Date))
            dtb.Columns.Add("NOTE", GetType(String))
            dtb.Columns.Add("STATUS", GetType(Integer))
            Return dtb
        Else
            Return ds.Tables(0)
        End If
    End Function

    Public Function LoadComboboxYear(ByVal P_ORG_ID As Integer) As DataTable
        Dim listYear As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.LOAD_COMBOBOX_YEAR", New List(Of Object)(New Object() {P_ORG_ID, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            listYear = ds.Tables(0)
        End If
        Return listYear
    End Function

    Public Function LoadComboboxYear_PlanReg(ByVal P_ORG_ID As Integer) As DataTable
        Dim listYear As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.LOAD_YEAR_PLANREG", New List(Of Object)(New Object() {P_ORG_ID, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            listYear = ds.Tables(0)
        End If
        Return listYear
    End Function

    Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
        Dim listMannName As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.LOAD_COMBOBOX_LISTMANNINGNAME", New List(Of Object)(New Object() {org_id, year, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            listMannName = ds.Tables(0)
        End If
        Return listMannName
    End Function

    Public Function GetCurrentManningTitle(ByVal P_ORG_ID As Integer, ByVal P_TITLE_ID As Integer) As DataTable
        Dim listYear As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_CURRENT_MANNING_TITLE", New List(Of Object)(New Object() {P_ORG_ID, P_TITLE_ID, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            listYear = ds.Tables(0)
        End If
        Return listYear
    End Function
    Public Function GetCurrentManningTitle1(ByVal P_ORG_ID As Integer, ByVal P_TITLE_ID As Integer, ByVal p_date As Date) As DataTable
        Dim listYear As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_CURRENT_MANNING_TITLE1", New List(Of Object)(New Object() {P_ORG_ID, P_TITLE_ID, p_date, OUT_CURSOR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            listYear = ds.Tables(0)
        End If
        Return listYear
    End Function

#End Region

#Region "Phân bổ chi phí tuyển dụng - (RC_COSTALLOCATE)"

    'Lấy danh sách phân bổ chi phí theo đợt tuyển dụng
    Public Function COSTALLOCATE_GetByStage(ByVal P_RC_STAGE_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.COSTALLOCATE_GET_BY_STAGE", New List(Of Object)(New Object() {P_RC_STAGE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới thông tin phân bổ chi phí
    Public Function COSTALLOCATE_AddNew(ByVal RC_STAGE_ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal COSTAMOUNT As Decimal,
                                 ByVal REMARK As String,
                                 ByVal CREATE_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_COSTALLOCATE", _
                                                   New List(Of Object)(New Object() {
                                                    RC_STAGE_ID, ORG_ID, COSTAMOUNT, REMARK,
                                                    CREATE_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật thông tin phân bổ chi phí
    Public Function COSTALLOCATE_Update(ByVal ID As Int32,
                                 ByVal RC_STAGE_ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal COSTAMOUNT As Decimal,
                                 ByVal REMARK As String,
                                 ByVal MODIFY_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_COSTALLOCATE", _
                                                     New List(Of Object)(New Object() {
                                                     ID, RC_STAGE_ID, ORG_ID, COSTAMOUNT, REMARK,
                                                     MODIFY_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa thông tin phân bổ chi phí
    Public Function COSTALLOCATE_Delete(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_COSTALLOCATE", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Quản lý chi phí tuyển dụng - (RC_COST)"

    'Cập nhật chi phí dự kiến
    Public Function COST_UpdateCostReality(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.COST_UPDATE_COSTREALITY", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Lấy thông tin chi phí tuyển dụng theo ID
    Public Function COST_GetByID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.COST_GET_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách chi phí tuyển dụng theo đơn vị
    Public Function COST_GetList(ByVal P_ORG_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.COST_GETLIST_BYFILTER", New List(Of Object)(New Object() {P_ORG_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới chi phí tuyển dụng
    Public Function COST_AddNew(ByVal RC_STAGE_ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal SOURCEOFREC_ID As String,
                                 ByVal COSTESTIMATE As Decimal,
                                 ByVal COSTREALITY As Decimal,
                                 ByVal REMARK As String,
                                 ByVal CREATE_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_COST", _
                                                   New List(Of Object)(New Object() {
                                                    RC_STAGE_ID, ORG_ID, SOURCEOFREC_ID,
                                                    COSTESTIMATE, COSTREALITY, REMARK,
                                                    CREATE_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật chi phí tuyển dụng
    Public Function COST_Update(ByVal ID As Int32,
                                 ByVal RC_STAGE_ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal SOURCEOFREC_ID As String,
                                 ByVal COSTESTIMATE As Decimal,
                                 ByVal COSTREALITY As Decimal,
                                 ByVal REMARK As String,
                                 ByVal MODIFY_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_COST", _
                                                     New List(Of Object)(New Object() {
                                                     ID, RC_STAGE_ID, ORG_ID, SOURCEOFREC_ID,
                                                     COSTESTIMATE, COSTREALITY, REMARK,
                                                     MODIFY_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa chi phí tuyển dụng
    Public Function COST_Delete(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_COST", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Phân bổ chi phí tuyển dụng - (RC_COST_COSTALLOCATE)"

    'Lấy danh sách phân bổ chi phí theo COST
    Public Function COST_COSTALLOCATE_GetByCOST(ByVal P_RC_COST_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.COST_COSTALLOCATE_GET_BY_COST", New List(Of Object)(New Object() {P_RC_COST_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới thông tin phân bổ chi phí
    Public Function COST_COSTALLOCATE_AddNew(ByVal RC_COST_ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal COSTAMOUNT As Int32,
                                 ByVal REMARK As String,
                                 ByVal CREATE_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_COST_COSTALLOCATE", _
                                                   New List(Of Object)(New Object() {
                                                    RC_COST_ID, ORG_ID, COSTAMOUNT, REMARK,
                                                    CREATE_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật thông tin phân bổ chi phí
    Public Function COST_COSTALLOCATE_Update(ByVal ID As Int32,
                                 ByVal RC_COST_ID As Int32,
                                 ByVal ORG_ID As Int32,
                                 ByVal COSTAMOUNT As Int32,
                                 ByVal REMARK As String,
                                 ByVal MODIFY_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_COST_COSTALLOCATE", _
                                                     New List(Of Object)(New Object() {
                                                     ID, RC_COST_ID, ORG_ID, COSTAMOUNT, REMARK,
                                                     MODIFY_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa thông tin phân bổ chi phí
    Public Function COST_COSTALLOCATE_Delete(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_COST_COSTALLOCATE", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Cập nhật kết quả thi tuyển"

    'Lấy danh sách ứng viên theo chương trình tuyển dụng
    Public Function CANDIDATE_LIST_GETBYPROGRAM(ByVal P_RC_PROGRAM_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.CANDIDATE_LIST_GETBYPROGRAM", New List(Of Object)(New Object() {P_RC_PROGRAM_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function CANDIDATE_LIST_GETBYPROGRAM_BY_EXAMS(ByVal P_RC_PROGRAM_ID As Int32, ByVal P_EXAMS_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.CANDIDATE_LIST_GETBYPROGRAM_BY_EXAMS", New List(Of Object)(New Object() {P_RC_PROGRAM_ID, P_EXAMS_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    'Lấy danh sách môn thi/phỏng ván theo thông tin ứng viên
    Public Function EXAMS_GETBYCANDIDATE(ByVal P_RC_PROGRAM_ID As Int32, ByVal P_RC_CANDIDATE_ID As Int32, ByVal P_IS_PV As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.EXAMS_GETBYCANDIDATE", New List(Of Object)(New Object() {P_RC_PROGRAM_ID, P_RC_CANDIDATE_ID, P_IS_PV}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function EXAMS_GETBYCANDIDATE_BY_PCS_ID(ByVal P_PSC_ID As Int32, ByVal P_IS_PV As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.EXAMS_GETBYCANDIDATE_BY_PCS_ID", New List(Of Object)(New Object() {P_PSC_ID, P_IS_PV}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy điểm trung bình của điểm thi (ko áp dụng cho Phỏng vấn)
    Public Function CANDIDATE_GET_AVERAGE_MARKS(ByVal P_RC_PROGRAM_ID As Int32, ByVal P_RC_CANDIDATE_ID As Int32) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT.CANDIDATE_GET_AVERAGE_MARKS", New List(Of Object)(New Object() {P_RC_PROGRAM_ID, P_RC_CANDIDATE_ID, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    'Cập nhật chi phí tuyển dụng
    Public Function UPDATE_CANDIDATE_RESULT(ByVal ID As Int32,
                                 ByVal POINT_RESULT As Decimal,
                                 ByVal COMMENT_INFO As String,
                                 ByVal ASSESSMENT_INFO As String,
                                 ByVal IS_PASS As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_CANDIDATE_RESULT", _
                                                     New List(Of Object)(New Object() {
                                                     ID, POINT_RESULT, COMMENT_INFO, ASSESSMENT_INFO, IS_PASS, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa thông tin kết quả thi tuyển / phỏng vấn
    Public Function DELETE_CANDIDATE_RESULT(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_CANDIDATE_RESULT", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function DELETE_RC_PROGRAM_SCHEDULE(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_RC_PROGRAM_SCHEDULE", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Ứng viên - (RC_CANDIDATE)"
    'Cập nhật tình trạng ứng viên
    Public Function UPDATE_CANDIDATE_STATUS(ByVal ID As Int32,
                                 ByVal STATUS_CODE As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_CANDIDATE_STATUS",
                                                     New List(Of Object)(New Object() {
                                                     ID, STATUS_CODE, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function UPDATE_PROGRAM_CANDIDATE_STATUS(ByVal ID As Int32,
                                 ByVal STATUS_CODE As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_PROGRAM_CANDIDATE_STATUS",
                                                     New List(Of Object)(New Object() {
                                                     ID, STATUS_CODE, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function UPDATE_PROGRAM_CANDIDATE_STATUS_BY_EXAMS_ORDER(ByVal ID As Int32,
                                 ByVal STATUS_CODE As String, P_EXAMS_ORDER As Int32, P_PRO_ID As Int32, P_SCHEDULE As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_PROGRAM_CANDIDATE_STATUS_BY_EXAMS_ORDER",
                                                     New List(Of Object)(New Object() {
                                                     ID, STATUS_CODE, P_EXAMS_ORDER, P_PRO_ID, P_SCHEDULE, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Lịch thi tuyển - (Program_Schedule)"
    'Lấy danh sách lịch thi tuyển theo chương trình tuyển dụng
    Public Function GET_PROGRAM_SCHCEDULE_LIST(ByVal P_PROGRAM_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_PROGRAM_SCHCEDULE_LIST", New List(Of Object)(New Object() {P_PROGRAM_ID}))
        If ds.Tables.Count > 0 Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    'Lấy danh sách ứng viên đã có lịch thi tuyển
    Public Function GET_PROGRAM_SCHCEDULE_LIST(ByVal P_PROGRAM_ID As Int32, ByVal P_PROGRAM_SCHEDULE_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_SCHCEDULE_CAN_BY_STATUS", New List(Of Object)(New Object() {P_PROGRAM_ID, P_PROGRAM_SCHEDULE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_SCHCEDULE_CAN_BY_SCHCEDULEID(ByVal P_PROGRAM_ID As Int32, ByVal P_PROGRAM_SCHEDULE_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_SCHCEDULE_CAN_BY_SCHCEDULEID", New List(Of Object)(New Object() {P_PROGRAM_ID, P_PROGRAM_SCHEDULE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách ứng viên chưa có lịch thi tuyển
    Public Function GET_CANDIDATE_NOT_SCHEDULE(ByVal P_PROGRAM_ID As Int32, ByVal P_PROGRAM_SCHEDULE_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_CANDIDATE_NOT_SCHEDULE", New List(Of Object)(New Object() {P_PROGRAM_ID, P_PROGRAM_SCHEDULE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_CANDIDATE_NOT_SCHEDULE_1(ByVal P_PROGRAM_ID As Int32, ByVal P_PROGRAM_SCHEDULE_ID As Int32, ByVal P_EXAMS_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_CANDIDATE_NOT_SCHEDULE_1", New List(Of Object)(New Object() {P_PROGRAM_ID, P_PROGRAM_SCHEDULE_ID, P_EXAMS_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    'lấy email ds ứng viên
    Public Function Get_Email_Candidate(ByVal ID As Decimal) As String
        Dim obj As New List(Of Object)
        Try
            obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.Get_Email_Candidate", New List(Of Object)(New Object() {ID, OUT_STRING}))
            If obj IsNot Nothing Then
                Return obj(0)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'lấy email ds ứng viên
    Public Function Get_Email_Employee(ByVal ID As Decimal) As String
        Dim obj As New List(Of Object)
        Try
            obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.Get_Email_Employee", New List(Of Object)(New Object() {ID, OUT_STRING}))
            If obj IsNot Nothing Then
                Return obj(0)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Thêm mới lịch thi tuyển cho ứng viên
    Public Function ADDNEW_CAN_PRO_SCHEDULE(ByVal CANDIDATE_ID As Int32,
                                 ByVal PROGRAM_SCHEDULE_ID As Int32,
                                 ByVal PROGRAM_EXAMS_ID As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_CAN_PRO_SCHEDULE", _
                                                   New List(Of Object)(New Object() {
                                                    CANDIDATE_ID, PROGRAM_SCHEDULE_ID, PROGRAM_EXAMS_ID, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function


    'Thêm mới thông tin phân bổ chi phí
    Public Function ADDNEW_PRO_SCHEDULE(ByVal RC_PROGRAM_ID As Int32,
                                       ByVal EMPLOYEE_ID As Decimal?,
                                 ByVal SCHEDULE_DATE As DateTime,
                                 ByVal EXAMS_PLACE As String,
                                 ByVal NOTE As String,
                                 ByVal TIME As String,
                                 ByVal ID_EXAM As String,
                                 ByVal CREATE_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.ADDNEW_PRO_SCHEDULE",
                                                   New List(Of Object)(New Object() {
                                                    RC_PROGRAM_ID, EMPLOYEE_ID, SCHEDULE_DATE, EXAMS_PLACE, NOTE, TIME, ID_EXAM,
                                                    CREATE_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật thông tin phân bổ chi phí
    Public Function UPDATE_PRO_SCHEDULE(ByVal ID As Int32,
                                        ByVal EMPLOYEE_ID As Decimal?,
                                 ByVal SCHEDULE_DATE As DateTime,
                                 ByVal EXAMS_PLACE As String,
                                 ByVal NOTE As String,
                                 ByVal TIME As String,
                                 ByVal ID_EXAM As String,
                                 ByVal MODIFY_BY As String,
                                 ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_PRO_SCHEDULE",
                                                     New List(Of Object)(New Object() {
                                                     ID, EMPLOYEE_ID, SCHEDULE_DATE, EXAMS_PLACE, NOTE, TIME, ID_EXAM,
                                                     MODIFY_BY, LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Lấy Lịch thi tuyển mới nhất
    Public Function GET_TOPID_PRO_SCHEDULE(ByVal P_RC_PROGRAM_ID As Int32) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_TOPID_PRO_SCHEDULE", New List(Of Object)(New Object() {P_RC_PROGRAM_ID, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    'Cập nhật PRO_Schedule id
    Public Function UPDATE_PRO_SCHEDULE_ID(ByVal P_PROGRAM_SCHEDULE_ID As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_PRO_SCHEDULE_ID", _
                                                     New List(Of Object)(New Object() {
                                                     P_PROGRAM_SCHEDULE_ID
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
    'lay template thuoc quan ly bieu mau
    Public Function GET_MAIL_TEMPLATE(ByVal code As String, ByVal group As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.GET_TEMPLATE_MAIL", New List(Of Object)(New Object() {code, group}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_INFO_CADIDATE(ByVal ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_INFO_CADIDATE", New List(Of Object)(New Object() {ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_EMAIL_COMPANY(ByVal empid As Decimal) As String
        Dim obj As New List(Of Object)
        Try
            obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_MAIL_COMPANY", New List(Of Object)(New Object() {empid, OUT_STRING}))
            If obj IsNot Nothing Then
                Return obj(0).ToString
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Xóa chi phí tuyển dụng
    Public Function DELETE_PRO_SCHEDULE_CAN_ISNULL() As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_PRO_SCHEDULE_CAN_ISNULL", New List(Of Object)(New Object() {OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa thông tin phân bổ chi phí
    Public Function DELETE_PRO_SCHEDULE_CAN(ByVal PRO_SCHEDULE_ID As Int32, ByVal CANDIDATE_ID As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_PRO_SCHEDULE_CAN", New List(Of Object)(New Object() {PRO_SCHEDULE_ID, CANDIDATE_ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Môn thi"
    'Lấy danh sách môn thi theo chương trình tuyển dụng
    Public Function GET_AllExams_ByProgram(ByVal P_PROGRAM_ID As Int32, ByVal P_SCHEDULE_ID As Decimal?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_ALL_EXAMS_BYPRO", New List(Of Object)(New Object() {P_PROGRAM_ID, P_SCHEDULE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Kiểm tra môn thi có tồn tại trong lịch thi
    'Public Function Check_Exams_IsExist(ByVal P_PROGRAM_EXAMS_ID As Int32) As Decimal
    '    Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT.CHECK_EXAMS_PRO_SCHEDULE_CAN", New List(Of Object)(New Object() {P_PROGRAM_EXAMS_ID, OUT_NUMBER}))
    '    Return objects(0).ToString
    'End Function
    Public Function Check_Exams_IsExist(ByVal P_PROGRAM_SCHEDULE_ID As Int32, ByVal P_CANDIDATE_ID As Int32) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT.CHECK_EXAMS_PRO_SCHEDULE_CAN", New List(Of Object)(New Object() {P_PROGRAM_SCHEDULE_ID, P_CANDIDATE_ID, OUT_NUMBER}))
        Return objects(0).ToString
    End Function


    'get by id
    Public Function GET_PRO_SCHEDULE_BYID(ByVal P_PROGRAM_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_PRO_SCHEDULE_BYID", New List(Of Object)(New Object() {P_PROGRAM_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    'get by Mail
    Public Function SCHEDULE_SENDMAIL_NPV(ByVal P_PROGRAM_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.SCHEDULE_SENDMAIL_NPV", New List(Of Object)(New Object() {P_PROGRAM_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_PRO_SCHEDULE_MAIL(ByVal P_PROGRAM_SCHEDULE_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_PRO_SCHEDULE_MAIL", New List(Of Object)(New Object() {P_PROGRAM_SCHEDULE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_PRO_SCHEDULE_MAIL_NV(ByVal P_PROGRAM_SCHEDULE_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_PRO_SCHEDULE_MAIL_NV", New List(Of Object)(New Object() {P_PROGRAM_SCHEDULE_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    'Lấy danh sách lịch thi tuyển theo chương trình tuyển dụng
    Public Function GET_PROGRAM_SCHCEDULE_LIST_ALL(ByVal P_ID_SCHEDULE As String, ByVal P_PROGRAM_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_PROGRAM_SCHCEDULE_LIST_ALL", New List(Of Object)(New Object() {P_ID_SCHEDULE, P_PROGRAM_ID}))
        If ds.Tables.Count > 0 Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
#End Region
#Region "List"
    Public Function GET_TITLE_COMBOBOX() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_TITLE_COMBOBOX")
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
#End Region
#Region "Request"
    'Lấy danh sách các chức danh có / không có trong kế hoạch tuyển dụng
    Public Function GET_TITLE_IN_PLAN(ByVal P_ORG_ID As Int32, ByVal IS_PLAN As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_TITLE_IN_PLAN", New List(Of Object)(New Object() {P_ORG_ID, IS_PLAN, Common.Common.SystemLanguage.Name}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    ' Kiểm tra danh sách các yêu cầu tuyển dụng có thông tin nào chưa phê duyệt
    Public Function CHECK_REQUEST_NOT_APPROVE(ByVal P_ID As String) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT.CHECK_REQUEST_NOT_APPROVE", New List(Of Object)(New Object() {P_ID, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    'load yêu cầu tuyển dụng theo ID
    Public Function REQUEST_GET_BY_ID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.REQUEST_GET_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'TU DONG TAO CODE RC
    Public Function AUTO_GEN_CODE_RC(ByVal P_ID As Int32, ByVal P_DATE_REQUEST As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.AUTO_GEN_CODE_RC", New List(Of Object)(New Object() {P_ID, P_DATE_REQUEST}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_NUM_PLANNING_DETAIL_BY_MONTH(ByVal P_ORG_ID As Integer, ByVal P_TITLE_ID As Integer, ByVal P_DATE_REQUEST As Date) As DataTable
        Try
            Dim listYear As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_NUM_PLANNING_DETAIL_BY_MONTH", New List(Of Object)(New Object() {P_ORG_ID, P_TITLE_ID, P_DATE_REQUEST}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                listYear = ds.Tables(0)
            End If
            Return listYear
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_RC_HR_PLANING_DETAIL_RANK_SAL(ByVal P_OGR_ID As Integer, ByVal P_TITLE_ID As Integer, ByVal P_SEND_DATE As Date) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.GET_RC_HR_PLANING_DETAIL_RANK_SAL", New List(Of Object)(New Object() {P_OGR_ID, P_TITLE_ID, P_SEND_DATE, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    Public Function GET_PLAN_DETAIL_BY_ORG(ByVal P_USERID As String, ByVal P_MONTH As Date, P_ORGID As Integer, P_ISDISSOLVE As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_PLAN_DETAIL_BY_ORG", New List(Of Object)(New Object() {P_USERID, P_MONTH, P_ORGID, P_ISDISSOLVE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_WORKING_TOTAL_SAL(ByVal P_USERID As String, ByVal P_SEND_DATE As Date, P_ORGID As Integer, P_ISDISSOLVE As Boolean) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.GET_WORKING_TOTAL_SAL", New List(Of Object)(New Object() {P_USERID, P_SEND_DATE, P_ORGID, P_ISDISSOLVE, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    Public Function CHECK_EXIST_SE_CONFIG(ByVal P_CODE As String) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.CHECK_EXIST_SE_CONFIG", New List(Of Object)(New Object() {P_CODE, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    Public Function GET_EMP_BY_USER(ByVal P_ID As Decimal) As Decimal
        Dim objects = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.GET_EMP_BY_USER", New List(Of Object)(New Object() {P_ID, OUT_NUMBER}))
        Return objects(0).ToString
    End Function

    Public Function UPDATE_RC_REQUEST_IS_LOCK(ByVal ID As String,
                                 ByVal STATUS_ID As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_RC_REQUEST_IS_LOCK",
                                                     New List(Of Object)(New Object() {
                                                     ID, STATUS_ID, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region
#Region "Request Portal"
    'Cập nhật tình trạng ứng viên
    Public Function UPDATE_APPROVED_REQUEST_PORTAL(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_APPROVED_REQUEST_PORTAL", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_APP_ALL_TEMPLATES() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_APP_ALL_TEMPLATES", New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Plan"
    'load kế hoạch tuyển dụng theo ID
    Public Function PLAN_GET_BY_ID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.PLAN_GET_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_GROUP_TITLE_BY_ID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_GROUP_TITLE_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_TITLE_BY_ID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_TITLE_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_INF_EMP_BY_ID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_INF_EMP_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
#End Region

#Region "trainning"
    Public Function Get_candidate_trainning(ByVal p_candidate_code As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.Get_candidate_trainning", New List(Of Object)(New Object() {p_candidate_code}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_SE_USER_BY_IS_RC(ByVal P_IS_RC As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_SE_USER_BY_IS_RC", New List(Of Object)(New Object() {P_IS_RC}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GetComboList(ByVal p_combobox_code As String) As List(Of OtherListDTO)
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.get_combobox", New List(Of Object)(New Object() {p_combobox_code}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            Dim re As New List(Of OtherListDTO)
            For Each item In ds.Tables(0).AsEnumerable
                re.Add(New OtherListDTO With {
                       .ID = item.Field(Of Decimal)("ID"),
                        .NAME_VN = item.Field(Of String)("NAME_VN"),
                        .NAME_EN = item.Field(Of String)("NAME_EN")
                       })
            Next
            Return re
        End If
        Return New List(Of OtherListDTO)
    End Function

    Public Function insert_cadidate_trainning(ByVal r As RC_CANDIDATE_TRAINNING_DTO) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.insert_cadidate_trainning",
                                                   New List(Of Object)(New Object() {
                                                   r.YEAR_GRA, r.NAME_SHOOLS, r.FORM_TRAIN_ID, r.SPECIALIZED_TRAIN,
                                                   r.RESULT_TRAIN, r.CERTIFICATE, r.EFFECTIVE_DATE_FROM,
                                                   r.EFFECTIVE_DATE_TO, r.CANDIDATE_ID, r.FROM_DATE, r.TO_DATE,
                                                   r.UPLOAD_FILE, r.FILE_NAME, r.TYPE_TRAIN_ID, r.RECEIVE_DEGREE_DATE,
                                                   r.IS_RENEWED, r.LEVEL_ID, r.POINT_LEVEL, r.CONTENT_LEVEL, r.NOTE, r.CERTIFICATE_CODE,
                                                   r.TYPE_TRAIN_NAME, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function update_cadidate_trainning(ByVal r As RC_CANDIDATE_TRAINNING_DTO) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.update_cadidate_trainning",
                                                   New List(Of Object)(New Object() {
                                                   r.ID, r.YEAR_GRA, r.NAME_SHOOLS, r.FORM_TRAIN_ID, r.SPECIALIZED_TRAIN,
                                                   r.RESULT_TRAIN, r.CERTIFICATE, r.EFFECTIVE_DATE_FROM,
                                                   r.EFFECTIVE_DATE_TO, r.CANDIDATE_ID, r.FROM_DATE, r.TO_DATE,
                                                   r.UPLOAD_FILE, r.FILE_NAME, r.TYPE_TRAIN_ID, r.RECEIVE_DEGREE_DATE,
                                                   r.IS_RENEWED, r.LEVEL_ID, r.POINT_LEVEL, r.CONTENT_LEVEL, r.NOTE, r.CERTIFICATE_CODE,
                                                   r.TYPE_TRAIN_NAME, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function delete_rc_candidate_trainning(ByVal ls_id As List(Of Decimal), ByRef num_row As Decimal) As Boolean
        Try
            For Each item In ls_id
                Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT.delete_rc_candidate_trainning",
                                                       New List(Of Object)(New Object() {item, OUT_NUMBER}))
                Dim re = Int32.Parse(obj(0).ToString())
                If re = -1 Then
                    Throw New Exception("lỗi")
                End If
                num_row = num_row + re
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region

#Region "Information Lists"
    Public Function GET_ALL_LIST(ByVal org_id As Decimal) As DataSet
        Try
            Dim ds As New DataSet
            ds = rep.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.GET_ALL_LIST", New List(Of Object)(New Object() {org_id}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_ALL_LIST1(ByVal org_id As Decimal) As DataSet
        Try
            Dim ds As New DataSet
            ds = rep.ExecuteToDataSet("PKG_PA_BUSINESS.GET_ALL_LIST1", New List(Of Object)(New Object() {org_id}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "hr_planing"
    Public Function GET_HR_PLANINGS_DEFAULT(Optional ByVal IS_BLANK As Boolean = True) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.GET_HR_PLANINGS_DEFAULT", New List(Of Object)(New Object() {IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GetTitleByOrgList(ByVal orgID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_TITLE_BYORG", New List(Of Object)(New Object() {orgID, isBlank, Common.Common.SystemLanguage.Name}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_TITLE(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_TITLE", New List(Of Object)(New Object() {isBlank, Common.Common.SystemLanguage.Name}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function IMPORT_HR_DETAIL(ByVal P_DOCXML As String, ByVal P_YEAR_PLAN_ID As Decimal, ByVal P_USER As String) As Boolean
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.IMPORT_HR_DETAIL", New List(Of Object)(New Object() {P_DOCXML, P_YEAR_PLAN_ID, P_USER}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function

    Public Function COPY_HR_DETAIL(ByVal P_FROM_ID As Decimal, ByVal P_TO_ID As Decimal, ByVal P_USER As String) As Boolean
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.COPY_HR_DETAIL", New List(Of Object)(New Object() {P_FROM_ID, P_TO_ID, P_USER}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function

    Public Function IMPORT_HR_BUDGET_DETAIL(ByVal P_DOCXML As String, ByVal P_YEAR_PLAN_ID As Decimal, ByVal P_USER As String) As Boolean
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.IMPORT_HR_BUDGET_DETAIL", New List(Of Object)(New Object() {P_DOCXML, P_YEAR_PLAN_ID, P_USER}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function

    Public Function EXPORT_DATA_HR_DETAIL(ByVal P_ORG_ID As Decimal, ByVal P_USERNAME As String, ByVal P_ISDISSOLVE As Boolean, ByVal P_YEAR_PLAN_ID As Decimal) As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.EXPORT_DATA_HR_DETAIL", New List(Of Object)(New Object() {P_ORG_ID, P_USERNAME, P_ISDISSOLVE, P_YEAR_PLAN_ID}))
        Return ds
    End Function
#End Region


    Public Function GET_RANK_SAL_BY_YEAR(ByVal P_USER As String, ByVal P_YEAR As Integer, ByVal PV_ORG_ID As Integer, ByVal P_ISDISSOLVE As Boolean) As DataSet

        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_DASHBOARD.GET_RANK_SAL_BY_YEAR", New List(Of Object)(New Object() {P_USER, PV_ORG_ID, P_ISDISSOLVE, P_YEAR}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStatisticGender(ByVal P_USER As String, ByVal _year As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_GENDER", New List(Of Object)(New Object() {P_USER, _year}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function REPORT_RANK_SAL_BY_YEAR_SAL1(ByVal P_USER As String, ByVal _year As Integer, ByVal _month As Integer, ByVal _org As Integer, ByVal P_ISDISSOLVE As Boolean) As Decimal
        Try
            Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT_DASHBOARD.REPORT_RANK_SAL_BY_YEAR_SAL1", New List(Of Object)(New Object() {P_USER, _org, _month, _year, P_ISDISSOLVE, OUT_NUMBER}))
            Return Decimal.Parse(obj(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DELETE_SALARY_ALLOWANCE_CANDIDATE(ByVal RC_PROGRAM_ID As Decimal, ByVal RC_CANDIDATE_ID As Decimal) As Decimal
        Try
            Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.DELETE_SALARY_ALLOWANCE_CANDIDATE", New List(Of Object)(New Object() {RC_PROGRAM_ID, RC_CANDIDATE_ID, OUT_NUMBER}))
            Return Decimal.Parse(obj(0).ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_TITLE_GROUP_ID(ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_COMMON_LIST.GET_TITLE_GROUP_ID", New List(Of Object)(New Object() {ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_STATUS_RECRUITMENT() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_STATUS_RECRUITMENT")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
End Class
