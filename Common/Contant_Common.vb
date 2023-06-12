Public Class Contant_Common

    'Bảo hiểm
    Public Const SALARY_MIN As Int32 = 1210000 'Mức lương BH tối thiểu chung
    Public Const SALARY_MIN_APPLY_DATE As String = "01/05/2016" 'Mức lương BH tối thiểu chung
    Public Const NGAY_CONG_CHUAN_BH As Int32 = 24 'Ngày công chuẩn BH
    Public Const NGAY_CONG_CHUAN_BH_APPLY_DATE As String = "01/01/2016" 'Mức lương BH tối thiểu chung

    'Danh sách các loại hưởng chế đố theo công thức
    Public Const Ban_Than_Om_Dai_Ngay As String = "Bản thân ốm dài ngày"
    Public Const Sinh_Nuoi_Con As String = "Sinh con, nuôi con"
    Public Const Huu_Tri As String = "Hưu trí"
    Public Const Tu_Tuat As String = "Tử tuất"
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ' Key
    Public Const AWAITING_APPROVAL As String = "2" ' Chờ phê duyệt
    Public Const APPROVAL As String = "1" ' Đã phê duyệt
    Public Const NOT_APPROVAL As String = "0" ' Không phê duyệt


    Public Const INFO_SALARY As String = "Bấm nút [Thông tin lương] để chọn lương cho nhân viên" '
    Public Const DISCIPLINE_OBJECT1 As String = "DISCIPLINE_OBJECT1" 'Đối tượng kỷ luật cá nhân
    Public Const DISCIPLINE_OBJECT2 As String = "DISCIPLINE_OBJECT2" 'Đối tượng kỷ luật tập thể
    Public Const COMMEND_OBJECT1 As String = "COMMEND_OBJECT1" 'Đối tượng khen thưởng cá nhân
    Public Const COMMEND_OBJECT2 As String = "COMMEND_OBJECT2" 'Đối tượng khen thưởng tập thể
    Public Const ID_VIETNAM As Integer = 244
    Public Const NAME_VIETNAM As String = "Việt Nam"
    Public Const ID_NATIVE_KINH As String = "NATIVE_1"
    Public Const NAME_NATIVE_KINH As String = "Kinh"
    Public Const HDKXD As Integer = 4 'Hợp đồng không xác định thời hạn

    ' Key form đánh giá hợp đồng - màn hình khai báo hợp đồng
    Public Const DAT As Integer = 0 ' Đạt
    Public Const KHONGDAT As Integer = 1 '  Không đạt
    Public Const LOAIHD As Integer = 2 ' Chuyển loại hđ

    'Other List
    Public Const ACTION_TYPE As String = "ACTION_TYPE" 'Loại thao tác: SAVE, EDIT, DELETE...
    Public Const SYSTEM_LANGUAGE As String = "SYSTEM_LANGUAGE" 'Ngôn ngữ hệ thống: Tiếng Việt, English, ...
    Public Const APPROVE_STATUS As String = "APPROVE_STATUS" 'Trạng thái, phê duyệt, chờ phê duyệt
    Public Const APPROVE_STATUS2 As String = "APPROVE_STATUS2" 'Trạng thái, phê duyệt
    Public Const RANK As String = "RANK" 'Xếp loại: Xuất sắc, Giỏi, Khá, Trung bình,....
    Public Const VS_TYPE_NONE As String = "VS_TYPE_NONE" 'Loại value set kiểu control
    Public Const VALUESET_TYPE As String = "VALUESET_TYPE" 'Loại value set: Câu truy vấn, Loại xác định, Loại tự xác định
    Public Const REQUEST_STATUS As String = "REQUEST_STATUS" 'Loại value set: Đang khởi tạo, Chờ xử lý, Đang xử lý,...
    Public Const DATA_TYPE As String = "DATA_TYPE" 'Kiểu dữ liệu: Kiểu số, Kiểu chuỗi, Kiểu ngày giờ, ...
    Public Const TEMPLATE_TYPE_IN As String = "TEMPLATE_TYPE_IN" 'Mẫu file đầu vào: Excel, Word,...
    Public Const TEMPLATE_TYPE_OUT As String = "TEMPLATE_TYPE_OUT" 'Mẫu file đầu ra: Excel, Word,...
    Public Const ATTATCH_TYPE As String = "ATTATCH_TYPE" 'Loại tập tin đính kèm: Excel, Word,...
    Public Const PROCESS_TYPE As String = "PROCESS_TYPE" 'Loại xử lý: Nhập liệu, Xuất dữ liệu, Xử lý, Báo cáo, ...
    Public Const FORM_TYPE As String = "FORM_TYPE" 'Danh mục biểu mẫu quyết định: Quyết định bổ nhiệm, Quyết định miễn nhiệm, Quyết định điều động,...
    Public Const FORM_CONTRACT As String = "FORM_CONTRACT" 'Danh mục biểu mẫu hợp đồng: Quyết định bổ nhiệm, Quyết định miễn nhiệm, Quyết định điều động,...
    Public Const PRIORITY As String = "PRIORITY" 'Độ ưu tiên: 1,2,3...
    Public Const DAY_TYPE As String = "DAY_TYPE" 'Buổi trong ngày: Sáng, Chiều, Cả Ngày,....
    Public Const CALCULATION As String = "CALCULATION" 'Toán tử: Cộng, Trừ, Nhân, Chia,....
    Public Const CURRENCY As String = "CURRENCY" 'Đơn vị tiền tệ: VNĐ, USD....
    Public Const RECRUITMENT_PRO_STATUS As String = "RECRUITMENT_PRO_STATUS" 'Trạng thái tuyển dụng

    ' Code thong ke
    Public Const EMP_COUNT As String = "EMP_COUNT" 'Tong so nhan vien hien tai
    Public Const EMP_NEW As String = "EMP_NEW" 'Tong so nhan vien tuyen moi trong thang
    Public Const EMP_TER As String = "EMP_TER" 'Tong so nhan vien nghi viec trong thang
    Public Const CONTRACT_NEW As String = "CONTRACT_NEW" 'Hop dong tao moi trong thang
    Public Const AGE_AVG As String = "AGE_AVG" 'Tuoi binh quan
    Public Const SENIORITY_AVG As String = "SENIORITY_AVG" 'Tham nien binh quan

    ' Code loai nhac nho
    Public Const REMINDER1 As String = "001" 'Hết hạn thử việc
    Public Const REMINDER2 As String = "002" 'Hết hạn HĐLĐ
    Public Const REMINDER3 As String = "003" 'Ngày sinh nhật
    Public Const REMINDER4 As String = "004" 'Hết hạn Visa
    Public Const REMINDER5 As String = "005" 'Hết hạn Hộ chiếu
    Public Const REMINDER6 As String = "006" 'Đến hạn nâng lương
    Public Const REMINDER7 As String = "007" 'Đến tuổi về hưu
    Public Const REMINDER8 As String = "008" 'Hết hạn bổ nhiệm
    Public Const REMINDER9 As String = "009" 'Hết hạn đình chỉ công tác
    Public Const REMINDER10 As String = "010" 'Hết hạn số sổ lao động
    Public Const REMINDER11 As String = "011" 'Hết hạn điều động
    Public Const REMINDER12 As String = "012" 'Hết hạn thử thách nâng ngạch/chuyển ngạch
    Public Const REMINDER13 As String = "013" 'Kiêm nhiệm có thời hạn
    Public Const REMINDER14 As String = "014" 'Thôi hưởng phụ cấp (PC theo thời hạn)
    Public Const REMINDER15 As String = "015" 'Hết hạn nghỉ thai sản
    Public Const REMINDER16 As String = "016" 'Nghỉ ko lương dài hạn
End Class
