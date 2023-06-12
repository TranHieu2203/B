<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="100%" BorderStyle="None" Font-Bold="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitle" runat="server" ReadOnly="True" BorderStyle="None" Font-Bold="True">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsInPlan" runat="server" Text="<%$ Translate: Trong kế hoạch %>" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <%--<td class="lb">
                    <%# Translate("Mã tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtProCode" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtProCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mã tuyển dụng %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Mã tuyển dụng %>"> 
                    </asp:RequiredFieldValidator>
                </td>--%>
                <td class="lb">
                    <%# Translate("Đợt tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitment" runat="server">
                    </tlk:RadComboBox>
                </td>
                 <td class="lb">
                    <%# Translate("Ngày dự kiến đi làm ")%>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblExpectedToWorkDay" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdSendDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"> 
                    </asp:RequiredFieldValidator>
                </td>
               <%-- <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRecruitNumber" runat="server" SkinID="Number" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>--%>
                <td class="lb">
                    <%# Translate("Số lượng theo yêu cầu")%>
                </td>
                <td>
                    <asp:Label ID="lblRecruitNumber" runat="server" Text="0"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitReason" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusRecruitReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Lý do tuyển dụng%>"
                        ToolTip="<%$ Translate: Bạn phải chọn Lý do tuyển dụng %>" ClientValidationFunction="cusRecruitReason">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người theo dõi")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFollowers" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmp" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Mức độ ưu tiên")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPriorityLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng chi tiết")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRecruitReason" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="lb">
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmployee" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <%# Translate("Chọn người được thay thế")%>
                </td>
                <td colspan="2">
                </td>
                <td>
                    <%# Translate("Phạm vi tuyển dụng")%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <tlk:RadListBox ID="lstEmployee" runat="server" Width="100%" Height="80px">
                    </tlk:RadListBox>
                </td>
                <td colspan="2">
                    <tlk:RadListBox ID="lstScope" runat="server" Width="100%" Height="80px" CheckBoxes="true"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm công việc chức năng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGroupWork" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Mức lương yêu cầu từ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRequestSalaryFrom" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Mức lương yêu cầu đến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRequestSalaryTo" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsPortal" runat="server" Text="<%$ Translate: Cho phép hiển thị trên Portal %>" />
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="chkNegotiableSalary" runat="server" Text="<%$ Translate: Lương thỏa thuận %>" />
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái%>"
                        ToolTip="<%$ Translate: Bạn phải chọn trạng thái %>" ClientValidationFunction="cusStatus">
                    </asp:CustomValidator>
                </td>
            </tr>
        </table>
        <tlk:RadTabStrip ID="rtabProfileInfo" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
            AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtID1" PageViewID="rpv1" Text="<%$ Translate: Mô tả công việc %>"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtID2" PageViewID="rpv2" Text="<%$ Translate: Điều kiện học vấn %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtID3" PageViewID="rpv3" Text="<%$ Translate: Điều kiện khác %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%"
            ScrollBars="Auto">
            <tlk:RadPageView ID="rpv1" runat="server" Width="100%">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Thời gian làm việc")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTimeWork" runat="server">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cusTimeWork" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thời gian làm việc %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Thời gian làm việc %>" ClientValidationFunction="cusTimeWork">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Mã tuyển dụng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mã tuyển dụng %>" ToolTip="<%$ Translate: Bạn phải nhập Mã tuyển dụng %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên công việc")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtJobName" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtJobName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên công việc %>" ToolTip="<%$ Translate: Bạn phải nhập Tên công việc %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả công việc")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtDescription" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Yêu cầu kinh nghiệm")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRequestExperience" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày bắt đầu tuyển dụng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdRecruitStart" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdRecruitStart"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày bắt đầu tuyển dụng %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Ngày bắt đầu tuyển dụng %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày kết thúc nhận hồ sơ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdReceiveEnd" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdReceiveEnd"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày kết thúc nhận hồ sơ %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Ngày kết thúc nhận hồ sơ %>"> 
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdReceiveEnd"
                                ControlToCompare="rdRecruitStart" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc nhận hồ sơ phải lớn hơn Ngày bắt đầu tuyển dụng %>"
                                ToolTip="<%$ Translate: Ngày kết thúc nhận hồ sơ phải lớn hơn Ngày bắt đầu tuyển dụng %>"></asp:CompareValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỹ năng đặc biệt")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSpecialSkills" runat="server" >
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Kỹ năng mềm khác")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadListBox ID="lstSoftSkill" runat="server" Width="100%" Height="80px" CheckBoxes="true"
                                OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Yêu cầu khác")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRequestOther" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpv2" runat="server" Width="100%">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Trình độ học vấn")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLearningLevel" runat="server">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trình độ học vấn %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Trình độ học vấn %>" ClientValidationFunction="cusLearningLevel">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Số năm kinh nghiệm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtExperienceNumber" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rntxtExperienceNumber"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số năm kinh nghiệm %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Số năm kinh nghiệm %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Trình độ đào tạo chính")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTrainingLevelMain" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trường đào tạo chính")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTrainingSchoolMain" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chuyên ngành chính")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboMajorMain" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại tốt nghiệp chính")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGraduationTypeMain" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Trình độ đào tạo phụ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTrainingLevelSub" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trường đào tạo phụ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTrainingSchoolSub" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chuyên ngành phụ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboMajorSub" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại tốt nghiệp phụ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGraduationTypeSub" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trình độ máy tính")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboComputerLevel" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngoại ngữ 1")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage1" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trình độ ngoại ngữ 1")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage1Level" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Điểm số ngoại ngữ 1")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtLanguage1Point" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngoại ngữ 2")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage2" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trình độ ngoại ngữ 2")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage2Level" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Điểm số ngoại ngữ 2")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtLanguage2Point" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngoại ngữ 3")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage3" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trình độ ngoại ngữ 3")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLanguage3Level" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Điểm số ngoại ngữ 3")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtLanguage3Point" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpv3" runat="server" Width="100%">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Độ tuổi từ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtAgeFrom" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Độ tuổi đến")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtAgeTo" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rntxtAgeTo"
                                ControlToCompare="rntxtAgeFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"
                                ToolTip="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"></asp:CompareValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Giới tính")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGender" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chiều cao")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtChieucao" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Cân nặng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCanNang" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngoại hình")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboAppearance" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thị lực trái")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtThiLucTrai" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Thị lực phải")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtThiLucPhai" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tình trạng sức khỏe")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboHealthStatus" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Tính cách")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadListBox ID="lstCharacter" runat="server" Width="100%" Height="80px" CheckBoxes="true"
                                OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusRecruitReason(oSrc, args) {
            var cbo = $find("<%# cboRecruitReason.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusTimeWork(oSrc, args) {
            var cbo = $find("<%# cboTimeWork.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusLearningLevel(oSrc, args) {
            var cbo = $find("<%# cboLearningLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }



        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }


        function clientButtonClicking(sender, args) {
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }

    </script>
</tlk:RadCodeBlock>
