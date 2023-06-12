<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUpdateManager.ascx.vb"
    Inherits="Profile.ctrlUpdateManager" %>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            debugger;
            oWnd = $find('ctl00_MainContent_rwMainPopup');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if(arg == null)
            {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId2 %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }

    </script>
</tlk:RadScriptBlock>
