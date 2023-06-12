Imports CommonDAL
Imports Framework.Data
Imports Framework.Data.SystemConfig

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace CommonBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface ICommonBusiness
        <OperationContract()>
        Function SE_RESET_USER(ByVal UserIDs As String) As Boolean
        <OperationContract()>
        Function GetUserPermisionPortal(ByVal userName As String, ByVal employeeID As Decimal) As List(Of PermissionDTO)
        <OperationContract()>
        Function GetUserPermisionExceptionPortal(ByVal userName As String, ByVal fid As String, ByVal employeeID As Decimal) As List(Of PermissionDTO)
        <OperationContract()>
        Function CheckOtherListExistInDatabase(ByVal lstID As List(Of Decimal), ByVal typeID As Decimal) As Boolean

        <OperationContract()>
        Function GetATOrgPeriod(ByVal periodID As Decimal) As DataTable
        <OperationContract()>
        Function GetPositionToPopupFind(ByVal _filter As PositionPopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "ID asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of PositionPopupFindListDTO)

#Region "Group Organization"
        <OperationContract()>
        Function GetGroupOrganization(ByVal _groupID As Decimal) As List(Of Decimal)

        <OperationContract()>
        Function GetGroupOrganizationFunction(ByVal _groupID As Decimal) As List(Of Decimal)

        <OperationContract()>
        Function DeleteGroupOrganization(ByVal _groupId As Decimal)

        <OperationContract()>
        Function UpdateGroupOrganization(ByVal _lstOrg As List(Of GroupOrgAccessDTO)) As Boolean

        <OperationContract()>
        Function UpdateGroupOrganizationFunction(ByVal _lstOrg As List(Of GroupOrgFunAccessDTO)) As Boolean
#End Region
#Region "Case Config"
        <OperationContract()>
        Function GetCaseConfigByID(ByVal codename As String, ByVal codecase As String) As Integer

        <OperationContract()>
        Function GET_VALUE_SE_CONFIG(ByVal P_EMP_CODE As String) As String
#End Region
#Region "Sign"
        <OperationContract()>
        Function GetSignList() As List(Of ATTimeManualDTO)
#End Region

#Region "Check User Login"

        <OperationContract()>
        Function IsUsernameExist(ByVal Username As String) As Boolean

        <OperationContract()>
        Function GetUserWithPermision(ByVal Username As String) As UserDTO

        <OperationContract()>
        Function GetUserPermissions(ByVal Username As String) As List(Of PermissionDTO)

        <OperationContract()>
        Function CheckUserAdmin(ByVal Username As String) As Boolean

        <OperationContract()>
        Function ChangeUserPassword(ByVal Username As String, ByVal _oldpass As String, ByVal _newpass As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetPassword(ByVal Username As String) As String

        <OperationContract()>
        Function GetUserByUserID(ByVal UserID As Decimal) As CommonDAL.SE_USER

        <OperationContract()>
        Function GetUserByUserName(ByVal UserName As String) As CommonDAL.SE_USER

        <OperationContract()>
        Function UpdateUserStatus(ByVal Username As String, ByVal _ACTFLG As String, ByVal log As UserLog) As Boolean
#End Region

#Region "Organization"
        <OperationContract()>
        Function GetOrganizationChildByList(ByVal _username As String,
                                            ByVal _status As String,
                                            ByVal _lstOrgID As List(Of String)) As List(Of Decimal)

        <OperationContract()>
        Function GetOrganizationPermission(ByVal _username As String,
                                            ByVal _lstOrgID As List(Of Decimal)) As List(Of Decimal)

        <OperationContract()>
        Function GetOrganizationChildTextByList(ByVal _username As String,
                                            ByVal _status As String,
                                            ByVal _lstOrgID As List(Of String)) As List(Of String)
        ''' <summary>
        ''' Lấy danh sách chỉ gồm id va name cua phong ban
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOrganizationList() As List(Of OrganizationDTO)
        <OperationContract()>
        Function GetOrganizationLocationTreeView_New(ByVal _username As String) As List(Of OrganizationDTO)
        <OperationContract()>
        Function GetOrganizationAll(ByVal IsOM As Boolean) As List(Of OrganizationDTO)
        ''' <summary>
        ''' Lấy danh sách sơ đồ tổ chức hàng dọc cho TreeView phân quyền
        ''' </summary>
        ''' <param name="_username">tên tài khoản</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOrganizationLocationTreeView(ByVal _username As String, Optional ByVal fid As String = "", Optional ByVal state As String = "") As List(Of OrganizationDTO)
        ''' <summary>
        ''' Lấy thông tin sơ đồ tổ chức Location
        ''' </summary>
        ''' <param name="_orgId">Org ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOrganizationLocationInfo(ByVal _orgId As Decimal) As OrganizationDTO
        ''' <summary>
        ''' Lấy thông tin cấu trúc sơ đồ tổ chức
        ''' </summary>
        ''' <param name="_orgId">Org ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOrganizationStructureInfo(ByVal _orgId As Decimal) As List(Of OrganizationStructureDTO)
        <OperationContract()>
        Function GetListOrgImport(ByVal orgID As List(Of System.Decimal)) As List(Of CommonDAL.OrganizationDTO)
#End Region

#Region "User List"
        ''' <summary>
        ''' Lay danh sach tai khoan
        ''' </summary>
        ''' <returns>danh sach tai khoan</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUser(ByVal _filter As UserDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        <OperationContract()>
        Function GetUserList(ByVal _filter As UserDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        ''' <summary>
        ''' Them tai khoan
        ''' </summary>
        ''' <param name="_user">doi tuong chua cac thong tin can them</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Kiểm tra dữ liệu tài khoản
        ''' </summary>
        ''' <param name="_validate">Dữ liệu cần kiểm tra</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateUser(ByVal _validate As UserDTO) As Boolean
        ''' <summary>
        ''' Sua tai khoan
        ''' </summary>
        ''' <param name="_user">doi tuong chua cac thong tin can sua</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xoa tai khoan
        ''' </summary>
        ''' <param name="_lstUserID">doi tuong chua thong tin can xoa</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteUser(ByVal _lstUserID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Lock or Unlock tài khoản
        ''' </summary>
        ''' <param name="_lstUserID"></param>
        ''' <param name="_status"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateUserListStatus(ByVal _lstUserID As List(Of Decimal), ByVal _status As String, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Đồng bộ hóa
        ''' </summary>
        ''' <param name="_newUser"></param>
        ''' <param name="_modifyUser"></param>
        ''' <param name="_deleteUser"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function SyncUserList(ByRef _newUser As String, ByRef _modifyUser As String, ByRef _deleteUser As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ResetUserPassword(ByVal _userid As List(Of Decimal),
                                      ByVal _minLength As Integer,
                                      ByVal _hasLowerChar As Boolean,
                                      ByVal _hasUpperChar As Boolean,
                                      ByVal _hasNumbericChar As Boolean,
                                      ByVal _hasSpecialChar As Boolean,
                                      ByVal _hasPasswordConfig As Boolean, ByVal _hasPasswordDefault As Boolean, ByVal _hasPasswordDefaultText As String) As Boolean

        ''' <summary>
        ''' Gửi Email thông báo mật khẩu
        ''' </summary>
        ''' <param name="_userid"></param>
        ''' <param name="_subject"></param>
        ''' <param name="_content"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        <OperationContract()>
        Function Get_user_info(ByVal id As Decimal) As List(Of String)
        <OperationContract()>
        Function GET_MAIL_TEMPLATE(ByVal code As String, ByVal group As String) As List(Of String)

        <OperationContract()>
        Function SendMailConfirmUserPassword(ByVal _userid As List(Of Decimal),
                                                ByVal _subject As String,
                                                ByVal _content As String,
                                                Optional ByVal _orderBy As Decimal = 6) As Boolean

        ''' <summary>
        ''' Lấy danh sách user cần send mail từ Group
        ''' </summary>
        ''' <param name="_groupid"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserNeedSendMail(ByVal _groupid As Decimal) As List(Of UserDTO)
        <OperationContract()>
        Function ValidateUserForgetPass(ByVal _userName As String, ByRef uID As Decimal) As String

        <OperationContract()>
        Function checkRulePass(ByVal pass As String, ByVal oldpass As String, ByVal userid As Decimal) As Decimal

        <OperationContract()>
        Function ChangePassByForget(ByVal _userName As String, ByVal _passWord As String) As Boolean

        <OperationContract()>
        Function ForgetPassSendCode(ByVal _email As String, ByVal _code As String) As Boolean
#End Region

#Region "Group List"
        ''' <summary>
        ''' Lấy danh sách nhóm tài khoản cho Combo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetGroupListToComboListBox() As List(Of GroupDTO)

        <OperationContract()>
        Function GetGroupListToComboListBox1() As List(Of GroupDTO)
        ''' <summary>
        ''' Lấy danh sách nhóm tài khoản
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetGroupList(ByVal _filter As GroupDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of GroupDTO)
        ''' <summary>
        ''' Kiểm tra dữ liệu nhóm tài khoản
        ''' </summary>
        ''' <param name="_validate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateGroupList(ByVal _validate As GroupDTO) As Boolean
        ''' <summary>
        ''' Thêm nhóm tài khoản
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertGroup(ByVal lst As GroupDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Sửa nhóm tài khoản
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateGroup(ByVal lst As GroupDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xóa nhóm tài khoản
        ''' </summary>
        ''' <param name="GroupID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteGroup(ByVal GroupID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Đổi trạng thái của Group
        ''' </summary>
        ''' <param name="_lstID"></param>
        ''' <param name="_ACTFLG"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateGroupStatus(ByVal _lstID As List(Of Decimal), ByVal _ACTFLG As String, ByVal log As UserLog) As Boolean
#End Region

#Region "Controls Manage"
        <OperationContract()>
        Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME ASC") As List(Of FunctionDTO)

        <OperationContract()>
        Function Insert_Update_Control_List(ByVal _id As String, ByVal Config As String, ByVal log As UserLog) As Boolean
#End Region
#Region "register user"
        <OperationContract()>
        Function GetRegisterUser(ByVal _filter As RegisterUserDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of RegisterUserDTO)
        <OperationContract()>
        Function UpdateRegisterUser(ByVal _item As List(Of RegisterUserDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function InsertRegisterUser(ByVal _item As RegisterUserDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteRegisterUser(ByVal lst_id As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function check_dup_reguser(ByVal effect_date As Date, Optional ByVal id As Decimal = 0) As Decimal

#End Region
#Region "Function List"
        ''' <summary>
        ''' Lấy danh sách chức năng hệ thống
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetFunctionList(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        ''' <summary>
        ''' Kiểm tra dữ liệu chức năng hệ thống
        ''' </summary>
        ''' <param name="_validate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateFunctionList(ByVal _validate As FunctionDTO) As Boolean
        ''' <summary>
        ''' Cập nhập chức năng hệ thống
        ''' </summary>
        ''' <param name="_item"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateFunctionList(ByVal _item As List(Of FunctionDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function InsertFunctionList(ByVal _item As FunctionDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Lấy danh sách các Module
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetModuleList() As List(Of ModuleDTO)

        <OperationContract()>
        Function ActiveFunctions(ByVal lstFunction As List(Of FunctionDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean
#End Region

#Region "Group User"

        ''' <summary>
        ''' Lấy danh sách các tài khoản trong nhóm
        ''' </summary>
        ''' <param name="GroupID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserListInGroup(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        <OperationContract()>
        Function GetUserListInGroup1(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        ''' <summary>
        ''' Lấy danh sách các tài khoản ngoài nhóm
        ''' </summary>
        ''' <param name="GroupID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserListOutGroup(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        ''' <summary>
        ''' Thêm tài khoản vào nhóm
        ''' </summary>
        ''' <param name="_groupID"></param>
        ''' <param name="_lstUserID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xóa tài khoản khỏi nhóm
        ''' </summary>
        ''' <param name="_groupID"></param>
        ''' <param name="_lstUserID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "Group Function"
        ''' <summary>
        ''' Lấy danh sách chức năng trong nhóm
        ''' </summary>
        ''' <param name="GroupID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetGroupFunctionPermision(ByVal GroupID As Decimal,
                                            ByVal _filter As GroupFunctionDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "FUNCTION_CODE asc") As List(Of GroupFunctionDTO)
        ''' <summary>
        ''' Lấy danh sách chức năng ngoài nhóm
        ''' </summary>
        ''' <param name="GroupID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetGroupFunctionNotPermision(ByVal GroupID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        ''' <summary>
        ''' Thêm chức năng vào nhóm
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertGroupFunction(ByVal lst As List(Of GroupFunctionDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CopyGroupFunction(ByVal groupCopyID As Decimal, ByVal groupID As Decimal, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Cập nhập phân quyền chức năng
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateGroupFunction(ByVal lst As List(Of GroupFunctionDTO), ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xóa chức năng khỏi nhóm
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteGroupFunction(ByVal lst As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetCurMIDByFID(ByVal _fid As String) As String
#End Region

#Region "Group Report"
        ''' <summary>
        ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
        ''' </summary>
        ''' <param name="_groupID">ID nhóm tài khoản</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetGroupReportList(ByVal _groupID As Decimal) As List(Of GroupReportDTO)
        ''' <summary>
        ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
        ''' </summary>
        ''' <param name="_groupID">ID nhóm tài khoản</param>
        ''' <param name="_filter">bộ lọc</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetGroupReportListFilter(ByVal _groupID As Decimal, ByVal _filter As GroupReportDTO) As List(Of GroupReportDTO)
        ''' <summary>
        ''' Cập nhập danh sách Report
        ''' </summary>
        ''' <param name="_groupID">ID nhóm tài khoản</param>
        ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateGroupReport(ByVal _groupID As Decimal, ByVal _lstReport As List(Of GroupReportDTO)) As Boolean
#End Region

#Region "User Function"
        <OperationContract()>
        Function GetUserFunctionPermisionException(ByVal UserID As Decimal,
                                            ByVal _filter As UserFunctionDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "FUNCTION_CODE asc") As List(Of UserFunctionDTO)
        <OperationContract()>
        Function UpdateUserFunctionException(ByVal lst As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteUserFunctionException(ByVal lst As List(Of Decimal)) As Boolean


        ''' <summary>
        ''' Lấy danh sách chức năng trong nhóm
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserFunctionPermision(ByVal UserID As Decimal,
                                            ByVal _filter As UserFunctionDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "FUNCTION_CODE asc") As List(Of UserFunctionDTO)
        ''' <summary>
        ''' Lấy danh sách chức năng ngoài nhóm
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserFunctionNotPermision(ByVal UserID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        ''' <summary>
        ''' Thêm chức năng vào nhóm
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertUserFunction(ByVal lst As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetUserFunctionNotPermisionException(ByVal UserID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        ''' <summary>
        ''' Thêm chức năng vào nhóm
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertUserFunctionException(ByVal lst As List(Of UserFunctionDTO), ByVal lstOrg As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CopyUserFunction(ByVal UserCopyID As Decimal, ByVal UserID As Decimal, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Cập nhập phân quyền chức năng
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateUserFunction(ByVal lst As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xóa chức năng khỏi nhóm
        ''' </summary>
        ''' <param name="lst"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteUserFunction(ByVal lst As List(Of Decimal)) As Boolean
#End Region

#Region "User Organization"
        <OperationContract()>
        Function GetUserOrganization(ByVal UserID As Decimal) As List(Of Decimal)

        <OperationContract()>
        Function DeleteUserOrganization(ByVal _UserId As Decimal)

        <OperationContract()>
        Function UpdateUserOrganization(ByVal OrgIDs As List(Of UserOrgAccessDTO)) As Boolean
#End Region

#Region "User Report"
        ''' <summary>
        ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
        ''' </summary>
        ''' <param name="_UserID">ID nhóm tài khoản</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserReportList(ByVal _UserID As Decimal) As List(Of UserReportDTO)
        ''' <summary>
        ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
        ''' </summary>
        ''' <param name="_UserID">ID nhóm tài khoản</param>
        ''' <param name="_filter">bộ lọc</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetUserReportListFilter(ByVal _UserID As Decimal, ByVal _filter As UserReportDTO) As List(Of UserReportDTO)
        ''' <summary>
        ''' Cập nhập danh sách Report
        ''' </summary>
        ''' <param name="_UserID">ID nhóm tài khoản</param>
        ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateUserReport(ByVal _UserID As Decimal, ByVal _lstReport As List(Of UserReportDTO)) As Boolean
#End Region

#Region "Employee"
        <OperationContract()>
        Function GetEmployeeSignToPopupFind(ByVal _filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        <OperationContract()>
        Function GetEmployeeSignToPopupFind_V2(ByVal _filter As EmployeePopupFindListDTO,
                                               ByVal PageIndex As Integer,
                                               ByVal PageSize As Integer,
                                               ByRef Total As Integer,
                                               Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                               Optional ByVal log As UserLog = Nothing,
                                               Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        <OperationContract()>
        Function GetEmployeeToPopupFind(ByVal _filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        <OperationContract()>
        Function GetEmployeeFind(ByVal _filter As EmployeePopupFindListDTO,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        <OperationContract()>
        Function GetEmployeeToPopupFind2(ByVal _filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            ByVal request_id As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        <OperationContract()>
        Function GetEmployeeToPopupFind_EmployeeID(ByVal _empId As List(Of Decimal)) As List(Of EmployeePopupFindDTO)

        <OperationContract()>
        Function GetEmployeeID(ByVal _empId As Decimal) As EmployeePopupFindDTO
#End Region

#Region "Title"
        ''' <summary>
        ''' Lay danh sach Title
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetTitle(ByVal Filter As TitleDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME_VN asc") As List(Of TitleDTO)
        ''' <summary>
        ''' Lấy thông tin Title từ Id
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetTitleFromId(ByVal _lstIds As List(Of Decimal)) As List(Of TitleDTO)
#End Region

#Region "AccessLog"
        <OperationContract()>
        Function GetAccessLog(ByVal filter As AccessLogFilter,
                              ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer,
                              Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)

        <OperationContract()>
        Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean
#End Region

#Region "ActionLog"
        <OperationContract()>
        Function GetActionLog(ByVal filter As ActionLogFilter,
                              ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer,
                              Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)

        <OperationContract()>
        Function GetActionLogByObjectId(ByVal ObjectId As Decimal) As List(Of ActionLog)

        <OperationContract()>
        Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl)

        <OperationContract()>
        Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer
#End Region

#Region "Get Combo List Data"
        <OperationContract()>
        Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
        <OperationContract()>
        Function GetAllTrCertificateList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetPeriodYear(ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetPeriodByYear(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        <OperationContract()>
        Function GetClassification(ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetLearningLevel(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetHU_CompetencyList(ByVal isBlank As Boolean) As DataTable

#End Region

#Region "System Config"
        ''' <summary>
        ''' Lấy danh sách cấu hình hệ thống
        ''' </summary>
        ''' <returns>Danh sách dạng Dictionary</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)

        ''' <summary>
        ''' Cập nhập giá trị cấu hình hệ thống
        ''' </summary>
        ''' <param name="_lstConfig"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean

#End Region

#Region "SendMail"
        ''' <summary>
        ''' Gọi hàm gửi mail queue trong DB
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function SendMail() As Boolean
        ''' <summary>
        ''' Thêm queue mail
        ''' </summary>
        ''' <param name="_from"></param>
        ''' <param name="_to"></param>
        ''' <param name="_subject"></param>
        ''' <param name="_content"></param>
        ''' <param name="_cc"></param>
        ''' <param name="_bcc"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertMail(ByVal _from As String, ByVal _to As String, ByVal _subject As String, ByVal _content As String, Optional ByVal _cc As String = "", Optional ByVal _bcc As String = "", Optional ByVal _viewName As String = "", Optional ByVal DefaultRunNowIsZero As Boolean = False,
                               Optional ByVal _orderBy As Decimal = 6)
#End Region

#Region "OtherList"
        <OperationContract()>
        Function saveActionLogMB(ByVal userid As Decimal, ByVal objname As String, ByVal computername As String, ByVal viewName As String, ByVal lstpr As List(Of OtherListDTO)) As Decimal
        <OperationContract()>
        Function GetOtherListByTypeToCombo(ByVal sType As String) As List(Of OtherListDTO)
        ''' <summary>
        ''' Lay danh sach OtherList
        ''' </summary>
        ''' <returns>danh sach OtherList</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOtherList(ByVal sACT As String) As List(Of OtherListDTO)
        ''' <summary>
        ''' Lay danh sach OtherListByType
        ''' </summary>
        ''' <returns>danh sach OtherList</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOtherListByType(ByVal gID As Decimal,
                                       ByVal _filter As OtherListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERBYID ASC") As List(Of OtherListDTO)
        ''' <summary>
        ''' Them OtherList
        ''' </summary>
        ''' <param name="objOtherList">doi tuong chua cac thong tin can them</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertOtherList(ByVal objOtherList As OtherListDTO,
                                 ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Sua OtherList
        ''' </summary>
        ''' <param name="objOtherList">doi tuong chua cac thong tin can sua</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyOtherList(ByVal objOtherList As OtherListDTO,
                                 ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateOtherList(ByVal _validate As OtherListDTO) As Boolean


        ''' <summary>
        ''' Xoa OtherList
        ''' </summary>
        ''' <param name="lstOtherList">doi tuong chua thong tin can xoa</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveOtherList(ByVal lstOtherList As List(Of Decimal),
                                 ByVal sActive As String,
                                 ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteOtherList(ByVal lstOtherList As List(Of OtherListDTO)) As Boolean
#End Region

#Region "OtherListGroup"
        ''' <summary>
        ''' Lay danh sach OtherListGroup theo ma phan he
        ''' </summary>
        ''' <returns>danh sach OtherListGroup</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOtherListGroupBySystem(ByVal systemName As String) As List(Of OtherListGroupDTO)

        ''' <summary>
        ''' Lay danh sach OtherListGroup
        ''' </summary>
        ''' <returns>danh sach OtherListGroup</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOtherListGroup(ByVal sACT As String) As List(Of OtherListGroupDTO)
        ''' <summary>
        ''' Them OtherListGroup
        ''' </summary>
        ''' <param name="objOtherListGroup">doi tuong chua cac thong tin can them</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO,
                                 ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Sua OtherListGroup
        ''' </summary>
        ''' <param name="objOtherListGroup">doi tuong chua cac thong tin can sua</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO,
                                 ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xoa OtherListGroup
        ''' </summary>
        ''' <param name="lstOtherListGroup">doi tuong chua thong tin can xoa</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveOtherListGroup(ByVal lstOtherListGroup() As OtherListGroupDTO,
                                 ByVal sActive As String,
                                 ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteOtherListGroup(ByVal lstOtherListGroup As List(Of OtherListGroupDTO)) As Boolean
#End Region

#Region "OtherListType"
        ''' <summary>
        ''' Lay danh sach OtherListType theo phan he
        ''' </summary>
        ''' <returns>danh sach OtherListType</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOtherListTypeSystem(ByVal systemName As String) As List(Of OtherListTypeDTO)
        ''' <summary>
        ''' Lay danh sach OtherListType
        ''' </summary>
        ''' <returns>danh sach OtherListType</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetOtherListType(ByVal sACT As String) As List(Of OtherListTypeDTO)
        ''' <summary>
        ''' Them OtherListType
        ''' </summary>
        ''' <param name="objOtherListType">doi tuong chua cac thong tin can them</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertOtherListType(ByVal objOtherListType As OtherListTypeDTO,
                                 ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Sua OtherListType
        ''' </summary>
        ''' <param name="objOtherListType">doi tuong chua cac thong tin can sua</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyOtherListType(ByVal objOtherListType As OtherListTypeDTO,
                                 ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xoa OtherListType
        ''' </summary>
        ''' <param name="lstOtherListType">doi tuong chua thong tin can xoa</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveOtherListType(ByVal lstOtherListType() As OtherListTypeDTO,
                                 ByVal sActive As String,
                                 ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteOtherListType(ByVal lstOtherListType As List(Of OtherListTypeDTO)) As Boolean
#End Region

#Region "Reminder per user"

        <OperationContract()>
        Function GetReminderConfig(ByVal username As String) As Dictionary(Of Integer, String)
        <OperationContract()>
        Function SetReminderConfig(ByVal username As String, ByVal type As Integer, ByVal value As String) As Boolean

#End Region

#Region "Approve Process"
#Region "BangDV"

#Region "Process Setup - Thiết lập quy trình"

        <OperationContract()>
        Function GetApproveProcessList() As List(Of ApproveProcessDTO)
        <OperationContract()>
        Function GetApproveProcess(ByVal processId As Decimal) As ApproveProcessDTO
        <OperationContract()>
        Function InsertApproveProcess(ByVal item As ApproveProcessDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateApproveProcess(ByVal item As ApproveProcessDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateApproveProcessStatus(ByVal itemUpdates As List(Of Decimal), ByVal status As String) As Boolean
#End Region

#Region "Setup Approve - Thiết lập phê duyệt"
        <OperationContract()>
        Function GetLeavePlanList() As List(Of OtherListDTO)
        <OperationContract()>
        Function GetTitleList() As List(Of OtherListDTO)
        <OperationContract()>
        Function GetApproveSetupByEmployee(ByVal employeeId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        <OperationContract()>
        Function GetApproveSetupByOrg(ByVal orgId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        <OperationContract()>
        Function GetApproveSetup(ByVal id As Decimal) As ApproveSetupDTO
        <OperationContract()>
        Function InsertApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteApproveSetup(ByVal itemDeletes As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function IsExistSetupByDate(ByVal itemCheck As ApproveSetupDTO, Optional ByVal idExclude As Decimal? = Nothing) As Boolean
        <OperationContract()>
        Function GetTitleByRank_ID(ByVal lst_Rank As List(Of OtherListDTO)) As List(Of TitleDTO)
        <OperationContract()>
        Function GetTitleAll() As List(Of TitleDTO)

#End Region

#Region "Approve Template - Thiết lập template phê duyệt"
        <OperationContract()>
        Function InsertApproveTemplate(ByVal item As ApproveTemplateDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateApproveTemplate(ByVal item As ApproveTemplateDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetApproveTemplate(ByVal id As Decimal) As ApproveTemplateDTO
        <OperationContract()>
        Function GetApproveTemplateList() As List(Of ApproveTemplateDTO)
        <OperationContract()>
        Function DeleteApproveTemplate(ByVal itemDeletes As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function IsApproveTemplateUsed(ByVal templateID As Decimal) As Boolean
#End Region

#Region "Approve Template Detail - Thiết lập template chi tiết"

        <OperationContract()>
        Function InsertApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetApproveTemplateDetail(ByVal id As Decimal) As ApproveTemplateDetailDTO
        <OperationContract()>
        Function GetApproveTemplateDetailList(ByVal templateId As Decimal) As List(Of ApproveTemplateDetailDTO)
        <OperationContract()>
        Function DeleteApproveTemplateDetail(ByVal itemDeletes As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckLevelInsert(ByVal level As Decimal, ByVal idExclude As Decimal, ByVal idTemplate As Decimal) As Boolean
#End Region

#Region "Setup Approve Ext - Thiết lập người thay thế"
        <OperationContract()>
        Function InsertApproveSetupExt(ByVal item As ApproveSetupExtDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateApproveSetupExt(ByVal item As ApproveSetupExtDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetApproveSetupExt(ByVal id As Decimal) As ApproveSetupExtDTO
        <OperationContract()>
        Function GetApproveSetupExtList(ByVal employeeId As Decimal, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupExtDTO)
        <OperationContract()>
        Function DeleteApproveSetupExt(ByVal itemDeletes As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function IsExistSetupExtByDate(ByVal itemCheck As ApproveSetupExtDTO, Optional ByVal idExclude As Decimal? = Nothing) As Boolean
#End Region

#Region "Lấy thông tin về quy trình phê duyệt cho nhân viên khi đăng ký"

        <OperationContract()>
        Function GetApproveUsers(ByVal employeeId As Decimal, ByVal processCode As String) As List(Of ApproveUserDTO)

#End Region

        <OperationContract()>
        Function GetListEmployee(ByVal _orgIds As List(Of Decimal), Optional ByVal is_ter As Decimal = 0) As List(Of EmployeeDTO)
#End Region

#End Region

#Region "SE_MAIL"
        <OperationContract()>
        Function Get_Se_Mail(ByVal _filter As SEMailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATE_DATE desc") As List(Of SEMailDTO)
#End Region

#Region "Ldap"

        <OperationContract()>
        Function GetLdap(ByVal _filter As LdapDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "LDAP_NAME") As List(Of LdapDTO)

        <OperationContract()>
        Function InsertLdap(ByVal objLdap As LdapDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyLdap(ByVal objLdap As LdapDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteLdap(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Validate ID Table"
        <OperationContract()>
        Function CheckExistIDTable(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
        <OperationContract()>
        Function CheckExistValue(ByVal lstID As List(Of String), ByVal table As String, ByVal column As String) As Boolean
        <OperationContract()>
        Function CheckWorkStatus(ByVal colName As String, ByVal Value As String) As Boolean
        <OperationContract()>
        Function CheckExistProgram(ByVal strTableName As String, ByVal strcolName As String, ByVal strValue As String) As Boolean
        <OperationContract()>
        Function AutoGenColID(ByVal strTableName As String, ByVal strColName As String) As Decimal
        <OperationContract()>
        Function ValidateComboboxActive(ByVal tableName As String, ByVal colName As String, ByVal ID As Decimal) As Boolean
#End Region

#Region "MailTemplate"
        <OperationContract()>
        Function GetMailTemplate(ByVal _filter As MailTemplateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of MailTemplateDTO)

        <OperationContract()>
        Function GetMailTemplateBaseCode(ByVal code As String, ByVal group As String) As MailTemplateDTO

        <OperationContract()>
        Function InsertMailTemplate(ByVal mailTemplate As MailTemplateDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyMailTemplate(ByVal mailTemplate As MailTemplateDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteMailTemplate(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CheckValidEmailTemplate(ByVal code As String, ByVal group As String) As Boolean
#End Region

#Region "DynamicControl"
        <OperationContract()>
        Function GetListControl(ByVal KeyView As String) As DataTable

        <OperationContract()>
        Function Insert_Edit_Dynamic_Control(ByVal KeyView As String, ByVal dataView As String) As DataTable

#End Region
#Region "Config Display view"
        <OperationContract()>
        Function GetConfigView(ByVal KeyView As String) As DataTable
        <OperationContract()>
        Function GetConfigViewAndFillData(ByVal keyView As String, ByRef viewcontrol As se_view_config_control_DTO, ByRef girdcollum As se_view_config_girdColumm_DTO)
#End Region

#Region "Config portal"
        <OperationContract()>
        Function SetGeneralConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean
#End Region
#Region "SE_SYSTEM_MAINTAIN"
        <OperationContract()>
        Function GetSystem_Maintain(ByVal _filter As SE_SYSTEM_MAINTAINDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SE_SYSTEM_MAINTAINDTO)
        <OperationContract()>
        Function ModifySystem_Maintain(ByVal lstObj As SE_SYSTEM_MAINTAINDTO,
                                   ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function InsertSystem_Maintain(ByVal _item As SE_SYSTEM_MAINTAINDTO, ByVal log As UserLog) As Boolean

        '<OperationContract()>
        'Function DeleteRegisterUser(ByVal lst_id As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CHECK_DUP_SYSTEM_MAINTAIN(ByVal effect_date As Date, Optional ByVal id As Decimal = 0) As Decimal
        <OperationContract()>
        Function CHECK_SYSTEM_MAINTAIN() As String
        <OperationContract()>
        Function CHECK_SYSTEM_MAINTAIN_IS_END() As Boolean
        <OperationContract()>
        Function GET_SYSTEM_MAINTAIN_IS_END() As String
        <OperationContract()>
        Function GetSE_BACKGROUND_PORTALList() As List(Of SE_BACKGROUND_PORTALDTO)

        <OperationContract()>
        Function ModifyBACKGROUND_PORTAL(ByVal lstObj As SE_BACKGROUND_PORTALDTO,
                                   ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function InsertBACKGROUND_PORTAL(ByVal _item As SE_BACKGROUND_PORTALDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GET_SE_BACKGROUND_PORTAL() As String
        <OperationContract()>
        Function DeleteSE_BACKGROUND_PORTAL(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Trainning Request"
        <OperationContract()>
        Function GetTrainningRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
#End Region

#Region "Portal Process"
        <OperationContract()>
        Function GetProcessByIDReg(ByVal _id_RegGroup As Decimal, ByVal _process_Type As String) As List(Of ProcessDTO)
#End Region

#Region "Work Process"

        <OperationContract()>
        Function GetHRProcessList(ByVal _filter As HR_PROCESS_DTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE DESC", Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetPeWorkProcessByID(ByVal id As Decimal) As PE_WORK_PROCESSDTO

        <OperationContract()>
        Function GetPeWorkEmployeeByProcess(ByVal id As Decimal) As List(Of PE_WORK_EMPLOYEEDTO)

        <OperationContract()>
        Function GetPeWorkManByProcess(ByVal id As Decimal) As List(Of PE_WORK_MANAGERDTO)

        <OperationContract()>
        Function GetPeWorkManConcludeByProcess(ByVal id As Decimal) As PE_WORK_MANAGER_CONCLUDEDTO

        <OperationContract()>
        Function InsertPeWorkProcess(ByVal obj As PE_WORK_PROCESSDTO, ByVal log As UserLog) As Integer

        <OperationContract()>
        Function ModifyPeWorkProcess(ByVal obj As PE_WORK_PROCESSDTO, ByVal log As UserLog) As Integer

        <OperationContract()>
        Function DeleteWorkProcess(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateWorkProcess(ByVal obj As PE_WORK_PROCESSDTO) As Boolean

        <OperationContract()>
        Function InsertManagerConclude(ByVal obj As PE_WORK_MANAGER_CONCLUDEDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyManagerConclude(ByVal obj As PE_WORK_MANAGER_CONCLUDEDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function HRProcessCancel(ByVal id As Decimal, ByVal process As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function HRProcessChangeAssignee(ByVal id As Decimal, ByVal _empID As Decimal) As Boolean

        <OperationContract()>
        Function PrintWorkProcess(ByVal _Id_reggroup As Decimal) As Byte()

        <OperationContract()>
        Function PrintTerProcess(ByVal _Id_reggroup As Decimal) As Byte()

        <OperationContract()>
        Function PrintTerHandover(ByVal _Id_reggroup As Decimal) As Byte()

        <OperationContract()>
        Function GetTitlesByOrg2(ByVal _emp As Decimal, Optional ByVal _Is_Blank As Boolean = False) As DataTable

        <OperationContract()>
        Function GetEmpInfoNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO)
        <OperationContract()>
        Function GetEmpHrProcessNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO)

        <OperationContract()>
        Function ReadHRNoty(ByVal id As Decimal, ByVal processType As String) As Boolean

        <OperationContract()>
        Function GetAppProcessNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO)
#End Region

#Region "Ter Process"

        <OperationContract()>
        Function GetTerProcessByID(ByVal id As Decimal) As HU_TERMINATION_PROCESSDTO

        <OperationContract()>
        Function InsertTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO, ByVal log As UserLog) As Integer

        <OperationContract()>
        Function ModifyTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO, ByVal log As UserLog) As Integer

        <OperationContract()>
        Function DeleteTerProcess(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO) As Boolean
#End Region

#Region "Man Process"

        <OperationContract()>
        Function InsertManProcess(ByVal obj As PE_MANAGER_OFFER_DTO, ByVal log As UserLog) As Integer

        <OperationContract()>
        Function ModifyManProcess(ByVal obj As PE_MANAGER_OFFER_DTO, ByVal log As UserLog) As Integer

        <OperationContract()>
        Function DeleteManProcess(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetEmployeeByManager(ByVal man_id As Decimal) As List(Of EmployeeDTO)
        <OperationContract()>
        Function PrintManProcess(ByVal _Id_reggroup As Decimal) As Byte()
#End Region



#Region "quan ly file"
        <OperationContract()>
        Function GetFoldersAll() As List(Of FolderDTO)

        <OperationContract()>
        Function GetFoldersStructureInfo(ByVal _folderId As Decimal) As List(Of FolderDTO)
#End Region
        <OperationContract()>
        Function GetFrameSalaryAll() As List(Of PA_FRAME_SALARYDTO)
        <OperationContract()>
        Function GetFrameProductivityAll() As List(Of PA_FRAME_PRODUCTIVITYDTO)

#Region "Dashboard"
        <OperationContract()>
        Function SendMail_ctrlDashboardHome(ByVal lstDTO As List(Of SEMailDTO)) As Boolean
#End Region
    End Interface
End Namespace
