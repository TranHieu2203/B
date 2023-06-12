<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHomeDashboard.ascx.vb" 
Inherits="Profile.ctrlHomeDashboard" %>
<style>
    :root {
        --ds-lb-clr: #8b8b8b;
    }
    #RAD_SPLITTER_ctl00_MainContent_RadSplitter1{
        border: none;
        background: #f9f9f9;
    }
    #WindowMainRegion{
        background: #f9f9f9;
    }
    #ctl00_MainContent_RadPaneMain{
        border: none;
    }
    #RadWindowWrapper_ctl00_MainContent_ctrlHomeDashboard_rw4{
        width: 98% !important;
        left: 13px !important;
        top: 88px !important;
    }
    .lb,.lb3,label{
        color: var(--ds-lb-clr);
    }
</style>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="None" runat="server" Width="1200px"
    VisibleTitlebar="false" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;">
    <Windows>
        <tlk:RadWindow runat="server" ID="rw4" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd4;

        var i4 = 0;

        function SizeToFit(resize) {
            const queryString = window.location.search;
            const urlParams = new URLSearchParams(queryString);

            window.setTimeout(function () {
                winW = ($("#WindowMainRegion").width() + 85) / 2;
                winH = ($("#WindowMainRegion").height() + 550);
                pos = $("#WindowMainRegion").offset();
                var left = pos.left + 8;
                var top = pos.top + 32;

                oWnd4 = $find("<%=rw4.ClientID %>");

                oWnd4.setSize(winW * 2 - 100, winH / 1.85);
                oWnd4.moveTo(left, top);
                if (i4 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlDashboardHome&group=Shared&width=' + (oWnd4.get_width()) + '&height=' + (oWnd4.get_height()-35) + "&noscroll=1&resize=" + resize + "&kindRemind=" + urlParams.get('kindRemind'), "rw4");
                    i4 = 1;
                }

            }, 100);
        }
        $(window).resize(function () {
            SizeToFit(1);
        });
    </script>
</tlk:RadCodeBlock>
