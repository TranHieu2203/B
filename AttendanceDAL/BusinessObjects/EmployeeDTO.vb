Public Class EmployeeDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property FIRST_NAME_VN As String
    Public Property LAST_NAME_VN As String
    Public Property FULLNAME_VN As String
    Public Property FIRST_NAME_EN As String
    Public Property LAST_NAME_EN As String
    Public Property FULLNAME_EN As String
    Public Property ORG_ID As Decimal
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_ABBR As String
    Public Property WORK_STATUS As Decimal?
    Public Property WORK_STATUS_NAME As String
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property IMAGE As String                 'Ảnh đại diện ( Tên ảnh thôi ghi trong database)
    Public Property IMAGE_BINARY As Byte()          'Binary của Ảnh đại diện (Dùng service đọc ảnh ra binary)
    'Contract
    Public Property CONTRACT_ID As Decimal?             'Hop dong dang hieu luc
    Public Property CONTRACT_TYPE_ID As Decimal?        'Loại hợp đồng.
    Public Property CONTRACT_TYPE_NAME As String
    Public Property CONTRACT_NO As String
    Public Property CONTRACT_EFFECT_DATE As Date?    'Ngày hiệu lực của hợp đồng hiện tại
    Public Property CONTRACT_EXPIRE_DATE As Date?    'Ngày hết hiệu lực của hợp đồng hiện tại

    Public Property TITLE_ID As Decimal                'Chức danh
    Public Property TITLE_NAME_VN As String
    Public Property TITLE_NAME_EN As String

    Public Property LAST_WORKING_ID As Decimal?

    Public Property JOIN_DATE As Date?            'Lấy theo hợp đồng đầu tiên
    Public Property DIRECT_MANAGER As Decimal?
    Public Property DIRECT_MANAGER_NAME As String     'Tên Quản lý trực tiếp
    Public Property TER_EFFECT_DATE As Date?             'Ngày hiệu lực của quyết định nghỉ việc.
    Public Property TER_LAST_DATE As Date?               'Ngày làm việc cuối cùng

    Public Property EMPLOYEE_PREVIOUS As String          'Mã nhân viên kề trước
    Public Property EMPLOYEE_NEXT As String              'Mã nhân viên kề sau
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class
