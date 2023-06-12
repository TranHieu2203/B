<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb"
    Inherits="HistaffWebApp.ChangePassword" ViewStateMode="Enabled" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <tlk:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <tlk:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <tlk:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </tlk:AjaxSetting>
        </AjaxSettings>
    </tlk:RadAjaxManager>
    <tlk:RadWindow runat="server" ID="radLogin" VisibleOnPageLoad="true" VisibleStatusbar="false"
        Width="570px" Height="360px" EnableShadow="true" Behaviors="None" Modal="true"
        Title="<%$ Translate: Thay đổi mật khẩu%>">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnChangePassword">
                <fieldset>
                    <table class="table-form" style="height: 100%;">
                        <tr>
                            <td rowspan="8" style="vertical-align: top; padding-top: 5px">
                                <img src="../Static/Images/change_password.jpg" alt="#" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td>
                            </td>
                            <td class="lb" style="width: 120px;">
                                <%# Translate("Mật khẩu cũ")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtOLD_PASS" MaxLength="255" runat="server" TextMode="Password">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtOLD_PASS"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập Mật khẩu cũ%>" ToolTip="<%$ Translate: Bạn phải nhập lại Mật khẩu cũ%>">
                                </asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="valiidateOldPass" runat="server"  ErrorMessage="<%$ Translate: Mật khẩu cũ không đúng. %>"
                                    ToolTip="<%$ Translate: Mật khẩu cũ không đúng. %>"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                        </tr>
                        <tr style="height: 20px">
                            <td>
                            </td>
                            <td class="lb" style="width: 120px;">
                                <%# Translate("Mật khẩu mới")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNEW_PASS" runat="server" SkinID="TextboxPassword">
                                    <PasswordStrengthSettings IndicatorElementID="CustomIndicator1" />
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" Display="Static" runat="server"
                                    ControlToValidate="txtNEW_PASS" ErrorMessage="<%$ Translate: Bạn phải nhập Mật khẩu mới%>"
                                    ToolTip="<%$ Translate: Bạn phải nhập lại Mật khẩu mới%>">
                                </asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="validatePASSWORD_2" runat="server"></asp:CustomValidator>
                                <span id="Span1">&nbsp;</span>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <span id="CustomIndicator1">&nbsp;</span>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td>
                            </td>
                            <td class="lb" style="width: 120px;">
                                <%# Translate("Nhập lại mật khẩu")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNEW_PASS_AGAIN" runat="server" SkinID="TextboxPassword">
                                    <PasswordStrengthSettings IndicatorElementID="CustomIndicator2" />
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Static" runat="server"
                                    ControlToValidate="txtNEW_PASS_AGAIN" ErrorMessage="<%$ Translate: Bạn phải nhập lại Mật khẩu mới%>"
                                    ToolTip="<%$ Translate: Bạn phải nhập lại Mật khẩu mới%>">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtNEW_PASS"
                                    ControlToCompare="txtNEW_PASS_AGAIN" ErrorMessage="<%$ Translate: VALIDATE_PASSWORD_COMPARE %>"
                                    ToolTip="<%$ Translate: VALIDATE_PASSWORD_COMPARE %>"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <span id="CustomIndicator2">&nbsp;</span>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="text-align: center; padding-right: 10px; padding-top: 5px">
                    <tlk:RadButton ID="btnChangePassword" runat="server" Text="<%$ Translate: Đổi mật khẩu%>"
                        CausesValidation="true" />
                    <tlk:RadButton ID="btnCancel" runat="server" Text="<%$ Translate: Hủy%>"
                        CausesValidation="false" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </tlk:RadWindow>
    <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel">
    </tlk:RadAjaxLoadingPanel>
</asp:Content>
