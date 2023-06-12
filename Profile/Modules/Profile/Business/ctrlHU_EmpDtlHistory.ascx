<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlHistory.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlHistory" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView>
                <Columns>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thay đổi %>" DataField="DATE_CHANGE"
                        UniqueName="DATE_CHANGE" SortExpression="DATE_CHANGE" DataFormatString="{0:dd/MM/yyy HH:mm:ss}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu thay đổi %>" DataField="TYPE_CHANGE"
                        UniqueName="TYPE_CHANGE" SortExpression="TYPE_CHANGE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quản lý trực tiếp %>" DataField="DIRECT_MANAGER_NAME"
                        UniqueName="DIRECT_MANAGER_NAME" SortExpression="DIRECT_MANAGER_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quản lý trên 1 cấp %>" DataField="LEVEL_MANAGER_NAME"
                        UniqueName="LEVEL_MANAGER_NAME" SortExpression="LEVEL_MANAGER_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        UniqueName="GENDER_NAME" SortExpression="GENDER_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi sinh %>" DataField="BIRTH_PLACE"
                        UniqueName="BIRTH_PLACE" SortExpression="BIRTH_PLACE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng hôn nhân %>" DataField="MARITAL_STATUS_NAME"
                        UniqueName="MARITAL_STATUS_NAME" SortExpression="MARITAL_STATUS_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tôn giáo %>" DataField="RELIGION_NAME"
                        UniqueName="RELIGION_NAME" SortExpression="RELIGION_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Dân tộc %>" DataField="NATIVE_NAME"
                        UniqueName="NATIVE_NAME" SortExpression="NATIVE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quốc tịch %>" DataField="NATIONALITY_NAME"
                        UniqueName="NATIONALITY_NAME" SortExpression="NATIONALITY_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="PER_ADDRESS"
                        UniqueName="PER_ADDRESS" SortExpression="PER_ADDRESS">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="PER_PROVINCE_NAME"
                        UniqueName="PER_PROVINCE_NAME" SortExpression="PER_PROVINCE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="PER_DISTRICT_NAME"
                        UniqueName="PER_DISTRICT_NAME" SortExpression="PER_DISTRICT_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="PER_WARD_NAME"
                        UniqueName="PER_WARD_NAME" SortExpression="PER_WARD_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ tạm trú %>" DataField="NAV_ADDRESS"
                        UniqueName="NAV_ADDRESS" SortExpression="NAV_ADDRESS">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="NAV_PROVINCE_NAME"
                        UniqueName="NAV_PROVINCE_NAME" SortExpression="NAV_PROVINCE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="NAV_DISTRICT_NAME"
                        UniqueName="NAV_DISTRICT_NAME" SortExpression="NAV_DISTRICT_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="NAV_WARD_NAME"
                        UniqueName="NAV_WARD_NAME" SortExpression="NAV_WARD_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại cố định %>" DataField="HOME_PHONE"
                        UniqueName="HOME_PHONE" SortExpression="HOME_PHONE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại di động %>" DataField="MOBILE_PHONE"
                        UniqueName="MOBILE_PHONE" SortExpression="MOBILE_PHONE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                        UniqueName="ID_DATE" SortExpression="ID_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hộ chiếu %>" DataField="PASS_NO"
                        UniqueName="PASS_NO" SortExpression="PASS_NO">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="PASS_DATE"
                        UniqueName="PASS_DATE" SortExpression="PASS_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hạn %>" DataField="PASS_EXPIRE"
                        UniqueName="PASS_EXPIRE" SortExpression="PASS_EXPIRE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="PASS_PLACE"
                        UniqueName="PASS_PLACE" SortExpression="PASS_PLACE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Visa %>" DataField="VISA" UniqueName="VISA"
                        SortExpression="VISA">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="VISA_DATE"
                        UniqueName="VISA_DATE" SortExpression="VISA_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hạn %>" DataField="VISA_EXPIRE"
                        UniqueName="VISA_EXPIRE" SortExpression="VISA_EXPIRE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="VISA_PLACE"
                        UniqueName="VISA_PLACE" SortExpression="VISA_PLACE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giấy phép lao động %>" DataField="WORK_PERMIT"
                        UniqueName="WORK_PERMIT" SortExpression="WORK_PERMIT">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="WORK_PERMIT_DATE"
                        UniqueName="WORK_PERMIT_DATE" SortExpression="WORK_PERMIT_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hạn %>" DataField="WORK_PERMIT_EXPIRE"
                        UniqueName="WORK_PERMIT_EXPIRE" SortExpression="WORK_PERMIT_EXPIRE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="WORK_PERMIT_PLACE"
                        UniqueName="WORK_PERMIT_PLACE" SortExpression="WORK_PERMIT_PLACE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email công ty %>" DataField="WORK_EMAIL"
                        UniqueName="WORK_EMAIL" SortExpression="WORK_EMAIL">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email cá nhân %>" DataField="PER_EMAIL"
                        UniqueName="PER_EMAIL" SortExpression="PER_EMAIL">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã số thuế %>" DataField="PIT_CODE"
                        UniqueName="PIT_CODE" SortExpression="PIT_CODE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người liên hệ %>" DataField="CONTACT_PER"
                        UniqueName="CONTACT_PER" SortExpression="CONTACT_PER">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="CONTACT_PER_PHONE"
                        UniqueName="CONTACT_PER_PHONE" SortExpression="CONTACT_PER_PHONE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tài khoản %>" DataField="BANK_NO"
                        UniqueName="BANK_NO" SortExpression="BANK_NO">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngân hàng %>" DataField="BANK_NAME"
                        UniqueName="BANK_NAME" SortExpression="BANK_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh ngân hàng %>" DataField="BANK_BRANCH_NAME"
                        UniqueName="BANK_BRANCH_NAME" SortExpression="BANK_BRANCH_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Công đoàn phí %>" DataField="DOAN_PHI"
                        UniqueName="DOAN_PHI" SortExpression="DOAN_PHI">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào đoàn %>" DataField="NGAY_VAO_DOAN"
                        UniqueName="NGAY_VAO_DOAN" SortExpression="NGAY_VAO_DOAN">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi vào đoàn %>" DataField="NOI_VAO_DOAN"
                        UniqueName="NOI_VAO_DOAN" SortExpression="NOI_VAO_DOAN">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức vụ đoàn %>" DataField="CHUC_VU_DOAN"
                        UniqueName="CHUC_VU_DOAN" SortExpression="CHUC_VU_DOAN">
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đảng phí %>" DataField="DANG_PHI"
                        UniqueName="DANG_PHI" SortExpression="DANG_PHI">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào đảng %>" DataField="NGAY_VAO_DANG"
                        UniqueName="NGAY_VAO_DANG" SortExpression="NGAY_VAO_DANG">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi vào đảng %>" DataField="NOI_VAO_DANG"
                        UniqueName="NOI_VAO_DANG" SortExpression="NOI_VAO_DANG">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức vụ đảng %>" DataField="CHUC_VU_DANG"
                        UniqueName="CHUC_VU_DANG" SortExpression="CHUC_VU_DANG">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngành nghề %>" DataField="CAREER"
                        UniqueName="CAREER" SortExpression="CAREER">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ văn hóa %>" DataField="ACADEMY_NAME"
                        UniqueName="ACADEMY_NAME" SortExpression="ACADEMY_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ chuyên môn %>" DataField="MAJOR_NAME"
                        UniqueName="MAJOR_NAME" SortExpression="MAJOR_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Diễn giải trình độ chuyên môn %>"
                        DataField="MAJOR_REMARK" UniqueName="MAJOR_REMARK" SortExpression="MAJOR_REMARK">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trường học %>" DataField="GRADUATE_SCHOOL_NAME"
                        UniqueName="GRADUATE_SCHOOL_NAME" SortExpression="GRADUATE_SCHOOL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TRAINING_FORM_NAME"
                        UniqueName="TRAINING_FORM_NAME" SortExpression="TRAINING_FORM_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ học vấn %>" DataField="LEARNING_LEVEL_NAME"
                        UniqueName="LEARNING_LEVEL_NAME" SortExpression="LEARNING_LEVEL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngoại ngữ %>" DataField="LANGUAGE"
                        UniqueName="LANGUAGE" SortExpression="LANGUAGE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ ngoại ngữ %>" DataField="LANGUAGE_LEVEL_NAME"
                        UniqueName="LANGUAGE_LEVEL_NAME" SortExpression="LANGUAGE_LEVEL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm số/Xếp loại %>" DataField="LANGUAGE_MARK"
                        UniqueName="LANGUAGE_MARK" SortExpression="LANGUAGE_MARK">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chiều cao %>" DataField="CHIEU_CAO"
                        UniqueName="CHIEU_CAO" SortExpression="CHIEU_CAO">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cân nặng %>" DataField="CAN_NANG"
                        UniqueName="CAN_NANG" SortExpression="CAN_NANG">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm máu %>" DataField="NHOM_MAU"
                        UniqueName="NHOM_MAU" SortExpression="NHOM_MAU">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Huyết áp %>" DataField="HUYET_AP"
                        UniqueName="HUYET_AP" SortExpression="HUYET_AP">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thị lực mắt trái %>" DataField="MAT_TRAI"
                        UniqueName="MAT_TRAI" SortExpression="MAT_TRAI">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thị lực mắt phải %>" DataField="MAT_PHAI"
                        UniqueName="MAT_PHAI" SortExpression="MAT_PHAI">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại sức khỏe %>" DataField="LOAI_SUC_KHOE"
                        UniqueName="LOAI_SUC_KHOE" SortExpression="LOAI_SUC_KHOE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tai mũi họng %>" DataField="TAI_MUI_HONG"
                        UniqueName="TAI_MUI_HONG" SortExpression="TAI_MUI_HONG">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Răng hàm mặt %>" DataField="RANG_HAM_MAT"
                        UniqueName="RANG_HAM_MAT" SortExpression="RANG_HAM_MAT">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tim %>" DataField="TIM" UniqueName="TIM"
                        SortExpression="TIM">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phổi ngực %>" DataField="PHOI_NGUC"
                        UniqueName="PHOI_NGUC" SortExpression="PHOI_NGUC">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Viêm gan B %>" DataField="VIEM_GAN_B"
                        UniqueName="VIEM_GAN_B" SortExpression="VIEM_GAN_B">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Da và hoa liễu %>" DataField="DA_HOA_LIEU"
                        UniqueName="DA_HOA_LIEU" SortExpression="DA_HOA_LIEU">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú sức khỏe %>" DataField="GHI_CHU_SUC_KHOE"
                        UniqueName="GHI_CHU_SUC_KHOE" SortExpression="GHI_CHU_SUC_KHOE">
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlHistory_RadSplitter2');
        }

    </script>
</tlk:RadScriptBlock>