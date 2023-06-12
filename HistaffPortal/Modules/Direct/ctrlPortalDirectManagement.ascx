<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalDirectManagement.ascx.vb"
    Inherits="Profile.ctrlPortalDirectManagement" %>
<table class="table-form">
    <tr>
        <td>
            <%# Translate("Chọn cán bộ")%></b>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboEmployee" SkinID="LoadDemand" AutoPostBack="true"
                CausesValidation="false" Width="200px">
            </tlk:RadComboBox>
        </td>
    </tr>
</table>
<tlk:RadTabStrip ID="rtabProfileInfo" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
    AutoPostBack="false">
    <Tabs>
        <tlk:RadTab runat="server" ID="rtIdEmpInfo" PageViewID="rpvEmpInfo" Text="<%$ Translate: Thông tin hồ sơ %>"
            Selected="True">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdFamilyInfo" PageViewID="rpvFamilyInfo" Text="<%$ Translate: Nhân thân %>">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdWorkingBeforeInfo" PageViewID="rpvdWorkingBeforeInfo"
            Text="<%$ Translate: Quá trình Công tác trước đây %>">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdWorkingInfo" PageViewID="rpvdWorkingInfo" Text="<%$ Translate: Quá trình Công tác %>">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdTrainingOutInfo" PageViewID="rpvdTrainingOutInfo"
            Text="<%$ Translate: Quá trình Đào tạo ngoài công ty %>">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdContractInfo" PageViewID="rpvdContractInfo" Text="<%$ Translate: Quá trình Hợp đồng lao động %>">
        </tlk:RadTab>
        <%--<tlk:RadTab runat="server" ID="rtIdWageInfo" PageViewID="rpvdWageInfo" Text="<%$ Translate: Quá trình Thay đổi lương - phụ cấp %>" Enabled ="false">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdCommendInfo" PageViewID="rpvdCommendInfo" Text="<%$ Translate: Khen thưởng %>" Enabled ="false">
        </tlk:RadTab>
        <tlk:RadTab runat="server" ID="rtIdDisciplineInfo" PageViewID="rpvdDisciplineInfo"
            Text="<%$ Translate: Kỷ luật %>" Enabled ="false">
        </tlk:RadTab>--%>
    </Tabs>
