<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_Configuration.ascx.vb"
    Inherits="Common.ctrlSE_Configuration" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Session")%></b><hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Session Timeout (phút)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSessionTimeout" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqSESSION_TIMEOUT" ControlToValidate="rntxtSessionTimeout"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Session Timeout. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Session Timeout. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Session Warning (phút)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSessionWarning" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqSESSION_WARNING" ControlToValidate="rntxtSessionWarning"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Session Warning. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Session Warning. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator runat="server" ID="cvalWarning" ErrorMessage='<%$ Translate : Session Warning không được lớn hơn Session Timeout %>'></asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Active Timeout (phút)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtActiveTimeout" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqACTIVE_TIMEOUT" ControlToValidate="rntxtActiveTimeout"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Active Timeout. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Active Timeout. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lần login lỗi tối đa")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMaxLoginFail" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqMAX_LOGIN_FAIL" ControlToValidate="rntxtMaxLoginFail"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lần login lỗi tối đa. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Số lần login lỗi tối đa. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Portal port")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPortalPort" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Application port")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAppPort" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>   
            <tr>
                <td class="lb">
                    <%# Translate("Thời gian hiệu lực code reset mật khẩu (giây)")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnEffectTimeForCodeResetPassword" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqEffectTimeForCodeResetPassword" ControlToValidate="rnEffectTimeForCodeResetPassword"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian hiệu lực code reset mật khẩu. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian hiệu lực code reset mật khẩu. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b>
                        <%# Translate("Mật khẩu")%></b><hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Độ dài tối thiểu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPasswordLength" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqPASSWORD_LENGTH" ControlToValidate="rntxtPasswordLength"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Độ dài tối thiểu. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Độ dài tối thiểu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="chkPasswordUpper" runat="server" Text="Ký tự chữ hoa" />
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="cbPasswordLower" runat="server" Text="Ký tự chữ thường" />
                    <asp:CustomValidator runat="server" ID="valCharacter" ErrorMessage='<%$ Translate : Phải có ít nhất 1 dạng ký tự được đánh dấu. %>'></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="cbPasswordNumber" runat="server" Text="Ký tự số" />
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox ID="cbPasswordSpecial" runat="server" Text="Ký tự đặc biệt" />
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Reset Mật khẩu")%><hr />
                </td>
            </tr>
            <tr>
                <td class="lb"></td>
                <td>
                    <asp:RadioButton ID="cbPasswordConfig" runat="server" Text="Phát sinh mật khẩu theo thiết lập" AutoPostBack="true" GroupName="Reset_Pass"/>
                </td>
                </tr>
            <tr>
                <td class="lb"></td>
                <td>
                    <asp:RadioButton ID="cbPasswordDefault" runat="server" Text="Phát sinh mật khẩu mặc định" AutoPostBack="true" GroupName="Reset_Pass"/>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtPasswordDefault" runat="server" >
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rvPasswordDefault" ControlToValidate="rtPasswordDefault"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mật khẩu mặc định. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập mật khẩu mặc định. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
