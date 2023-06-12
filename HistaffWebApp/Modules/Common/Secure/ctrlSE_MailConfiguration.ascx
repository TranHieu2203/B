<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_MailConfiguration.ascx.vb"
    Inherits="Common.ctrlSE_MailConfiguration" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("SMTP server")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMailServer" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Port")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPort" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ email")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMailAddress" runat="server">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ID="cvalMailAddress" ControlToValidate="txtMailAddress" runat="server"
                        ErrorMessage="<%$ Translate: ĐỊNH DẠNG EMAIL KHÔNG CHÍNH XÁC%>" ToolTip="<%$ Translate: ĐỊNH DẠNG EMAIL KHÔNG CHÍNH XÁC%>"
                        ValidationExpression="^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$">
                    </asp:RegularExpressionValidator>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cboIsSSL" Text="Xác thực SSL?" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tài khoản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMailAccount" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Mật khẩu")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMailPass" runat="server" TextMode="Password">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:CheckBox runat="server" ID="cbIsAuthen" Text="Có xác thực tài khoản khi gửi thư không?"
                        AutoPostBack="true" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
