<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgetPass.aspx.vb" Inherits="HistaffPortal.ForgetPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" runat="server">

<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=EDGE" />
    <title></title>
    <link type="image/x-icon" rel="shortcut icon" href="/Static/images/fav-icon.ico" />
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script>
</head>
<body runat="server">
    <form id="form1" runat="server">
        <tlk:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
            <StyleSheets>
                <tlk:StyleSheetReference Path="~/Styles/reset.css" />
                <tlk:StyleSheetReference Path="~/Styles/Site.css" />
                <tlk:StyleSheetReference Path="~/Styles/Layout.css" />
                <tlk:StyleSheetReference Path="~/Styles/jMenu.jquery.css" />
                <tlk:StyleSheetReference Path="~/Styles/RadGrid.css" />
                <tlk:StyleSheetReference Path="~/Styles/Scheduler.css" />
            </StyleSheets>
        </tlk:RadStyleSheetManager>
        <tlk:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="360000">
            <Scripts>
                <tlk:RadScriptReference Path="~/Scripts/jquery-1.7.1.min.js" />
                <tlk:RadScriptReference Path="~/Scripts/common.js" />
                <tlk:RadScriptReference Path="~/Scripts/jMenu.jquery.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/jquery.noty.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/layouts/center.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/layouts/topCenter.js" />
                <tlk:RadScriptReference Path="~/Scripts/noty/themes/default.js" />
                <tlk:RadScriptReference Path="~/Scripts/PreventBackSpace.js" />
            </Scripts>
        </tlk:RadScriptManager>
        <tlk:RadAjaxManager runat="server" ID="RadAjaxManager1">
            <AjaxSettings>
                <tlk:AjaxSetting AjaxControlID="Panel1">
                    <UpdatedControls>
                        <tlk:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </tlk:AjaxSetting>
                <tlk:AjaxSetting AjaxControlID="Panel2">
                    <UpdatedControls>
                        <tlk:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </tlk:AjaxSetting>
                <tlk:AjaxSetting AjaxControlID="Panel3">
                    <UpdatedControls>
                        <tlk:AjaxUpdatedControl ControlID="Panel3" LoadingPanelID="LoadingPanel" />
                    </UpdatedControls>
                </tlk:AjaxSetting>
            </AjaxSettings>
        </tlk:RadAjaxManager>
        <contenttemplate runat="server">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnForgetPassword" BorderStyle="None" EnableViewState="true">
                <fieldset style="border: none;" runat="server">
                    <table class="table-form" style="height: 100%;margin-top:60px" runat="server">
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="lb" style="width: 120px;">
                                <%# Translate("Nhập tài khoản")%><span class="lbReq" style="color: red;">*</span>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox ID="txtUsername" MaxLength="255" runat="server" Width="100%">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUsername"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập Username%>" ToolTip="<%$ Translate: Bạn phải nhập Username%>">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <tlk:RadButton ID="btnForgetPassword" runat="server" Text="<%$ Translate: Xác nhận%>"
                                    CausesValidation="true" />
                            </td>
                            <td>
                                <tlk:RadButton ID="btnCancel" runat="server" Text="<%$ Translate: Bỏ qua%>"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSubmitCode" Visible="false" BorderStyle="None">
                <fieldset style="border: none;">
                    <table class="table-form" style="height: 100%;margin-top:60px">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="lb" style="width: 120px">
                                <%# Translate("Mã xác nhận")%><span class="lbReq" style="color: red;">*</span>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox ID="txtCode" MaxLength="255" runat="server" Width="100%">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập mã xác nhận%>" ToolTip="<%$ Translate: Bạn phải nhập mã xác nhận%>">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <tlk:RadButton ID="btnSubmitCode" runat="server" Text="<%$ Translate: Xác nhận%>"
                                    CausesValidation="true" />
                            </td>
                            <td>
                                <tlk:RadButton ID="btnCodeCancel" runat="server" Text="<%$ Translate: Bỏ qua%>"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSubmidPass" Visible="false" BorderStyle="None">
                <fieldset style="border: none;">
                    <table class="table-form" style="height: 100%;margin-top:20px">
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="lb" style="width: 120px;">
                                <%# Translate("Mật khẩu mới")%><span class="lbReq" style="color: red;">*</span>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox ID="txtPassword" MaxLength="255" runat="server" Width="100%" SkinID="TextboxPassword">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập mật khẩu mới%>" ToolTip="<%$ Translate: Bạn phải nhập mật khẩu mới%>">
                                </asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="validatePASSWORD" runat="server"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="lb" style="width: 120px;">
                                <%# Translate("Nhập lại mật khẩu mới")%><span class="lbReq" style="color: red;">*</span>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox ID="txtRePass" MaxLength="255" runat="server" Width="100%" SkinID="TextboxPassword">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRePass"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập lại mật khẩu%>" ToolTip="<%$ Translate: Bạn phải nhập lại mật khẩu%>">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRePass"
                                    Type="String" ControlToCompare="txtPassword" Operator="Equal" ErrorMessage="<%$ Translate: Mật khẩu nhập lại không khớp với mật khẩu mới %>"
                                    ToolTip="<%$ Translate: Mật khẩu nhập lại không khớp với mật khẩu mới %>">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <tlk:RadButton ID="btnSubmidPass" runat="server" Text="<%$ Translate: Xác nhận%>"
                                    CausesValidation="true" />
                            </td>
                            <td>
                                <tlk:RadButton ID="btnPassCancel" runat="server" Text="<%$ Translate: Bỏ qua%>"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </contenttemplate>
        <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel">
        </tlk:RadAjaxLoadingPanel>
        <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
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

                $.noty.defaults = {
                    layout: 'topCenter',
                    theme: 'defaultTheme',
                    type: 'alert',
                    text: '',
                    dismissQueue: true, // If you want to use queue feature set this true
                    template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                    animation: {
                        open: { height: 'toggle' },
                        close: { height: 'toggle' },
                        easing: 'swing',
                        speed: 500 // opening & closing animation speed
                    },
                    timeout: false, // delay for closing event. Set false for sticky notifications
                    force: true, // adds notification to the beginning of queue when set to true
                    modal: false,
                    closeWith: ['click'], // ['click', 'button', 'hover']
                    callback: {
                        onShow: function () { },
                        afterShow: function () { },
                        onClose: function () { },
                        afterClose: function () { }
                    },
                    buttons: false // an array of buttons
                };
            </script>
        </tlk:RadCodeBlock>
    </form>

</body>
</html>