</tlk:RadTabStrip>
<tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%">
    <tlk:RadPageView ID="rpvEmpInfo" runat="server" Width="100%" BorderWidth="1" BorderColor="DarkGray">
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Sơ yếu lý lịch")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmpCODE" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Họ và tên lót")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFirstNameVN" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLastNameVN" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitle">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Nhóm chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitleGroup" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Quản lý trực tiếp")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtDirectManager" ReadOnly="true" />
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quản lý trên một cấp")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtLevelManager" ReadOnly="true" />
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtStaffRank">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Giới tính")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtGender">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtID_NO">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdIDDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDPlace">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Quốc tịch")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNationlity">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Dân tộc")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNative">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tôn giáo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtReligion">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi sinh")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtBirthPlace" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày vào tập đoàn")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdJoinDateState" Enabled="false" DateInput-Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày vào công ty")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdJoinDate" Enabled="false" DateInput-Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtWorkStatus">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ thường trú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thành phố")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPer_Province">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận huyện")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPer_District">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Xã phường")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPer_Ward">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ tạm trú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNavAddress" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thành phố")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNav_Province">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận huyện")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNav_District">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Xã phường")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNav_Ward">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin hợp đồng lao động mới nhất")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại hợp đồng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractType" runat="server" Width="100%" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="false" DateInput-Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="false" DateInput-Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin phụ")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Email công ty")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtWorkEmail">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Email cá nhân")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPerEmail">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng hôn nhân")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFamilyStatus">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Điện thoại di động")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtMobilePhone">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điện thoại cố định")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtHomePhone">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngành nghề")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCareer">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkDoanPhi" runat="server" Text="<%$ Translate: Công đoàn phí %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày vào")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Chức vụ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucVuDoan" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nơi vào")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkDangPhi" runat="server" Text="<%$ Translate: Đảng phí %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày vào")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayVaoDang" runat="server" Width="160px">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Chức vụ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucVuDang" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nơi vào")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiVaoDang" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số hộ chiếu")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPassNo" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPassDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hạn")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPassExpireDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtPassPlace" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Visa")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtVisa" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdVisaDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hạn")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdVisaExpireDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtVisaPlace" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giấy phép lao động")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtWorkPermit" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdWorkPermitDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hạn")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdWorPermitExpireDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtWorkPermitPlace" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPerson" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPersonPhone" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Trình độ văn hóa")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên trường")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtGraduateSchool" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ chuyên môn")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMajor" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Diễn giải")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtMajorRemark" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ văn hóa")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAcademy" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hình thức đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTrainingForm" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ học vấn")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLearningLevel" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ ngoại ngữ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLanguage" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLangLevel" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điểm số/Xếp loại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLangMark" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin tài khoản")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số tài khoản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBankNo" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngân hàng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBank" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi nhánh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBankBranch" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã số Thuế")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPitCode" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin sức khỏe")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chiều cao(cm)")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChieuCao" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tai mũi họng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTaiMuiHong" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Cân nặng(kg)")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCanNang" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Răng hàm mặt")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRangHamMat" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm máu")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNhomMau" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tim")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTim" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Huyết áp")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtHuyetAp" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phổi và ngực")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPhoiNguc" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thị lực mắt trái")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMatTrai" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Viêm gan B")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtVienGanB" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thị lực mắt phải")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMatPhai" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Da và Hoa liễu")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDaHoaLieu" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại sức khỏe")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLoaiSucKhoe" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtGhiChuSK" Width="100%" SkinID="Textbox1023" />
                </td>
            </tr>
           <%-- <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Chức danh kiêm nhiệm")%></b>
                    <hr />
                </td>
            </tr>--%>
        </table>
        <%--<tlk:RadGrid PageSize=50 ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="250px" AllowSorting="True" AllowMultiRowSelection="true" Width="73%">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,NAME,EFFECT_DATE,EXPIRE_DATE" ClientDataKeyNames="ID,NAME,EFFECT_DATE,EXPIRE_DATE">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="NAME" UniqueName="NAME"
                        SortExpression="NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>--%>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvFamilyInfo" runat="server" Width="100%">
        <tlk:RadGrid PageSize=50 ID="rgFamily" runat="server" Height="300px" Width="100%">
            <MasterTableView>
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quan hệ %>" DataField="RELATION_NAME"
                        UniqueName="RELATION_NAME" SortExpression="RELATION_NAME">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Giảm trừ %>" DataField="IS_DEDUCT"
                        UniqueName="IS_DEDUCT" SortExpression="IS_DEDUCT" HeaderStyle-Width="80px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày giảm trừ %>" DataField="DEDUCT_FROM"
                        UniqueName="DEDUCT_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_FROM">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="DEDUCT_TO"
                        UniqueName="DEDUCT_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_TO">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_REG" HeaderText="<%$ Translate: Ngày đăng ký giảm trừ%>"
                        UniqueName="DEDUCT_REG" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="ADDRESS"
                        UniqueName="ADDRESS" SortExpression="ADDRESS">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdWorkingBeforeInfo" runat="server" Width="100%">
        <tlk:RadGrid PageSize=50 ID="rgWorkingBefore" runat="server" Height="300px" Width="100%">
            <MasterTableView>
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                        UniqueName="COMPANY_NAME" SortExpression="COMPANY_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="TELEPHONE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ công ty %>" DataField="COMPANY_ADDRESS"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="COMPANY_ADDRESS">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                        UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nghỉ %>" DataField="END_DATE"
                        UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridNumericColumn DataField="SALARY" HeaderText="<%$ Translate: Mức lương %>"
                        UniqueName="SALARY" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:n0}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="TITLE_NAME">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                        UniqueName="TER_REASON" SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdWorkingInfo" runat="server" Width="100%">
        <tlk:RadGrid PageSize=50 ID="rgWorking" runat="server" Height="300px" Width="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại quyết định %>" DataField="DECISION_TYPE_NAME"
                        UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="true ">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE"
                        ShowFilterIcon="true ">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                        UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                        UniqueName="SIGN_DATE" SortExpression="SIGN_DATE" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGN_NAME"
                        UniqueName="SIGN_NAME" SortExpression="SIGN_NAME" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGN_TITLE"
                        UniqueName="SIGN_TITLE" SortExpression="SIGN_TITLE" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdTrainingOutInfo" runat="server" Width="100%">
        <tlk:RadGrid PageSize=50 ID="rgTrainingOutCompany" runat="server" Height="300px" Width="100%">
            <MasterTableView>
                <Columns>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="YEAR_GRA"
                        UniqueName="YEAR_GRA" SortExpression="YEAR_GRA" ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="NAME_SHOOLS"
                        UniqueName="NAME_SHOOLS" SortExpression="NAME_SHOOLS" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="FORM_TRAIN_NAME"
                        UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="SPECIALIZED_TRAIN"
                        UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả đào tạo %>" DataField="RESULT_TRAIN"
                        UniqueName="RESULT_TRAIN" SortExpression="RESULT_TRAIN" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/chứng chỉ %>" DataField="CERTIFICATE"
                        UniqueName="CERTIFICATE" SortExpression="CERTIFICATE" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE_FROM"
                        UniqueName="EFFECTIVE_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECTIVE_DATE_TO"
                        UniqueName="EFFECTIVE_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdContractInfo" runat="server" Width="100%">
        <tlk:RadGrid PageSize=50 ID="rgContract" runat="server" Height="300px" Width="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACTTYPE_NAME"
                        UniqueName="CONTRACTTYPE_NAME" SortExpression="CONTRACTTYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hợp đồng %>" DataField="CONTRACT_NO"
                        UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE"
                        UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGNER_NAME"
                        UniqueName="SIGNER_NAME" SortExpression="SIGNER_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                        UniqueName="SIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SIGN_DATE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGNER_TITLE"
                        UniqueName="SIGNER_TITLE" SortExpression="SIGNER_TITLE" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdWageInfo" runat="server" Width="100%">
        <tlk:RadGrid PageSize=50 ID="rgSalary" runat="server" Height="300px" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                        UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương cơ bản %>" DataField="SAL_BASIC"
                        UniqueName="SAL_BASIC" DataFormatString="{0:###,###,###,##0}" SortExpression="SAL_BASIC"
                        ShowFilterIcon="true">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % lương được hưởng %>" DataField="PERCENT_SALARY"
                        UniqueName="PERCENT_SALARY" SortExpression="PERCENT_SALARY" ShowFilterIcon="true">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí hỗ trợ  %>" DataField="COST_SUPPORT"
                        UniqueName="COST_SUPPORT" SortExpression="COST_SUPPORT" ShowFilterIcon="true">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <br />
        <tlk:RadGrid PageSize=50 ID="rgAllow" runat="server" Height="200px">
            <MasterTableView Caption="<%$ Translate: Phụ cấp theo tờ trình/QĐ %>">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                        SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                        SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                        SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </tlk:GridCheckBoxColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdCommendInfo" runat="server" Width="100%" Enabled ="false">
        <tlk:RadGrid PageSize=50 ID="rgCommend" runat="server" Height="350px" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp khen thưởng %>" DataField="COMMEND_LEVEL_NAME"
                        UniqueName="COMMEND_LEVEL_NAME" SortExpression="COMMEND_LEVEL_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức khen thưởng %>" DataField="COMMEND_TYPE_NAME"
                        UniqueName="COMMEND_TYPE_NAME" SortExpression="COMMEND_TYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung khen thưởng %>" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY" UniqueName="MONEY"
                        SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
    <tlk:RadPageView ID="rpvdDisciplineInfo" runat="server" Width="100%" Enabled ="false">
        <tlk:RadGrid PageSize=50 ID="rgDiscipline" runat="server" Height="350px" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp kỷ luật %>" DataField="DISCIPLINE_LEVEL_NAME"
                        UniqueName="DISCIPLINE_LEVEL_NAME" SortExpression="DISCIPLINE_LEVEL_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại kỷ luật %>" DataField="DISCIPLINE_TYPE_NAME"
                        UniqueName="DISCIPLINE_TYPE_NAME" SortExpression="DISCIPLINE_TYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY" UniqueName="MONEY"
                        SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPageView>
</tlk:RadMultiPage>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    </script>
</tlk:RadCodeBlock>
