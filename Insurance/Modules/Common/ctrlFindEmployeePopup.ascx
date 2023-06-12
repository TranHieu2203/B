<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindEmployeePopup.ascx.vb"
    Inherits="Common.ctrlFindEmployeePopup" %>
<tlk:RadButton ID="rbtnPostback" runat="server"></tlk:RadButton>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function OnClientClose(oWnd, args) {
            oWnd = $find('<%#popupId %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack("PopupPostback:" + arg.ID);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%# AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }

        function onRequestStart(sender, args) {
            var currentLoadingPanel = $find("<%# AjaxLoadingId %>");
            //show the loading panel over the updated control
            currentLoadingPanel.show('WindowMainRegion');
        }
        function onRequestEnd() {
            var currentLoadingPanel = $find("<%# AjaxLoadingId %>");
            //hide the loading panel and clean up the global variables
            currentLoadingPanel.hide('WindowMainRegion');
        }
    </script>
</tlk:RadScriptBlock>
