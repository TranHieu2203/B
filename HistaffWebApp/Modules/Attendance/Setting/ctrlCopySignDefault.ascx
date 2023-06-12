<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCopySignDefault.ascx.vb"
    Inherits="Attendance.ctrlCopySignDefault" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .AutoHeight
    {
        height: auto !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RSMain" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RPTbar" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RPItem" runat="server" Scrolling="None" CssClass="AutoHeight">
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm sao chép")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnYear" SkinID="Number" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnYear"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập năm. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'SAVE') {

            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
    </script>
</tlk:RadCodeBlock>
