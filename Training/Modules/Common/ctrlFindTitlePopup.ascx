﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTitlePopup.ascx.vb"
    Inherits="Common.ctrlFindTitlePopup" %>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack("PopupPostback:" + arg.ID);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>