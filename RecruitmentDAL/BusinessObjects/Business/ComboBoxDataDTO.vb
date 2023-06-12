Public Class ComboBoxDataDTO

    'Trạng thái kiêm nghiệm
    Public Property GET_STATUS_CONCURRENTLY As Boolean
    Public Property LIST_STATUS_CONCURRENTLY As List(Of OtherListDTO)

    'Hình thức tuyển dụng
    Public Property GET_RECRUITMENT As Boolean
    Public Property LIST_RECRUITMENT As List(Of OtherListDTO)

    'Danh mục danh hieu khen thuong
    Public Property GET_TROPHIES_COMMEND As Boolean
    Public Property LIST_TROPHIES_COMEND As List(Of OtherListDTO)

    'Danh mục hình thức thanh toán khen thưởng
    Public Property GET_PAYMENT_COMEND As Boolean
    Public Property LIST_PAYMENT_COMEND As List(Of OtherListDTO)

    'Danh mục loại quyết đinh
    Public Property GET_DIS_TYPE As Boolean
    Public Property LIST_DIS_TYPE As List(Of OtherListDTO)
    'Danh mục trạng thái
    Public Property GET_DEVICES_STATUS As Boolean
    Public Property LIST_DEVICES_STATUS As List(Of OtherListDTO)

    'Danh mục thiết bị
    Public Property GET_DEVICES_TYPE As Boolean
    Public Property LIST_DEVICES_TYPE As List(Of OtherListDTO)

    'Danh mục loại phụ cấp
    Public Property GET_ALLOWTYPE As Boolean
    Public Property LIST_ALLOWTYPE As List(Of OtherListDTO)


    'Danh mục phụ cấp kiêm nhiệm
    Public Property GET_CON_ALLOW As Boolean
    Public Property LIST_CON_ALLOW As List(Of OtherListDTO)

    'Danh mục biểu thuế
    Public Property GET_TAX_TABLE As Boolean
    Public Property LIST_TAX_TABLE As List(Of OtherListDTO)

    'Danh sách loại cấp chức danh
    Public Property GET_TITLE_TYPE As Boolean
    Public Property LIST_TITLE_TYPE As List(Of OtherListDTO)

    'Danh sách giới tính
    Public Property GET_GENDER As Boolean
    Public Property LIST_GENDER As List(Of OtherListDTO)

    'Danh sách nhóm va chạm
    Public Property GET_GIMPACT As Boolean
    Public Property LIST_GIMPACT As List(Of OtherListDTO)

    'Danh sách phương tiện
    Public Property GET_MEANS As Boolean
    Public Property LIST_MEANS As List(Of OtherListDTO)

    'Danh sách loại tiền
    Public Property GET_MONEYTYPE As Boolean
    Public Property LIST_MONEYTYPE As List(Of OtherListDTO)

    'Danh sách danh mục
    Public Property GET_LIST As Boolean
    Public Property LIST_LIST As List(Of OtherListDTO)

    'Danh sách loại
    Public Property GET_TYPE As Boolean
    Public Property LIST_TYPE As List(Of OtherListDTO)

    'Danh sách nhóm
    Public Property GET_GROUP As Boolean
    Public Property LIST_GROUP As List(Of OtherListDTO)

    'Danh sách Trạng thái bồi thường
    Public Property GET_STATUS_INDEMNITY As Boolean
    Public Property LIST_STATUS_INDEMNITY As List(Of OtherListDTO)

    'Danh sách trạng thái hỗ trợ
    Public Property GET_STATUS_SUPPORT As Boolean
    Public Property LIST_STATUS_SUPPORT As List(Of OtherListDTO)

    'Danh sách mức độ hỗ trợ
    Public Property GET_LEVEL_SUPPORT As Boolean
    Public Property LIST_LEVEL_SUPPORT As List(Of OtherListDTO)

    'Danh mục dân tộc
    Public Property GET_NATIVE As Boolean
    Public Property LIST_NATIVE As List(Of OtherListDTO)

    'Danh mục tôn giáo
    Public Property GET_RELIGION As Boolean
    Public Property LIST_RELIGION As List(Of OtherListDTO)

    'Danh mục tình trạng gia đình(hôn nhân)
    Public Property GET_FAMILY_STATUS As Boolean
    Public Property LIST_FAMILY_STATUS As List(Of OtherListDTO)

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

    'Danh sách loại sức khỏe
    Public Property GET_HEALTH_TYPE As Boolean
    Public Property LIST_HEALTH_TYPE As List(Of OtherListDTO)

    'Danh sách District
    Public Property GET_DISTRICT As Boolean
    Public Property LIST_DISTRICT As List(Of DistrictDTO)

    'Danh sách OtherList - ALLOWANCE
    Public Property GET_ALLOWANCE_DETAIL As Boolean
    Public Property LIST_ALLOWANCE_DETAIL As List(Of OtherListDTO)

    'Danh sách OtherList - ORG_LEVEL
    Public Property GET_ORG_LEVEL As Boolean
    Public Property LIST_ORG_LEVEL As List(Of OtherListDTO)

    'Danh sách loại hợp đồng - ThanhNT 10072015
    Public Property GET_CONTRACTYPE As Boolean
    Public Property LIST_CONTRACTYPE As List(Of OtherListDTO)

    'Danh sách OtherList - tình trạng hợp đồng
    Public Property GET_CONTRACT_STATUS As Boolean
    Public Property LIST_CONTRACT_STATUS As List(Of OtherListDTO)

    'Danh sách OtherList - DECISION_STATUS
    Public Property GET_DECISION_STATUS As Boolean
    Public Property LIST_DECISION_STATUS As List(Of OtherListDTO)

    'Danh sách OtherList - DECISION_TYPE
    Public Property GET_DECISION_TYPE As Boolean
    Public Property LIST_DECISION_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_OBJ
    Public Property GET_DISCIPLINE_OBJ As Boolean
    Public Property LIST_DISCIPLINE_OBJ As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_TYPE
    Public Property GET_DISCIPLINE_TYPE As Boolean
    Public Property LIST_DISCIPLINE_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - DISCIPLINE_LEVEL
    Public Property GET_DISCIPLINE_LEVEL As Boolean
    Public Property LIST_DISCIPLINE_LEVEL As List(Of OtherListDTO)

    'Danh sách OtherList - COMMEND_OBJ
    Public Property GET_COMMEND_OBJ As Boolean
    Public Property LIST_COMMEND_OBJ As List(Of OtherListDTO)

    'Danh sách OtherList - COMMEND_TYPE
    Public Property GET_COMMEND_TYPE As Boolean
    Public Property LIST_COMMEND_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - COMMEND_LEVEL
    Public Property GET_COMMEND_LEVEL As Boolean
    Public Property LIST_COMMEND_LEVEL As List(Of OtherListDTO)

    'Danh sách OtherList - TER_TYPE
    Public Property GET_TER_TYPE As Boolean
    Public Property LIST_TER_TYPE As List(Of OtherListDTO)

    'Danh sách OtherList - TER_REASON
    Public Property GET_TER_REASON As Boolean
    Public Property LIST_TER_REASON As List(Of OtherListDTO)

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
    Public Property LIST_RELATION As List(Of OtherListDTO)

    'OtherList - ATTATCH_TYPE = Loại file đính kèm.
    Public Property GET_ATTATCH_TYPE As Boolean
    Public Property LIST_ATTATCH_TYPE As List(Of OtherListDTO)

    'OtherList - TRAINING_FORM = Hinh thuc dao tao
    Public Property GET_TRAINING_FORM As Boolean
    Public Property LIST_TRAINING_FORM As List(Of OtherListDTO)

    'OtherList - LEARNING_LEVEL = Trình độ học vấn
    Public Property GET_LEARNING_LEVEL As Boolean
    Public Property LIST_LEARNING_LEVEL As List(Of OtherListDTO)

    'OtherList - MARK_EDU = Xếp loại học vấn
    Public Property GET_MARK_EDU As Boolean
    Public Property LIST_MARK_EDU As List(Of OtherListDTO)

    'OtherList - SourceOfRec = Nguồn tuyển dụng
    Public Property GET_SOURCE_REC As Boolean
    Public Property LIST_SOURCE_REC As List(Of OtherListDTO)

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

    'Danh sách ngân hàng
    Public Property GET_BANK As Boolean
    Public Property LIST_BANK As List(Of BankListDTO)

    'Danh sách chi nhánh ngân hàng
    Public Property GET_BANK_BRACH As Boolean
    Public Property LIST_BANK_BRACH As List(Of BankBranchDTO)

    'Danh sách lĩnh vực kinh doanh
    Public Property GET_BUSINESS As Boolean
    Public Property LIST_BUSINESS As List(Of OtherListDTO)

    'Danh sách loại hình doanh nghiệp
    Public Property GET_BUSINESSTYPE As Boolean
    Public Property LIST_BUSINESSTYPE As List(Of OtherListDTO)

    'Danh sách loại vi phạm
    Public Property GET_VIOLATIONTYPE As Boolean
    Public Property LIST_VIOLATIONTYPE As List(Of OtherListDTO)

    'Danh sách loại đồng phục
    Public Property GET_UNIFORMTYPE As Boolean
    Public Property LIST_UNIFORMTYPE As List(Of OtherListDTO)

    'Danh sách trạng thái cấp phát đồng phục
    Public Property GET_ALLOCATESTATUS As Boolean
    Public Property LIST_ALLOCATESTATUS As List(Of OtherListDTO)

    'Danh sách đơn vị cung cấp
    Public Property GET_SUPPLYUNIT As Boolean
    Public Property LIST_SUPPLYUNIT As List(Of OtherListDTO)

    'Danh sách hình thức trả lương
    Public Property GET_PAYMENT_TYPE As Boolean
    Public Property LIST_PAYMENT_TYPE As List(Of OtherListDTO)

    'Danh sách vùng
    Public Property GET_AREA As Boolean
    Public Property LIST_AREA As List(Of OtherListDTO)

    'Danh sách đơn giá hệ số lương
    Public Property GET_SALARY_COEFFICIENT_PRICE As Boolean
    Public Property LIST_SALARY_COEFFICIENT_PRICE As List(Of OtherListDTO)

    'Trung tâm chi phí
    Public Property GET_COST_CENTER As Boolean
    Public Property LIST_COST_CENTER As List(Of CostCenterDTO)

    'Nhóm cấp bậc
    Public Property GET_GROUP_LEVEL As Boolean
    Public Property LIST_GROUP_LEVEL As List(Of OtherListDTO)

    'Nhóm lương
    Public Property GET_GROUP_SALARY As Boolean
    Public Property LIST_GROUP_SALARY As List(Of OtherListDTO)

    'Loại tuyển dụng
    Public Property GET_RECRUITMENT_TYPE As Boolean
    Public Property LIST_RECRUITMENT_TYPE As List(Of OtherListDTO)

    'đợt tuyển dụng
    Public Property GET_STAGE As Boolean
    Public Property LIST_STAGE As List(Of StageDTO)

    'Loại hợp đồng
    Public Property GET_CONTRACTTYPE As Boolean
    Public Property LIST_CONTRACTTYPE As List(Of OtherListDTO)
End Class
