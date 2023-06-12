<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlConfirmCodeAccuracy.ascx.vb"
    Inherits="Common.ctrlConfirmCodeAccuracy" %>
<%@ Import Namespace="Common" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />

<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPanel" runat="server" Height="100%" Scrolling="None">
        <table class="table-form" style="padding-top: 30px; height: 100px">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Mã xác thực:  %>"></asp:Label>
                </td>
                <td colspan="10">
                    <tlk:RadTextBox ID="txtCodeAccuracy" runat="server" Width="300px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td></td>
                <td>
                    <tlk:RadButton ID="btnLogin" runat="server" Text="<%$ Translate: Xác nhận %>">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btncancel" runat="server" Text="<%$ Translate: Bỏ qua %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>

    </tlk:RadPane>
</tlk:RadSplitter>


<script type="text/javascript">
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
        //if (args.get_item().get_commandName() == 'LOGIN') {
        getRadWindow().close(null);
        args.set_cancel(true);
        //}
    }
    function popupclose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
        }
    }

    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>
