Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Public Class RecruitmentRepository
    Inherits RecruitmentRepositoryBase

#Region "Hoadm"

#Region "Lấy data danh mục Combo"

    Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OTHER_LIST",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Get_HU_WORK_PLACE(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.Get_HU_WORK_PLACE",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetProvinceList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_PROVINCE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetContractTypeList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_CONTRACT_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE_BYORG",
                                           New With {.P_ORGID = orgID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTitleByOrgListInPlan(ByVal orgID As Decimal, _year As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE_BYORG_INPLAN",
                                           New With {.P_ORGID = orgID,
                                                     .P_YEAR = _year,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetProgramExamsList(ByVal programID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_RC_PROGRAM_EXAMS",
                                           New With {.P_RC_PROGRAM_ID = programID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetProgramList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_RC_PROGRAM",
                                           New With {.P_ORG_ID = orgID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Get Combobox Data"

    ''' <summary>
    ''' Lấy dữ liệu cho combobox
    ''' </summary>
    ''' <param name="_combolistDTO">Trả về dữ liệu combobox</param>
    ''' <returns>TRUE: Success</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
        Dim query

        'Nguồn tuyển dụng -- TanVN - 02/06/2016
        If _combolistDTO.GET_SOURCE_REC Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "RC_SOURCE_REC" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_SOURCE_REC = query
        End If

        'Loại cấp chức danh
        If _combolistDTO.GET_TITLE_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "TITLE_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TITLE_TYPE = query
        End If

        'Hình thức tuyển dụng
        If _combolistDTO.GET_RECRUITMENT Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "RECRUITMENT_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_RECRUITMENT = query
        End If
        'Danh hieu khen thuong
        If _combolistDTO.GET_TROPHIES_COMMEND Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "TROPHIES_COMMEND" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TROPHIES_COMEND = query
        End If
        'Hình thức thanh toán
        If _combolistDTO.GET_PAYMENT_COMEND Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "PAYMENT_COMMEND" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_PAYMENT_COMEND = query
        End If

        'Phụ cấp kiêm nhiệm
        If _combolistDTO.GET_CON_ALLOW Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "CON_ALLOW" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_CON_ALLOW = query
        End If


        'Danh mục loại quyết định kỷ luât
        If _combolistDTO.GET_DIS_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "DIS_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DIS_TYPE = query
        End If

        'Danh mục trang thiết bị
        If _combolistDTO.GET_DEVICES_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "DEVICES_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DEVICES_TYPE = query
        End If

        'Danh mục trang thiết bị
        If _combolistDTO.GET_DEVICES_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "DEVICES_STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DEVICES_STATUS = query
        End If


        'Loại phụ cấp
        If _combolistDTO.GET_ALLOWTYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "ALLOW_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_ALLOWTYPE = query
        End If

        'Biểu thuế
        If _combolistDTO.GET_TAX_TABLE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "TAX_TABLE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TAX_TABLE = query
        End If
        'Nhóm va chạm
        If _combolistDTO.GET_GIMPACT Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "G_IMPACT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_GIMPACT = query
        End If

        'Phương tiện
        If _combolistDTO.GET_MEANS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "MEANS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MEANS = query
        End If

        'Danh mục
        If _combolistDTO.GET_LIST Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "LIST" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LIST = query
        End If


        'Loại
        If _combolistDTO.GET_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TYPE = query
        End If


        'Nhóm
        If _combolistDTO.GET_GROUP Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "GROUP" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_GROUP = query
        End If

        'Loại tiền
        If _combolistDTO.GET_MONEYTYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "MONEY_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MONEYTYPE = query
        End If

        'Trạng thái bồi thường
        If _combolistDTO.GET_STATUS_INDEMNITY Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "STATUS_INDEMNITY" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_STATUS_INDEMNITY = query
        End If

        'Trạng thái hỗ trợ
        If _combolistDTO.GET_STATUS_SUPPORT Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "STATUS_SUPPORT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_STATUS_SUPPORT = query
        End If

        'Mức độ hỗ trợ
        If _combolistDTO.GET_LEVEL_SUPPORT Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "LEVEL_SUPPORT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEVEL_SUPPORT = query
        End If
        'Giới tính
        If _combolistDTO.GET_GENDER Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "GENDER" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_GENDER = query
        End If

        'Dân tộc
        If _combolistDTO.GET_NATIVE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "NATIVE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_NATIVE = query
        End If

        'Tôn giáo
        If _combolistDTO.GET_RELIGION Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "RELIGION" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_RELIGION = query
        End If

        'Tình trạng gia đình
        If _combolistDTO.GET_FAMILY_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "FAMILY_STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_FAMILY_STATUS = query
        End If

        'Loại sức khỏe
        If _combolistDTO.GET_HEALTH_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "HEALTH_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_HEALTH_TYPE = query
        End If

        'Danh mục quận huyện
        If _combolistDTO.GET_DISTRICT Then
            query = (From p In Context.HU_DISTRICT
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New DistrictDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .PROVINCE_ID = p.PROVINCE_ID}).ToList
            _combolistDTO.LIST_DISTRICT = query
        End If
        If _combolistDTO.GET_NATION Then
            query = (From p In Context.HU_NATION
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New NationDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_NATION = query
        End If
        If _combolistDTO.GET_PROVINCE Then
            query = (From p In Context.HU_PROVINCE
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New ProvinceDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NATION_ID = p.NATION_ID,
                         .NATION_NAME = p.HU_NATION.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_PROVINCE = query
        End If


        'Danh mục cấp đơn vị
        If _combolistDTO.GET_ORG_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "ORG_LEVEL" _
                     And p.ACTFLG = "A" _
                     Order By p.CODE
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.CODE + " - " + p.NAME_EN,
                         .NAME_VN = p.CODE + " - " + p.NAME_VN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_ORG_LEVEL = query
        End If

        'Danh mục loại Hợp đồng được load lên Danh Mục --> Loại hợp đồng
        If _combolistDTO.GET_CONTRACTYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "CONTRACT_TYPE" _
                     And p.ACTFLG = "A" _
                     Order By p.CODE
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.NAME_EN,
                         .NAME_VN = p.NAME_VN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_CONTRACTYPE = query
        End If


        'Danh mục trình độ văn hóa( Danh mục trường Đào tạo, vd: ĐH Quốc Gia,...
        If _combolistDTO.GET_ACADEMY Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "ACADEMY" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_ACADEMY = query
        End If

        'Danh mục ngành đào tạo:(Chuyên môn: Kỹ sư CNTT,...)
        If _combolistDTO.GET_MAJOR Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "MAJOR" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MAJOR = query
        End If

        'Danh mục bằng cấp: Đại học, Cao Đẳng, Kỹ sư,...
        If _combolistDTO.GET_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEVEL = query
        End If
        'Danh mục trình độ tin học: 
        If _combolistDTO.GET_COMPUTER_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMPUTER_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMPUTER_LEVEL = query
        End If
        'Danh mục ngoại ngữ :  
        If _combolistDTO.GET_LANGUAGE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LANGUAGE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LANGUAGE = query
        End If
        'Danh mục hình thức đào tạo :  
        If _combolistDTO.GET_TRAINING_FORM Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TRAINING_FORM" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TRAINING_FORM = query
        End If
        'Danh mục trình độ học vấn :  
        If _combolistDTO.GET_LEARNING_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LEARNING_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEARNING_LEVEL = query
        End If
        'Danh mục xếp loại học vấn:  
        If _combolistDTO.GET_MARK_EDU Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "MARK_EDU" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MARK_EDU = query
        End If

        'Danh mục trình độ ngoại ngữ :  
        If _combolistDTO.GET_LANGUAGE_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LANGUAGE_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LANGUAGE_LEVEL = query
        End If

        'Danh mục trạng thái quyết định
        If _combolistDTO.GET_DECISION_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DECISION_STATUS = query
        End If

        'Danh mục đối tượng kỷ luật
        If _combolistDTO.GET_DISCIPLINE_OBJ Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_OBJECT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_OBJ = query
        End If

        'Danh mục cấp kỷ luật
        If _combolistDTO.GET_DISCIPLINE_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_LEVEL = query
        End If

        'Danh mục hình thức kỷ luật
        If _combolistDTO.GET_DISCIPLINE_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_TYPE = query
        End If

        'Danh mục đối tượng khen thưởng
        If _combolistDTO.GET_COMMEND_OBJ Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMMEND_OBJECT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMMEND_OBJ = query
        End If

        'Danh mục cấp khen thưởng
        If _combolistDTO.GET_COMMEND_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMMEND_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMMEND_LEVEL = query
        End If

        'Danh mục hình thức khen thưởng
        If _combolistDTO.GET_COMMEND_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMMEND_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMMEND_TYPE = query
        End If

        'Danh mục hình thức chấm dứt hợp đồng
        If _combolistDTO.GET_TER_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TER_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TER_TYPE = query
        End If

        'Danh mục lý do nghỉ việc
        If _combolistDTO.GET_TER_REASON Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TER_REASON" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TER_REASON = query
        End If

        'Danh mục trạng thái nghỉ việc
        If _combolistDTO.GET_TER_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "STATUS"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_TER_STATUS = query
        End If
        'Danh mục trạng thái kiêm nhiệm
        If _combolistDTO.GET_STATUS_CONCURRENTLY Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_STATUS_CONCURRENTLY = query
        End If
        'Danh mục trạng thái khen thưởng
        If _combolistDTO.GET_COMMEND_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_COMMEND_STATUS = query
        End If
        'Danh mục trạng thái 
        If _combolistDTO.GET_DISCIPLINE_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "STATUS" _
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_DISCIPLINE_STATUS = query
        End If
        'Danh mục mối quan hệ
        If _combolistDTO.GET_RELATION Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "RELATION" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_RELATION = query
        End If

        'Danh mục căn cứ quyết định
        If _combolistDTO.GET_ATTATCH_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "ATTATCH_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_ATTATCH_TYPE = query
        End If

        'Cấp chức danh
        If _combolistDTO.GET_TITLE_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Where p.ACTFLG = "A" And p.TYPE_ID = 160
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_TITLE_LEVEL = query
        End If

        'Danh mục lĩnh vực kinh doanh
        If _combolistDTO.GET_BUSINESS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "BUSINESS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_BUSINESS = query
        End If

        'Danh mục loại hình doanh nghiệp
        If _combolistDTO.GET_BUSINESSTYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "BUSINESSTYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_BUSINESSTYPE = query
        End If

        'Danh mục loại vi phạm
        If _combolistDTO.GET_VIOLATIONTYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "VIOLATION_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_VIOLATIONTYPE = query
        End If

        'Danh mục hình thức trả lương
        If _combolistDTO.GET_PAYMENT_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "PAYMENT_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_PAYMENT_TYPE = query
        End If

        'Danh mục vùng
        If _combolistDTO.GET_AREA Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "AREA" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_AREA = query
        End If

        'Danh mục loại đồng phục
        If _combolistDTO.GET_UNIFORMTYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "UNIFORM_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_UNIFORMTYPE = query
        End If

        'Danh mục đơn giá hệ số lương
        If _combolistDTO.GET_SALARY_COEFFICIENT_PRICE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "SALARY_COEFFICIENT_PRICE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_SALARY_COEFFICIENT_PRICE = query
        End If


        'Danh mục trạng thái cấp phát đồng phục
        If _combolistDTO.GET_ALLOCATESTATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "ALLOCATE_STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_ALLOCATESTATUS = query
        End If

        'Danh mục đơn vị cung cấp
        If _combolistDTO.GET_SUPPLYUNIT Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "SUPPLY_UNIT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_SUPPLYUNIT = query
        End If

        'Danh mục nhóm cấp bậc
        If _combolistDTO.GET_GROUP_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "GROUP_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_GROUP_LEVEL = query
        End If

        'Danh mục nhóm lương
        If _combolistDTO.GET_GROUP_SALARY Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "GROUP_SALARY" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1
                     }).ToList
            _combolistDTO.LIST_GROUP_SALARY = query
        End If
        'Bank
        If _combolistDTO.GET_BANK Then
            query = (From p In Context.HU_BANK
                     Where p.ACTFLG = "A"
                     Order By p.NAME.ToUpper
                     Select New BankListDTO With {
                         .ID = p.ID,
                         .BANK_ID = p.ID,
                         .BANK_NAME = p.NAME}).ToList
            _combolistDTO.LIST_BANK = query
        End If

        'Branch
        If _combolistDTO.GET_BANK_BRACH Then
            query = (From p In Context.HU_BANK_BRANCH
                     Where p.ACTFLG = "A"
                     Order By p.NAME.ToUpper
                     Select New BankBranchDTO With {
                         .ID = p.ID,
                         .BRANCH_ID = p.ID,
                         .BANK_ID = p.BANK_ID,
                         .BRANCH_NAME = p.NAME}).ToList
            _combolistDTO.LIST_BANK_BRACH = query
        End If

        'Recruitment type
        If _combolistDTO.GET_RECRUITMENT_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "RC_RECRUIT_TYPE"
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_RECRUITMENT_TYPE = query
        End If

        'Stage
        If _combolistDTO.GET_STAGE Then
            query = (From p In Context.RC_STAGE
                     Order By p.TITLE.ToUpper
                     Select New StageDTO With {
                         .ID = p.ID,
                         .TITLE = p.TITLE}).ToList
            _combolistDTO.LIST_STAGE = query
        End If

        'Danh mục loại hợp đồng
        If _combolistDTO.GET_CONTRACTTYPE Then
            query = (From p In Context.HU_CONTRACT_TYPE
                     From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                     Where p.ACTFLG = "A" And p.TYPE_CODE Is Nothing And p.IS_REQUIREMENT = -1
                     Order By p.NAME
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .CODE = p.CODE,
                        .NAME = p.NAME,
                        .TYPE_CODE = ot.CODE}).ToList
            _combolistDTO.LIST_CONTRACTTYPE = query
        End If

        Return True
    End Function

#End Region
#End Region

End Class
