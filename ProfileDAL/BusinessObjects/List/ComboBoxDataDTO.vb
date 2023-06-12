Public Class ComboBoxDataDTO
    Public Property GET_TDCM As Boolean
    Public Property LIST_TDCM As List(Of OtherListDTO)

    Public Property GET_TDT As Boolean
    Public Property LIST_TDT As List(Of OtherListDTO)

    Public Property GET_LEVEL_DISCIPLINE As Boolean
    Public Property LIST_LEVEL_DISCIPLINE As List(Of OtherListDTO)

    Public Property GET_CERTIFICATE_TYPE As Boolean
    Public Property LIST_CERTIFICATE_TYPE As List(Of OtherListDTO)

    Public Property GET_CERTIFICATE_TYPE_2 As Boolean
    Public Property LIST_CERTIFICATE_TYPE_2 As List(Of OtherListDTO)

    Public Property GET_HANDOVER_CONTENT As Boolean
    Public Property LIST_HANDOVER_CONTENT As List(Of OtherListDTO)

    Public Property GET_DEBT_TYPE As Boolean
    Public Property LIST_DEBT_TYPE As List(Of OtherListDTO)

    Public Property GET_DEBT_STATUS As Boolean
    Public Property LIST_DEBT_STATUS As List(Of OtherListDTO)

    Public Property GET_UNIT_LEVEL As Boolean
    Public Property LIST_UNIT_LEVEL As List(Of OtherListDTO)

    'Loại nguyên nhân tai nạn.
    Public Property GET_TYPE_INS_STATUS As Boolean
    Public Property LIST_INS_STATUS As List(Of OtherListDTO)

    'Trang thai so BH
    Public Property GET_REASON As Boolean
    Public Property LIST_REASON As List(Of OtherListDTO)

    'Loại công tác.
    Public Property GET_TYPE_WORK As Boolean
    Public Property LIST_TYPE_WORK As List(Of OtherListDTO)

    'list phụ câp.
    Public Property GET_WORKING_ALLOWANCE_LIST As Boolean
    Public Property LIST_WORKING_ALLOWANCE_LIST As List(Of AllowanceListDTO)
    'Loại bảo hộ lao dộng.
    Public Property GET_LABOURPROTECTION As Boolean
    Public Property LIST_LABOURPROTECTION As List(Of LabourProtectionDTO)

    'Danh mục trạng thái làm việc
    Public Property GET_WORKSTATUS As Boolean
    Public Property LIST_WORKSTATUS As List(Of OtherListDTO)

    'Danh mục giới tính
    Public Property GET_GENDER As Boolean
    Public Property LIST_GENDER As List(Of OtherListDTO)

    'Danh mục size loại bảo hộ lao động
    Public Property GET_SIZE_LABOURPROTECTION As Boolean
    Public Property LIST_SIZE_LABOURPROTECTION As List(Of OtherListDTO)

    'Danh sách chức danh
    Public Property GET_TITLE As Boolean
    Public Property LIST_TITLE As List(Of TitleDTO)

    'Danh sách công việc phải làm
    Public Property GET_WORK_DESCRIPTION As Boolean
    Public Property LIST_WORK_DESCRIPTION As List(Of OtherListDTO)

    'Danh sách Industry
    Public Property GET_INDUSTRY As Boolean
    Public Property LIST_INDUSTRY As List(Of IndustryDTO)


    'Danh sách ngân hàng
    Public Property GET_BANK As Boolean
    Public Property LIST_BANK As List(Of BankDTO)

    'Danh sách ngân hàng có cả CODE - Phần thông tin khác của nhân viên.
    Public Property GET_BANK_CODE As Boolean
    Public Property LIST_BANK_CODE As List(Of BankDTO)

    'Danh mục trình độ văn hóa( Danh mục trường Đào tạo, vd: ĐH Quốc Gia,...
    Public Property GET_ACADEMY As Boolean
    Public Property LIST_ACADEMY As List(Of OtherListDTO)


    'Danh mục ngành đào tạo:(Chuyên môn: Kỹ sư CNTT,...)
    Public Property GET_MAJOR As Boolean
    Public Property LIST_MAJOR As List(Of OtherListDTO)

    'Danh mục bằng cấp: Đại học, Cao Đẳng, Kỹ sư,...
    Public Property GET_LEVEL As Boolean
    Public Property LIST_LEVEL As List(Of OtherListDTO)

    'Danh mục trình độ tin học: 
    Public Property GET_COMPUTER_LEVEL As Boolean
    Public Property LIST_COMPUTER_LEVEL As List(Of OtherListDTO)

    'Danh mục ngoại ngữ :  
    Public Property GET_LANGUAGE As Boolean
    Public Property LIST_LANGUAGE As List(Of OtherListDTO)

    'Danh mục trình độ ngoại ngữ :  
    Public Property GET_LANGUAGE_LEVEL As Boolean
    Public Property LIST_LANGUAGE_LEVEL As List(Of OtherListDTO)

    'Danh sách Nation
    Public Property GET_NATION As Boolean
    Public Property LIST_NATION As List(Of NationDTO)

    'Danh sách Province
    Public Property GET_PROVINCE As Boolean
    Public Property LIST_PROVINCE As List(Of ProvinceDTO)

    Public Property GET_WARD As Boolean
    Public Property LIST_WARD As List(Of Ward_DTO)

    'Danh sách lý do tuyển dụng
    Public Property GET_RECRUIMENT_REASON As Boolean
    Public Property LIST_RECRUIMENT_REASON As List(Of OtherListDTO)

    'Danh sách loại sức khỏe
    Public Property GET_HEALTH_TYPE As Boolean
    Public Property LIST_HEALTH_TYPE As List(Of OtherListDTO)

    'Danh sách District
    Public Property GET_DISTRICT As Boolean
    Public Property LIST_DISTRICT As List(Of DistrictDTO)

    'Danh sách OtherList - ALLOWANCE
    Public Property GET_ALLOWANCE_DETAIL As Boolean
    Public Property LIST_ALLOWANCE_DETAIL As List(Of OtherListDTO)

    'Danh sách OtherList - ASSET_GROUP
    Public Property GET_ASSET_GROUP As Boolean
    Public Property LIST_ASSET_GROUP As List(Of OtherListDTO)

    'Danh sách OtherList - ORG_LEVEL
    Public Property GET_ORG_LEVEL As Boolean
    Public Property LIST_ORG_LEVEL As List(Of OtherListDTO)

    'Danh sách OtherList - ASSET_STATUS
    Public Property GET_ASSET_STATUS As Boolean
    Public Property LIST_ASSET_STATUS As List(Of OtherListDTO)

    'Danh sách  ASSET
    Public Property GET_ASSET As Boolean
    Public Property LIST_ASSET As List(Of AssetDTO)

    'Danh sách OtherList - UNIT
    Public Property GET_UNIT As Boolean
    Public Property LIST_UNIT As List(Of OtherListDTO)

    'Danh sách ContractType
    Public Property GET_CONTRACTTYPE_POPUP As Boolean
    Public Property LIST_CONTRACTTYPE_POPUP As List(Of ContractTypeDTO)

    'Danh sách ContractType
    Public Property GET_CONTRACTTYPE As Boolean
    Public Property LIST_CONTRACTTYPE As List(Of ContractTypeDTO)

    'Danh sách OtherList - tình trạng hợp đồng
    Public Property GET_CONTRACT_STATUS As Boolean
    Public Property LIST_CONTRACT_STATUS As List(Of OtherListDTO)

    'danh sách trạng thái quyết định
    Public Property GET_DECISION_STATUS As Boolean
    Public Property LIST_DECISION_STATUS As List(Of OtherListDTO)

    'Danh sách Welfare
    Public Property GET_WELFARE As Boolean
    Public Property LIST_WELFARE As List(Of WelfareListDTO)

    'Danh sách Welfare
    Public Property GET_WELFARE_AUTO As Boolean
    Public Property LIST_WELFARE_AUTO As List(Of WelfareListDTO)

    'Danh sách OtherList - TRANSFER_REASON
    Public Property GET_TRANSFER_REASON As Boolean
    Public Property LIST_TRANSFER_REASON As List(Of OtherListDTO)

    'Danh sách OtherList - DECISION_TYPE
    Public Property GET_DECISION_TYPE As Boolean
    Public Property LIST_DECISION_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_OBJ
    Public Property GET_DISCIPLINE_OBJ As Boolean
    Public Property LIST_DISCIPLINE_OBJ As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_TYPE
    Public Property GET_DISCIPLINE_TYPE As Boolean
    Public Property LIST_DISCIPLINE_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_REASON
    Public Property GET_DISCIPLINE_REASON As Boolean
    Public Property LIST_DISCIPLINE_REASON As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_LEVEL
    Public Property GET_DISCIPLINE_LEVEL As Boolean
    Public Property LIST_DISCIPLINE_LEVEL As List(Of OtherListDTO)

    'Danh sách chế độ sữa - MILK_MODE
    Public Property GET_MILK_MODE As Boolean
    Public Property LIST_MILK_MODE As List(Of OtherListDTO)

    'Danh sách OtherList - COMMEND_OBJ
    Public Property GET_COMMEND_OBJ As Boolean
    Public Property LIST_COMMEND_OBJ As List(Of OtherListDTO)

    'Danh sách OtherList - COMMEND_TYPE
    Public Property GET_COMMEND_TYPE As Boolean
    Public Property LIST_COMMEND_TYPE As List(Of OtherListDTO)

    'Danh sach loai khen thuong
    Public Property GET_COMMEND_LIST As Boolean
    Public Property LIST_COMMEND_LIST As List(Of CommendListDTO)

    'Danh sách OtherList - COMMEND_LEVEL
    Public Property GET_COMMEND_LEVEL As Boolean
    Public Property LIST_COMMEND_LEVEL As List(Of OtherListDTO)

    'Danh sách OtherList - TER_TYPE
    Public Property GET_TER_TYPE As Boolean
    Public Property LIST_TER_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - TER_REASON
    Public Property GET_TER_REASON As Boolean
    Public Property LIST_TER_REASON As List(Of OtherListDTO)

    'Danh sách OtherList - TER_REASON
    Public Property GET_TYPE_NGHI As Boolean
    Public Property LIST_TYPE_NGHI As List(Of OtherListDTO)

    'Danh sách OtherList - TER_STATUS
    Public Property GET_TER_STATUS As Boolean
    Public Property LIST_TER_STATUS As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_STATUS
    Public Property GET_DISCIPLINE_STATUS As Boolean
    Public Property LIST_DISCIPLINE_STATUS As List(Of OtherListDTO)

    'Danh sách OtherList - COMMEND_STATUS
    Public Property GET_COMMEND_STATUS As Boolean
    Public Property LIST_COMMEND_STATUS As List(Of OtherListDTO)

    'Danh sách OtherList - RELATION
    Public Property GET_RELATION As Boolean
    Public Property LIST_RELATION As List(Of RelationshipListDTO)

    'OtherList - TRANSFERTYPE = NEW_HIRE
    Public Property GET_TRANSFER_TYPE_NEW_HIRE As Boolean
    Public Property LIST_TRANSFER_TYPE_NEW_HIRE As OtherListDTO

    'OtherList - TRANSFERSTATUS = Phê duyệt.
    Public Property GET_TRANSFER_STATUS_APPROVE As Boolean
    Public Property LIST_TRANSFER_STATUS_APPROVE As OtherListDTO

    'OtherList - HU_CCQD = Căn cứ quyết định.
    Public Property GET_DECISION_REMARK As Boolean
    Public Property LIST_DECISION_REMARK As List(Of OtherListDTO)

    'OtherList - ATTATCH_TYPE = Loại file đính kèm.
    Public Property GET_ATTATCH_TYPE As Boolean
    Public Property LIST_ATTATCH_TYPE As List(Of OtherListDTO)

    'OtherList - TRAINING_FORM = Hinh thuc dao tao
    Public Property GET_TRAINING_FORM As Boolean
    Public Property LIST_TRAINING_FORM As List(Of OtherListDTO)

    'OtherList - TRAINING_TYPE = Loai hinh dao tao
    Public Property GET_TRAINING_TYPE As Boolean
    Public Property LIST_TRAINING_TYPE As List(Of OtherListDTO)

    'OtherList - LEARNING_LEVEL = Trình độ học vấn
    Public Property GET_LEARNING_LEVEL As Boolean
    Public Property LIST_LEARNING_LEVEL As List(Of OtherListDTO)

    'OtherList - MARK_EDU = Xếp loại học vấn
    Public Property GET_MARK_EDU As Boolean
    Public Property LIST_MARK_EDU As List(Of OtherListDTO)

    ''' <summary>
    ''' WORK_STATUS - 59 - Trạng thái nhân viên
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GET_WORK_STATUS As Boolean
    Public Property LIST_WORK_STATUS As List(Of OtherListDTO)

    ''' <summary>
    ''' WORK_STATUS - 160 - Cấp chức danh
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GET_TITLE_LEVEL As Boolean
    Public Property LIST_TITLE_LEVEL As List(Of OtherListDTO)
    'Danh sách ki danh giá
    Public Property GET_EVALUATE As Boolean
    Public Property LIST_EVALUATE As List(Of OtherListDTO)
    'Danh sách XEP LOAI
    Public Property GET_RANK As Boolean
    Public Property LIST_RANK As List(Of OtherListDTO)
    'Danh sách NANG LUC
    Public Property GET_CAPACITY As Boolean
    Public Property LIST_CAPACITY As List(Of OtherListDTO)

    'LĨNH VỰC ĐÀO TẠO
    Public Property GET_FIELD_TRAIN As Boolean
    Public Property LIST_FIELD_TRAIN As List(Of OtherListDTO)
    'CHUYEN NGANH
    Public Property GET_MAJOR_TRAIN As Boolean
    Public Property LIST_MAJOR_TRAIN As List(Of OtherListDTO)
    'TRINH DO
    Public Property GET_LEVEL_TRAIN As Boolean
    Public Property LIST_LEVEL_TRAIN As List(Of OtherListDTO)

    'nhom chức danh
    Public Property GET_TITLE_GROUP As Boolean
    Public Property LIST_TITLE_GROUP As List(Of OtherListDTO)

    'dia diem ky hop dong hu_location
    Public Property GET_LOCATION As Boolean
    Public Property LIST_LOCATION As List(Of LocationDTO)

    'OtherList - Loai quyet dinh cua Nghi viec
    Public Property GET_TER_DECISION_TYPE As Boolean
    Public Property LIST_TER_DECISION_TYPE As List(Of OtherListDTO)

    'OtherList - moi quan he
    Public Property GET_RELATE_OWNER As Boolean
    Public Property LIST_RELATE_OWNER As List(Of OtherListDTO)
    'OtherList - dan toc
    Public Property GET_NATIVE As Boolean
    Public Property LIST_NATIVE As List(Of OtherListDTO)
    'vung mien
    Public Property GET_ORG_REG As Boolean
    Public Property LIST_ORG_REG As List(Of OtherListDTO)
End Class
