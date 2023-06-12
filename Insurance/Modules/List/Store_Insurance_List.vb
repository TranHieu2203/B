Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class Store_Insurance_List
    Private rep As New HistaffFrameworkRepository

#Region "DANH MỤC"

    'Load danh sách tên chức danh
    Public Function GetPositionName(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_POSITION_NAME", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Load danh sách
    Public Function GetList(ByVal P_TABLE_NAME As String, ByVal P_WHERE As String, ByVal P_STATUS As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_LIST", New List(Of Object)(New Object() {P_TABLE_NAME, P_WHERE, P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Delete danh sách
    Public Function Delete(ByVal P_TABLE_NAME As String, ByVal ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.DELETE_RECORD", New List(Of Object)(New Object() {P_TABLE_NAME, ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Load danh sách city - district - TEMPLATE
    Public Function GetListCityDistrict() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.SPS_PROVINCE_DISTRICT_TEMP", Nothing)
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

#Region "Đơn vị BH - (INS_LIST_INSURANCE)"
    'Thêm mới đơn vị bảo hiểm
    Public Function ADDNEW_INS_LIST_INSURANCE(ByVal CODE As String,
                                              ByVal NAME As String,
                                              ByVal ADDRESS As String,
                                              ByVal BANK_NAME As String,
                                              ByVal ZIP_CODE As String,
                                              ByVal PHONE As String,
                                              ByVal REMARK As String,
                                              ByVal STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_LIST_INSURANCE", _
                                                   New List(Of Object)(New Object() {
                                                                       CODE,
                                                                       NAME,
                                                                       ADDRESS,
                                                                       BANK_NAME,
                                                                       ZIP_CODE,
                                                                       PHONE,
                                                                       REMARK,
                                                                       STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật đơn vị bảo hiểm
    Public Function UPDATE_INS_LIST_INSURANCE(ByVal ID As Int32,
                                                ByVal CODE As String,
                                                ByVal NAME As String,
                                                ByVal ADDRESS As String,
                                                ByVal BANK_NAME As String,
                                                ByVal ZIP_CODE As String,
                                                ByVal PHONE As String,
                                                ByVal REMARK As String,
                                                ByVal STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_LIST_INSURANCE", _
                                                     New List(Of Object)(New Object() {
                                                                         ID,
                                                                         CODE,
                                                                         NAME,
                                                                         ADDRESS,
                                                                         BANK_NAME,
                                                                         ZIP_CODE,
                                                                         PHONE,
                                                                         REMARK,
                                                                         STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

#End Region

#Region "Nơi khám bệnh - (INS_LIST_AREA)"
    'Load danh sách
    Public Function GetList_AREA(ByVal P_STATUS As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_LIST_AREA", New List(Of Object)(New Object() {P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới đơn vị bảo hiểm
    Public Function ADDNEW_INS_LIST_AREA(ByVal CODE As String,
                                              ByVal NAME As String,
                                              ByVal ADDRESS As String,
                                              ByVal CITY As Int32,
                                              ByVal DISTRICT As Int32,
                                              ByVal REMARK As String,
                                              ByVal IS_REG_DEFAULT As Int32,
                                              ByVal STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_LIST_AREA", _
                                                   New List(Of Object)(New Object() {
                                                                       CODE,
                                                                       NAME,
                                                                       ADDRESS,
                                                                       CITY,
                                                                       DISTRICT,
                                                                       REMARK,
                                                                       IS_REG_DEFAULT,
                                                                       STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật đơn vị bảo hiểm
    Public Function UPDATE_INS_LIST_AREA(ByVal ID As Int32,
                                                ByVal CODE As String,
                                                ByVal NAME As String,
                                                ByVal ADDRESS As String,
                                                ByVal CITY As Int32,
                                                ByVal DISTRICT As Int32,
                                                ByVal REMARK As String,
                                                ByVal IS_REG_DEFAULT As Int32,
                                                ByVal STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_LIST_AREA", _
                                                     New List(Of Object)(New Object() {
                                                                         ID,
                                                                         CODE,
                                                                         NAME,
                                                                         ADDRESS,
                                                                         CITY,
                                                                         DISTRICT,
                                                                         REMARK,
                                                                         IS_REG_DEFAULT,
                                                                         STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

#End Region

#Region "Biến động BH"

    'Load danh sách
    Public Function GET_INS_LIST_ARISING_TYPE(ByVal P_STATUS As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_LIST_ARISING_TYPE", New List(Of Object)(New Object() {P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới biến động bảo hiểm
    Public Function ADDNEW_INS_LIST_ARISING_TYPE(ByVal NAME As String,
                                              ByVal TYPE As String,
                                              ByVal SYMBOL As String,
                                              ByVal DETAIL As String,
                                              ByVal STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_LIST_ARISING_TYPE", _
                                                   New List(Of Object)(New Object() {
                                                                       NAME,
                                                                       TYPE,
                                                                       SYMBOL,
                                                                       DETAIL,
                                                                       STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật biến động bảo hiểm
    'Cập nhật đơn vị bảo hiểm
    Public Function UPDATE_INS_LIST_ARISING_TYPE(ByVal ID As Int32,
                                                ByVal NAME As String,
                                                ByVal TYPE As String,
                                                ByVal SYMBOL As String,
                                                ByVal DETAIL As String,
                                                ByVal STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_LIST_ARISING_TYPE", _
                                                     New List(Of Object)(New Object() {
                                                                         ID,
                                                                         NAME,
                                                                         TYPE,
                                                                         SYMBOL,
                                                                         DETAIL,
                                                                         STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Chế độ bảo hiểm"
    'Load danh sách
    Public Function GET_INS_LIST_ARISING_REGIMES(ByVal P_STATUS As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_LIST_ARISING_REGIMES", New List(Of Object)(New Object() {P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới chế độ bảo hiểm
    Public Function ADDNEW_INS_LIST_REGIMES(ByVal P_GROUP_ARISING As String,
                                              ByVal P_NAME As String,
                                              ByVal P_SALARY_TYPE As String,
                                              ByVal P_DAY_TYPE As String,
                                              ByVal P_TOTAL_DAYS As Decimal,
                                              ByVal P_ENJOY_MONEY As Decimal,
                                              ByVal P_STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_LIST_REGIMES", _
                                                   New List(Of Object)(New Object() {
                                                                       P_GROUP_ARISING,
                                                                       P_NAME,
                                                                       P_SALARY_TYPE,
                                                                       P_DAY_TYPE,
                                                                       P_TOTAL_DAYS,
                                                                       P_ENJOY_MONEY,
                                                                       P_STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật chế độ bảo hiểm
    Public Function UPDATE_INS_LIST_REGIMES(ByVal P_ID As Int32,
                                                ByVal P_GROUP_ARISING As String,
                                                ByVal P_NAME As String,
                                                ByVal P_SALARY_TYPE As String,
                                                ByVal P_DAY_TYPE As String,
                                                ByVal P_TOTAL_DAYS As Decimal,
                                                ByVal P_ENJOY_MONEY As Decimal,
                                                ByVal P_STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_LIST_REGIMES", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_GROUP_ARISING,
                                                                         P_NAME,
                                                                         P_SALARY_TYPE,
                                                                         P_DAY_TYPE,
                                                                         P_TOTAL_DAYS,
                                                                         P_ENJOY_MONEY,
                                                                         P_STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Danh mục loại điều chỉnh hồ sơ Bảo hiểm"
    'Load danh sách
    Public Function GET_INS_MODIFY_TYPE(ByVal P_STATUS As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_MODIFY_TYPE", New List(Of Object)(New Object() {P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới loại điều chỉnh hồ sơ Bảo hiểm
    Public Function ADDNEW_INS_MODIFY_TYPE(ByVal P_NAME As String,
                                              ByVal P_FIELD_UPDATE As String,
                                              ByVal P_STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_MODIFY_TYPE", _
                                                   New List(Of Object)(New Object() {
                                                                       P_NAME,
                                                                       P_FIELD_UPDATE,
                                                                       P_STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật loại điều chỉnh hồ sơ Bảo hiểm
    Public Function UPDATE_INS_MODIFY_TYPE(ByVal P_ID As Int32,
                                                ByVal P_NAME As String,
                                                ByVal P_FIELD_UPDATE As String,
                                                ByVal P_STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_MODIFY_TYPE", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_NAME,
                                                                         P_FIELD_UPDATE,
                                                                         P_STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Danh mục thiết lập ký hiệu công."
    'Load danh sách
    Public Function GetList_TIMESHEET_SYMBOL(ByVal P_STATUS As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_LIST_TIMESHEET_SYMBOL", New List(Of Object)(New Object() {P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Thêm mới thiết lập ký hiệu công
    Public Function ADDNEW_INS_TIMESHEET_SYMBOL(ByVal P_AT_SIGN_ID As Int32,
                                              ByVal P_TYPE As String,
                                              ByVal P_PARAM As Int32,
                                              ByVal P_VALIDITY_DATE As Date,
                                              ByVal P_EXPIRY_DATE As Date?,
                                              ByVal P_REMARK As String,
                                              ByVal P_STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_TIMESHEET_SYMBOL", _
                                                   New List(Of Object)(New Object() {
                                                                       P_AT_SIGN_ID,
                                                                       P_TYPE,
                                                                       P_PARAM,
                                                                       P_VALIDITY_DATE,
                                                                       P_EXPIRY_DATE,
                                                                       P_REMARK,
                                                                       P_STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật thiết lập ký hiệu công
    Public Function UPDATE_INS_TIMESHEET_SYMBOL(ByVal P_ID As Int32,
                                                ByVal P_AT_SIGN_ID As Int32,
                                                ByVal P_TYPE As String,
                                                ByVal P_PARAM As Int32,
                                                ByVal P_VALIDITY_DATE As Date,
                                                ByVal P_EXPIRY_DATE As Date?,
                                                ByVal P_REMARK As String,
                                                ByVal P_STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_TIMESHEET_SYMBOL", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_AT_SIGN_ID,
                                                                           P_TYPE,
                                                                           P_PARAM,
                                                                           P_VALIDITY_DATE,
                                                                           P_EXPIRY_DATE,
                                                                           P_REMARK,
                                                                           P_STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function


    'Kiểm tra thông tin chức danh đã tồn tại
    Public Function CHECK_INS_TIMESHEET_SYMBOL(ByVal P_ID As Int32, ByVal P_TYPE_CODE As String, ByVal P_AT_SIGN_ID As Int32, ByVal P_VALIDITY_DATE As DateTime) As Boolean
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.CHECK_INS_TIMESHEET_SYMBOL", New List(Of Object)(New Object() {P_ID, P_TYPE_CODE, P_AT_SIGN_ID, P_VALIDITY_DATE}))
        If Not ds Is Nothing And Not ds.Tables(0) Is Nothing And ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
#End Region

#Region "Danh mục nhóm quyền lợi bảo hiểm sức khỏe (AON)"

    'Thêm mới
    Public Function ADDNEW_INS_AON(ByVal P_CODE As String,
                                              ByVal P_NAME As String,
                                              ByVal P_AMOUNT As Int32,
                                              ByVal P_VALIDITY_DATE As Date,
                                              ByVal P_REMARK As String,
                                              ByVal P_STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_AON", _
                                                   New List(Of Object)(New Object() {
                                                                       P_CODE,
                                                                       P_NAME,
                                                                       P_AMOUNT,
                                                                       P_VALIDITY_DATE,
                                                                       P_REMARK,
                                                                       P_STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật
    Public Function UPDATE_INS_AON(ByVal P_ID As Int32,
                                                ByVal P_CODE As String,
                                                ByVal P_NAME As String,
                                                ByVal P_AMOUNT As Int32,
                                                ByVal P_VALIDITY_DATE As Date,
                                                ByVal P_REMARK As String,
                                                ByVal P_STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_AON", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_CODE,
                                                                           P_NAME,
                                                                           P_AMOUNT,
                                                                           P_VALIDITY_DATE,
                                                                           P_REMARK,
                                                                           P_STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#Region "Thiết lập nhóm quyền lợi theo chức danh"

    'Thêm mới
    Public Function ADDNEW_INS_POSITION(ByVal P_Position_ID As String,
                                        ByVal P_Position_NAME As String,
                                        ByVal P_INS_AON_ID As Int32,
                                              ByVal P_NAME As String,
                                              ByVal P_VALIDITY_DATE As Date,
                                              ByVal P_REMARK As String,
                                              ByVal P_STATUS As Int32,
                                              ByVal CREATE_BY As String,
                                              ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.ADDNEW_INS_POSITION", _
                                                   New List(Of Object)(New Object() {
                                                                       P_Position_ID,
                                                                       String.Empty,
                                                                       P_INS_AON_ID,
                                                                       P_NAME,
                                                                       P_VALIDITY_DATE,
                                                                       P_REMARK,
                                                                       P_STATUS,
                                                                       CREATE_BY,
                                                                       LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật
    Public Function UPDATE_INS_POSITION(ByVal P_ID As Int32,
                                                ByVal P_POSITION_ID As String,
                                                ByVal P_POSITION_NAME As String,
                                                ByVal P_INS_AON_ID As Int32,
                                                ByVal P_NAME As String,
                                                ByVal P_VALIDITY_DATE As Date,
                                                ByVal P_REMARK As String,
                                                ByVal P_STATUS As Int32,
                                                ByVal MODIFY_BY As String,
                                                ByVal LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.UPDATE_INS_POSITION", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_POSITION_ID,
                                                                         String.Empty,
                                                                         P_INS_AON_ID,
                                                                           P_NAME,
                                                                           P_VALIDITY_DATE,
                                                                           P_REMARK,
                                                                           P_STATUS,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Kiểm tra thông tin chức danh đã tồn tại
    Public Function CHECK_INS_POSITION_ISEXIST(ByVal P_ID As Int32, ByVal P_POSITION_NAME As String) As Boolean
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.CHECK_INS_POSITION_ISEXIST", New List(Of Object)(New Object() {P_ID, P_POSITION_NAME}))
        If Not ds Is Nothing And Not ds.Tables(0) Is Nothing And ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
#End Region

#Region "Quy định đối tượng và % đóng bảo hiểm"
    'Load danh sách BH theo loại
    Public Function GET_INS_LIST_AREA_ALL(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_LIST_AREA_ALL", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Load danh sách BH theo vùng
    Public Function GET_INS_LIST_AREA_VALUE(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_LIST_AREA_VALUE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Cập nhật "Đối tượng BH"
    Public Function SAVE_INS_LIST_OBJECTS(ByVal P_IS_CHECKED As Int32,
                                          ByVal P_INS_TYPE As String,
                                          ByVal P_CONTRACT_TYPE_ID As Int32,
                                          ByVal MODIFY_BY As String,
                                          ByVal LOG As String) As String
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.SAVE_INS_LIST_OBJECTS", _
                                                     New List(Of Object)(New Object() {
                                                                         P_IS_CHECKED,
                                                                         P_INS_TYPE,
                                                                         P_CONTRACT_TYPE_ID,
                                                                         MODIFY_BY,
                                                                         LOG, OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật "Vùng"
    Public Function SAVE_INS_LIST_AREA_VALUE(ByVal P_ID As Int32,
                                          ByVal P_AREA_TYPE As String,
                                          ByVal P_INS_ID As Int32,
                                          ByVal P_CEILING_AMOUNT As Int32,
                                          ByVal P_EMPLOYEE_PERCENT As Decimal,
                                          ByVal P_COMPANY_PERCENT As Decimal,
                                          ByVal P_EFFECTIVE_FROM_DATE As Date,
                                          ByVal P_EFFECTIVE_TO_DATE As Date,
                                          ByVal P_NOTE As String,
                                          ByVal MODIFY_BY As String,
                                          ByVal LOG As String) As String
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.SAVE_INS_LIST_AREA_VALUE", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_AREA_TYPE,
                                                                         P_INS_ID,
                                                                         P_CEILING_AMOUNT,
                                                                         P_EMPLOYEE_PERCENT,
                                                                         P_COMPANY_PERCENT,
                                                                         P_EFFECTIVE_FROM_DATE,
                                                                         P_EFFECTIVE_TO_DATE,
                                                                         P_NOTE,
                                                                         MODIFY_BY,
                                                                         LOG,
                                                                         OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Xóa "Vùng"
    Public Function DELETE_INS_LIST_AREA_VALUE(ByVal P_ID As Int32,
                                          ByVal P_INS_ID As Int32,
                                          ByVal P_EFFECTIVE_FROM_DATE As Date,
                                          ByVal MODIFY_BY As String,
                                          ByVal LOG As String) As String
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.DELETE_INS_LIST_AREA_VALUE", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_INS_ID,
                                                                         P_EFFECTIVE_FROM_DATE,
                                                                         MODIFY_BY,
                                                                         LOG,
                                                                         OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật "Mức đóng BH"
    Public Function SAVE_INS_LIST_AREA_ALL(ByVal P_ID As Int32,
                                          ByVal P_SICK As Int32,
                                          ByVal P_MATERNITY As Int32,
                                          ByVal P_OFF_IN_HOUSE As Int32,
                                          ByVal P_OFF_TOGETHER As Int32,
                                          ByVal P_RETIRE_MALE_AGE As Int32,
                                          ByVal P_RETIRE_FEMALE_AGE As Int32,
                                          ByVal P_EFFECTIVE_FROM_DATE As Date,
                                          ByVal P_EFFECTIVE_TO_DATE As Date,
                                          ByVal P_SI_DATE As Int32,
                                          ByVal P_HI_DATE As Int32,
                                          ByVal P_INS_ATTENDANCE As Int32,
                                          ByVal P_USER As String,
                                          ByVal P_MODIFY_LOG As String
                                          ) As String
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.SAVE_INS_LIST_AREA_ALL", _
                                                     New List(Of Object)(New Object() {
                                                                         P_ID,
                                                                         P_SICK,
                                                                         P_MATERNITY,
                                                                         P_OFF_IN_HOUSE,
                                                                         P_OFF_TOGETHER,
                                                                         P_RETIRE_MALE_AGE,
                                                                         P_RETIRE_FEMALE_AGE,
                                                                         P_EFFECTIVE_FROM_DATE,
                                                                         P_EFFECTIVE_TO_DATE,
                                                                         P_SI_DATE,
                                                                         P_HI_DATE,
                                                                         P_INS_ATTENDANCE,
                                                                         P_USER,
                                                                         P_MODIFY_LOG,
                                                                         OUT_NUMBER
                                                   }))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region

#End Region

#Region "Nghiệp vụ"

#Region "Khai báo biến động bảo hiểm"
    'Load danh sách
    Public Function GET_INS_EMPINFO(ByVal P_EMPLOYEE_ID As Int32,
                                    ByVal P_ORG_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_INS_EMPINFO", New List(Of Object)(New Object() {P_EMPLOYEE_ID, P_ORG_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
#End Region

#End Region

    Public Function GET_INS_POSITION_BY_ID(ByVal P_TITLE_ID As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_LIST.GET_INS_POSITION_BY_ID", New List(Of Object)(New Object() {P_TITLE_ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_VALUE_AREA(ByVal CODE As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_HU_IPROFILE_SALARY.GET_VALUE_AREA", _
                                                 New List(Of Object)(New Object() {CODE}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function


    'Kiểm tra thông tin  Danh mục nơi khám chữa bệnh đã tồn tại
    Public Function CHECK_INS_LIST_AREA(ByVal P_ID As Int32, ByVal P_CODE As Int32, ByVal P_CITY_ID As Int32) As Boolean
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.CHECK_INS_LIST_AREA", New List(Of Object)(New Object() {P_ID, P_CODE, P_CITY_ID}))
        If Not ds Is Nothing And Not ds.Tables(0) Is Nothing And ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Function GET_ARISING_MANUAL(ByVal P_ID As Int32) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_LIST.GET_ARISING_MANUAL", New List(Of Object)(New Object() {P_ID}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
