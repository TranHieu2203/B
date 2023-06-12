<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindEmployeePopup.ascx.vb"
    Inherits="Common.ctrlFindEmployeePopup" %>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if (arg == null) {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>
