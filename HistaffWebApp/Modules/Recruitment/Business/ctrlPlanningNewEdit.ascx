<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPlanningNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlPlanningNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgId" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidYear" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitleName" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtNumRequired" NumberFormat-DecimalDigits="0"
                        ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên đợt")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtName" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên đợt %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đợt %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtNumRequest" NumberFormat-DecimalDigits="0">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="rntxtNumRequest" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập số lượng yêu cầu %>" ToolTip="<%$ Translate: Bạn phải nhập số lượng yêu cầu %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRCReason">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusRCReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn lý du tuyển dụng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn lý du tuyển dụng %>" ClientValidationFunction="cusRCReason">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtCost" NumberFormat-DecimalDigits="0">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thời hạn cần có NV")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdResponseDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung chi phí")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtContentCost" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Y/c trình độ năng lực")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtCapacity" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả công việc")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtDescription" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>
                        <%# Translate("Điểm đạt")%></b>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 1")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam1Reach">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Môn thi 2")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam2Reach">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Môn thi 3")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam3Reach">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 4")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam4Reach">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Môn thi 5")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam5Reach">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>
                        <%# Translate("Tiến độ")%></b>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Xây dựng KH")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBuildDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đăng tin")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdPostDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdPostDate"
                        ControlToCompare="rdBuildDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày đăng tin phải lớn hơn hoặc bằng ngày xây dựng KH %>"
                        ToolTip="<%$ Translate: Ngày đăng tin phải lớn hơn hoặc bằng ngày xây dựng KH %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thu hồ sơ")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdIncomeDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdIncomeDate"
                        ControlToCompare="rdPostDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày thu hồ sơ phải lớn hơn hoặc bằng ngày đăng tin %>"
                        ToolTip="<%$ Translate: Ngày thu hồ sơ phải lớn hơn hoặc bằng ngày đăng tin %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thi vòng 1")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExam1Date">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="rdExam1Date"
                        ControlToCompare="rdIncomeDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày thi vòng 1 phải lớn hơn hoặc bằng ngày đăng tin %>"
                        ToolTip="<%$ Translate: Ngày thi vòng 1 phải lớn hơn hoặc bằng ngày đăng tin %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Phỏng vấn")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdInterviewDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdInterviewDate"
                        ControlToCompare="rdExam1Date" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày phỏng vấn phải lớn hơn hoặc bằng ngày thi vòng 1 %>"
                        ToolTip="<%$ Translate: Ngày phỏng vấn phải lớn hơn hoặc bằng ngày thi vòng 1 %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Kiểm tra ma túy")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDrugDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="rdDrugDate"
                        ControlToCompare="rdInterviewDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kiểm tra ma túy phải lớn hơn hoặc bằng ngày phỏng vấn %>"
                        ToolTip="<%$ Translate: Ngày kiểm tra ma túy phải lớn hơn hoặc bằng ngày phỏng vấn %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tổ chức đào tạo")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdTrainingDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="rdTrainingDate"
                        ControlToCompare="rdDrugDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày tổ chức đào tạo phải lớn hơn hoặc bằng ngày kiểm tra ma túy %>"
                        ToolTip="<%$ Translate: Ngày tổ chức đào tạo phải lớn hơn hoặc bằng ngày kiểm tra ma túy %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tiếp nhận")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdReceiveDate">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="rdReceiveDate"
                        ControlToCompare="rdTrainingDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày tiếp nhận phải lớn hơn hoặc bằng ngày tổ chức đào tạo %>"
                        ToolTip="<%$ Translate: Ngày tiếp nhận phải lớn hơn hoặc bằng ngày tổ chức đào tạo %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>
                        <%# Translate("Đơn vị phối hợp")%></b>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Xây dựng KH")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtBuildOrg">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đăng tin")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPostOrg">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thu hồ sơ")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIncomeOrg">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thi vòng 1")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam1Org">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phỏng vấn")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtInterviewOrg">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Kiểm tra ma túy")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtDrugOrg">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tổ chức đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTrainingOrg">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tiếp nhận")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtReceiveOrg">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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


        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'PRINT_TN') {
                enableAjax = false;
            }
        }

        function cusRCReason(oSrc, args) {
            var cbo = $find("<%# cboRCReason.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        
        
    </script>
</tlk:RadCodeBlock>
