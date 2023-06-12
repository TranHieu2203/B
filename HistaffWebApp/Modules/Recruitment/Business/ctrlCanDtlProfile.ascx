<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCanDtlProfile.ascx.vb"
    Inherits="Recruitment.ctrlCanDtlProfile" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="32px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <asp:Panel runat="server" ID="Panel1">
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Mã ứng viên")%>
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:HiddenField ID="hidID" runat="server" />
                                        <asp:HiddenField ID="hidWorkID" runat="server" />
                                        <asp:HiddenField ID="hidPrevious" runat="server" />
                                        <asp:HiddenField ID="hidNext" runat="server" />
                                        <asp:HiddenField ID="hidPlanID" runat="server" />
                                        <tlk:RadButton runat="server" ID="btnPre" Text="<<" CausesValidation="false">
                                        </tlk:RadButton>
                                        <tlk:RadTextBox ID="txtEmpCODE" runat="server" Width="80px" SkinID="Textbox15">
                                        </tlk:RadTextBox>
                                        <tlk:RadButton runat="server" ID="btnNext" Text=">>" CausesValidation="false">
                                        </tlk:RadButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%# Translate("Họ và tên đêm")%>
                                    <span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtFirstNameVN" runat="server" Width="166px">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="reqFirstNameVN" ControlToValidate="txtFirstNameVN"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập họ và tên lót %>" ToolTip="<%$ Translate: Bạn phải nhập họ và tên lót %>">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <%# Translate("Tên")%>
                                    <span class="lbReq">*</span>
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <tlk:RadTextBox ID="txtLastNameVN" runat="server" Width="166px">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="reqLastNameVN" ControlToValidate="txtLastNameVN"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên %>" ToolTip="<%$ Translate: Bạn phải nhập tên %>">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server">
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        </tlk:RadAjaxPanel>
        <tlk:RadTabStrip ID="rtabMain" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
            AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" PageViewID="rpvEmpCV" Text="<%$ Translate: Sơ yếu lý lịch %>"
                    Selected="true">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvOtherInfo" Text="<%$ Translate: Thông tin khác %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvEmpEdu" Text="<%$ Translate: Trình độ - Bằng cấp %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvEmpHistory" Text="<%$ Translate: Đặc điểm lịch sử bản thân %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%"
            ScrollBars="Auto">
            <tlk:RadPageView ID="rpvEmpCV" runat="server" Width="100%">
                <div style="margin-bottom: 10px">
                </div>
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày sinh")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqBirthDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày sinh %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Ngày sinh %>" ControlToValidate="rdBirthDate">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Giới tính")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboGender">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cusGender" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Giới tính %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Giới tính %>" ClientValidationFunction="cusGender">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số CMND")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtID_NO">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqID_NO" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Số CMND %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Số CMND %>" ControlToValidate="txtID_NO">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cusID_NO" runat="server" ErrorMessage="<%$ Translate: Ứng viên hiện tại đã có trong hệ thống %>"
                                ToolTip="<%$ Translate: Ứng viên hiện tại đã có trong hệ thống %>">
                            </asp:CustomValidator>
                            <asp:CustomValidator ID="cusBlackList" runat="server" ErrorMessage="<%$ Translate: Ứng viên thuộc danh sách đen %>"
                                ToolTip="<%$ Translate: Ứng viên thuộc danh sách đen %>">
                            </asp:CustomValidator>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdID_Date" Width="135px" DateInput-EmptyMessage="<%$ Translate:  Ngày cấp %>">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboID_Place" EmptyMessage="<%$ Translate: Nơi cấp %>">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số năm kinh nghiệm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rntxtYearExp" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <!--Start Quê quán -->
                    <tr>
                        <td class="lb">
                            <%# Translate("Quê quán")%>
                        </td>
                        <td colspan="2">
                            <tlk:RadTextBox ID="txtNavAddress" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboNav_Province" CausesValidation="false" EmptyMessage="<%$ Translate: Thành phố %>">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <!-- End Quê Quán-->
                    <!-- Start Nơi sinh-->
                    <tr>
                        <td class="lb">
                            <%# Translate("Nơi sinh")%>
                        </td>
                        <td colspan="2">
                            <tlk:RadTextBox ID="txtBirthAddress" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboBirth_Province" CausesValidation="false" EmptyMessage="<%$ Translate: Thành phố %>">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <!-- End Nơi sinh-->
                    <tr>
                        <td class="lb">
                            <%# Translate("Dân tộc")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboNative">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tôn giáo")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboReligion">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <!-- Start Địa chỉ thường chú -->
                    <tr>
                        <td class="lb">
                            <%# Translate("Địa chỉ thường trú")%>
                        </td>
                        <td colspan="2">
                            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPer_Province" CausesValidation="false" EmptyMessage="<%$ Translate: Thành phố %>">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <!-- End Địa chỉ thường chú -->
                    <!--Start Liên hệ -->
                    <tr>
                        <td class="lb">
                            <%# Translate("Địa chỉ liên hệ")%>
                        </td>
                        <td colspan="2">
                            <tlk:RadTextBox ID="txtContactAddress" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboContact_Province" CausesValidation="false"
                                EmptyMessage="<%$ Translate: Thành phố %>">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <!--End Địa chỉ hiện nay -->
                    <tr>
                        <td class="lb">
                            <%# Translate("Hôn nhân")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboMarital">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Điện thoại")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtMobilePhone">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Email")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmail">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mã số thuế")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtPIT">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số sổ BHXH")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtBHXH_No">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Nơi khám chưa bệnh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtHospital">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Sở trường công tác")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="txtWorkForte" Width="492px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvOtherInfo" runat="server" Width="100%">
                <div style="margin-bottom: 10px">
                </div>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width: 170px">
                            <%# Translate("Ngày vào Đảng")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdNgayVaoDang" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb" style="width: 105px">
                            <%# Translate("Ngày chính thức")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdNgayChinhThucVaoDang" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Nơi vào Đảng")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNoiVaoDang" runat="server" Width="440px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày tham gia tổ chức CTXH")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdNgayVaoCTXH" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày nhập ngũ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdNgayNhapNgu" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thông tin các tổ chức")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtThongTinToChuc" runat="server" Width="440px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày xuất ngũ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdNgayXuatNgu" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Quân hàm cao nhất")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboQuanHamCaoNhat" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Danh hiệu phong tặng cao nhất")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadComboBox ID="cboDanhHieuCaoNhat" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Khen thưởng")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtCommend" runat="server" Width="440px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Kỷ luật")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtDiscipline" runat="server" Width="440px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tình trạng sức khỏe")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLoaiSucKhoe" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Chiều cao(cm)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtChieuCao" MinValue="0" runat="server" NumberFormat-DecimalDigits="1">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Cân nặng(kg)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCanNang" runat="server" MinValue="0" NumberFormat-DecimalDigits="1">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhóm máu")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboNhomMau" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                        </td>
                        <td>
                            <tlk:RadButton runat="server" ID="chkThuongBinh" ToggleType="CheckBox" ButtonType="ToggleButton"
                                CausesValidation="false" Width="140px" Text="<%$ Translate: có phải là thương binh%>"
                                AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Hạng")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtHang" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                        </td>
                        <td>
                            <tlk:RadButton runat="server" ID="chkConChinhSach" ToggleType="CheckBox" ButtonType="ToggleButton"
                                CausesValidation="false" Width="140px" Text="<%$ Translate: có phải là con gia đình chính sách %>"
                                AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại đối tượng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLoaiDoiTuong" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày vào Đoàn")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Nơi vào Đoàn")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvEmpEdu" runat="server" Width="100%">
                <div style="margin-bottom: 10px">
                </div>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width: 170px">
                            <%# Translate("Trình độ giáo dục phổ thông")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboAcademy" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="width: 170px">
                            <%# Translate("Hệ đào tạo")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboHeDaoTao" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Trình độ học vấn cao nhất")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboHighestEducation" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trình độ chuyên môn cao nhất")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboHighestLevel" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Hình thức đào tạo (CM)")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trường tốt nghiệp")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtSchoolName" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm đào tạo")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rntxtGraduateYear" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSizes="4" MinValue="1900" MaxValue="9999">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Xếp loại")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboMark" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chứng chỉ khác")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCertificateOther" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Bằng cấp khác")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDegressOther" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngoại ngữ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tin học")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboComputer" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý luận chính trị")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPoliticalTheory" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Quản lý nhà nước")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStateManagement" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thông tin bổ sung")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtAddInfo" runat="server" Width="507px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvEmpHistory" runat="server" Width="100%">
                <div style="margin-bottom: 10px">
                </div>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width: 130px">
                        </td>
                        <td>
                            <tlk:RadButton runat="server" ID="chkOldRegime" ToggleType="CheckBox" ButtonType="ToggleButton"
                                CausesValidation="false" Width="140px" Text="<%$ Translate: Đã bị bắt làm việc trong chế độ cũ %>"
                                AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb" style="width: 73px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdOldRegimeStart" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdOldRegimeEnd" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ở đâu")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboOldRegimeProvince" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <tlk:RadButton runat="server" ID="chkForeignRelationship" ToggleType="CheckBox" ButtonType="ToggleButton"
                                CausesValidation="false" Width="140px" Text="<%$ Translate: Có quan hệ với các tổ chức nước ngoài không? %>"
                                AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Công việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtForeignWork" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên tổ chức")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtForeignName" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trụ sở")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtForeignOffice" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Có thân nhân nước ngoài")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtForeignFamily" runat="server" Width="447px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Kinh tế bản thân")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtEconomy" runat="server" Width="447px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Những vấn đề khác cần bổ sung")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtOtherIssue" runat="server" Width="447px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">


        function cusID_Place(oSrc, args) {
            var cbo = $find("<%# cboID_Place.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusGender(oSrc, args) {
            var cbo = $find("<%# cboGender.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
                pageLoad(165);
            }

        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseWindow() {
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close();
        }

        function LoadEmp(strEmpCode, strMessage, strCurrentPlaceHolder) {
            var oArg = new Object();
            var oWnd = GetRadWindow();
            if (strMessage != '') {
                var notify = noty({ text: strMessage, dismissQueue: true, type: 'success' });
                setTimeout(function () {
                    oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&emp=' + strEmpCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
                }, 2000);
            } else {
                oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&emp=' + strEmpCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
            }
        }

        //Function này để hủy postback khi nhập lương cơ bản rồi enter 
        //Lỗi này chỉ gặp ở trình duyệt chrome và ie.
        function OnKeyBSPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                eventArgs.set_cancel(true);
            }
        }

        function pageLoad(valHeight) {
            var multiPage = $find("<%# RadMultiPage1.ClientID %>");
            var $ = $telerik.$;

            var totalHeight = $(window).height() - 165 + valHeight;
            multiPage.get_element().style.height = totalHeight + "px";
        }

        $(document).ready(function () {
            pageLoad(0);
        });
        $(window).resize(function () {
            pageLoad(0);
        });
    </script>
</tlk:RadScriptBlock>
