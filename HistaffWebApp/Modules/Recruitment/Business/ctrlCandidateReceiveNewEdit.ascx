<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCandidateReceiveNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlCandidateReceiveNewEdit" %>
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
                    <%# Translate("Mã ứng viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCandidateCode" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCandidateName" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate" Enabled="false" DateInput-Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdEffectDate" DateInput-CausesValidation="false"
                        AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày hưởng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Ngày hưởng %>" ControlToValidate="rdEffectDate">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày vào")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdJoinDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqJoinDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày vào %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Ngày vào %>" ControlToValidate="rdJoinDate">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày dự kiến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdJoinPlanDate" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqJoinPlanDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày dự kiến %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Ngày dự kiến %>" ControlToValidate="rdJoinPlanDate">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdJoinDate"
                        ControlToCompare="rdJoinPlanDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày dự kiến đi làm phải nhỏ hơn ngày vào %>"
                        ToolTip="<%$ Translate: Ngày dự kiến đi làm phải nhỏ hơn ngày vào %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thang lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboSalaryGroup" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusSalaryGroup" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thang lương %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Thang lương %>" ClientValidationFunction="cusSalaryGroup">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngạch lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboSalaryLevel" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusSalaryLevel" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngạch lương %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Ngạch lương %>" ClientValidationFunction="cusSalaryLevel">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bậc lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboSalaryRank" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusSalaryRank" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Bậc lương %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Bậc lương %>" ClientValidationFunction="cusSalaryRank">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboSalaryCoefficient">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusSalaryCoefficient" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Hệ số %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Hệ số %>" ClientValidationFunction="cusSalaryCoefficient">
                    </asp:CustomValidator>
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
        }

        function cusSalaryGroup(oSrc, args) {
            var cbo = $find("<%# cboSalaryGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusSalaryLevel(oSrc, args) {
            var cbo = $find("<%# cboSalaryLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusSalaryRank(oSrc, args) {
            var cbo = $find("<%# cboSalaryRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusSalaryCoefficient(oSrc, args) {
            var cbo = $find("<%# cboSalaryCoefficient.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
    </script>
</tlk:RadCodeBlock>
