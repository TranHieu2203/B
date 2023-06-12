Public Class Contant_OtherList_Attendance
    'Table danh mục đã được định nghĩa
    Public Const TYPE_NB As String = "TYPE_NB" '  loại nghỉ bù
    Public Const OT_OTHER_LIST As String = "OT_OTHER_LIST" ' Danh mục dùng chung
    Public Const AT_SIGN As String = "AT_SIGN" ' Danh mục ký hiệu công
    Public Const AT_GSIGN As String = "AT_GSIGN" ' Nhóm ký hiệu công
    Public Const SHIFT_GROUP As String = "SHIFT_GROUP" ' Thiết lập ca theo nhóm
    Public Const AT_PERIOD As String = "AT_PERIOD" ' Danh sách kỳ công
    Public Const AT_ARREAR_PERIOD As String = "AT_ARREAR_PERIOD" ' Danh sách truy thu ngày công
    Public Const AT_HOLIDAY_NB As String = "AT_HOLIDAY_NB" ' 
    'CODE OT_OTHER_LIST
    Public Const TIME_SHIFT As String = "TIME_SHIFT" ' Thời gian làm việc, cả ngày, nữa ngày
    Public Const AT_SIGN_TYPE As String = "AT_SIGN_TYPE" ' Kiểu dữ liệu
    Public Const AT_SIGN_MODE As String = "AT_SIGN_MODE" ' Loại dữ liệu
    Public Const GROUP_WORK As String = "GROUP_WORK" ' Nhóm làm việc
    Public Const DISPLAY_STYLE As String = "DISPLAY_STYLE" ' Dạng hiển thị
    'Key constant
    Public Const TYPE_LEAVE As String = "TYPE_LEAVE" ' Danh mục loại nghỉ nghỉ sáng, chiều, cả ngày 
    Public Const TYPE_LEAVE1 As String = "TYPE_LEAVE1" ' Buổi sáng
    Public Const TYPE_LEAVE2 As String = "TYPE_LEAVE2" ' Buổi chiều
    Public Const TYPE_LEAVE3 As String = "TYPE_LEAVE3" ' Cả ngày
    Public Const DAY As String = "DAY" ' Key mặc định hiển thị lưới theo ngày 
    Public Const SIGN_CODE As String = "SIGN_CODE" ' Key mặc định hiển thị lưới theo ký hiệu công
    Public Const COL_IMPORT_OT As Integer = 15 ' Tổng các cột file import OT 
    Public Const COL_IMPORT_NB As Integer = 5 ' Tổng các cột file import NB
    Public Const COL_IMPORT_NB_REMAIN As Integer = 5 ' Tổng các cột file import NB

    Public Const LEAVE As String = "LEAVE" ' Nhóm nghỉ
    Public Const WLEO As String = "WLEO" ' Nhóm đi trễ về sớm
    Public Const SHIFT As String = "SHIFT" ' Nhóm ca
    Public Const OVERTIME As String = "OVERTIME" ' Nhóm OT
    Public Const SIGNID_OT As Integer = 56 ' ID ký hiệu OT
    Public Const SIGNNAME_OT As String = "OT" ' Name ký hiệu OT
    'List key màn hình chức năng công
    Public Const ctrlLeaveSummary As String = "ctrlLeaveSummary" ' Quản lý nghỉ
    Public Const ctrlShiftSummary As String = "ctrlShiftSummary" ' Quản lý ca
    Public Const ctrlWLEOSummary As String = "ctrlWLEOSummary" ' Quản lý đi trễ về sớm
    Public Const ctrlOvertimeSummary As String = "ctrlOvertimeSummary" ' Quản lý đi trễ về sớm
    Public Const ctrlTimesheetSummary_New As String = "ctrlTimesheetSummary_New" ' Tổng hợp  công
    Public Const ctrlTimesheetSummary As String = "ctrlTimesheetSummary" ' Tổng hợp  công
    Public Const ARREARS As String = "ARREARS" ' Loai truy thu/ truy lĩnh
End Class
