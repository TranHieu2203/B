<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryFund.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryFund" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="350px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                    CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                              <tlk:RadComboBox ID="cboYear" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="valrnmValueFrom" ControlToValidate="cboYear" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Năm %>" ToolTip="<%$ Translate: Bạn phải nhập Năm %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tháng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                              <tlk:RadComboBox ID="cboMonth" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboMonth"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tháng %>" ToolTip="<%$ Translate: Bạn phải nhập Tháng %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lương cứng cho phép")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtSalaryHard" runat="server">
                                <ClientEvents OnValueChanged="OnValueChanged" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Lương phụ cấp, BHXH")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtSalaryAllowance" runat="server">
                                <ClientEvents OnValueChanged="OnValueChanged" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lương mềm cho phép")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtSalarySoft" runat="server">
                                <ClientEvents OnValueChanged="OnValueChanged" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Lương khác cho phép")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtSalaryOther" runat="server">
                                <ClientEvents OnValueChanged="OnValueChanged" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tổng quỹ lương")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtSalaryTotal" runat="server" ReadOnly="true">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnValueChanged(sender, args) {
            var id = sender.get_id();
            var valueSalAllowance = 0;
            var valueSalHard = 0;
            var valueSalOther = 0;
            var valueSalSoft = 0;
            var valueSalTotal = 0;
            var objSalAllowance = $find('<%= rntxtSalaryAllowance.ClientID %>');
            var objSalHard = $find('<%= rntxtSalaryHard.ClientID %>');
            var objSalOther = $find('<%= rntxtSalaryOther.ClientID %>');
            var objSalSoft = $find('<%= rntxtSalarySoft.ClientID %>');
            var objSalTotal = $find('<%= rntxtSalaryTotal.ClientID %>');

            if (objSalAllowance.get_value()) {
                valueSalAllowance = objSalAllowance.get_value();
            }

            if (objSalHard.get_value()) {
                valueSalHard = objSalHard.get_value();
            }

            if (objSalOther.get_value()) {
                valueSalOther = objSalOther.get_value();
            }

            if (objSalSoft.get_value()) {
                valueSalSoft = objSalSoft.get_value();
            }

            switch (id) {
                case '<%= rntxtSalaryAllowance.ClientID %>':
                    valueSalAllowance = 0;
                    if (args.get_newValue()) {
                        valueSalAllowance = args.get_newValue();
                    }
                    break;
                case '<%= rntxtSalaryHard.ClientID %>':
                    valueSalHard = 0;
                    if (args.get_newValue()) {
                        valueSalHard = args.get_newValue();
                    }
                    break;
                case '<%= rntxtSalaryOther.ClientID %>':
                    valueSalOther = 0;
                    if (args.get_newValue()) {
                        valueSalOther = args.get_newValue();
                    }
                    break;
                case '<%= rntxtSalarySoft.ClientID %>':
                    valueSalSoft = 0;
                    if (args.get_newValue()) {
                        valueSalSoft = args.get_newValue();
                    }
                    break;
                default:
                    break;
            }

            valueSalTotal = parseFloat(valueSalAllowance) + parseFloat(valueSalHard) + parseFloat(valueSalOther) + parseFloat(valueSalSoft);
            objSalTotal.set_value(valueSalTotal);

        }
    </script>
</tlk:RadCodeBlock>
