<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlConfiguration.ascx.vb"
    Inherits="Portal.ctrlConfiguration" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Số lần login lỗi tối đa")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnMAX_LOGIN_FAIL" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Session Timeout")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnSESSION_TIMEOUT" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td>
                    (<%# Translate("phút")%>)
                </td>
            </tr>
            <tr>
            <td colspan="6">
                    <b>
                        <%# Translate("Mật khẩu")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Độ dài mật khẩu tối thiểu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPasswordLength" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqPASSWORD_LENGTH" ControlToValidate="rntxtPasswordLength"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Độ dài tối thiểu. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Độ dài tối thiểu. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ký tự chữ hoa")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkPasswordUpper" runat="server"  />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ký tự chữ thường")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkPasswordLower" runat="server"/>
                    <asp:CustomValidator runat="server" ID="valCharacter" ErrorMessage='<%$ Translate : Phải có ít nhất 1 dạng ký tự được đánh dấu. %>'></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ký tự số")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkPasswordNumber" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ký tự đặc biệt")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkPasswordSpecial" runat="server"/>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
