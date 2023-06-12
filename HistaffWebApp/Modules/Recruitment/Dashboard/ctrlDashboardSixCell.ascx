<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboardSixCellSixCell.ascx.vb"
    Inherits="Recruitment.ctrlDashboardSixCell" %>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="None" runat="server" Width="200px"
    VisibleTitlebar="false" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;">
    <Windows>
        <tlk:RadWindow runat="server" ID="rw1" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw2" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw3" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw4" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw5" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw6" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd1, oWnd2, oWnd3, oWnd4, oWnd5, oWnd6;

        var i1 = 0;
        var i2 = 0;
        var i3 = 0;
        var i4 = 0;
        var i5 = 0;
        var i6 = 0;
        function SizeToFit(resize) {

            window.setTimeout(function () {
                winW = ($("#WindowMainRegion").width() - 16) / 3;
                winH = ($("#WindowMainRegion").height() - 13) / 2;
                pos = $("#WindowMainRegion").offset();
                var left = pos.left + 8;
                var top = pos.top + 32;

                oWnd1 = $find("<%=rw1.ClientID %>");
                oWnd2 = $find("<%=rw2.ClientID %>");
                oWnd3 = $find("<%=rw3.ClientID %>");
                oWnd4 = $find("<%=rw4.ClientID %>");
                oWnd5 = $find("<%=rw5.ClientID %>");
                oWnd6 = $find("<%=rw6.ClientID %>");

                oWnd1.setSize(winW, winH);
                oWnd1.moveTo(left, top);
                if (i1 == 0) {
                    radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBCanToEmp&group=Dashboard&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize, "rw1");
                    i1 = 1;
                }

                oWnd2.setSize(winW, winH);
                oWnd2.moveTo(left + winW, top);
                if (i2 == 0) {
                    radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBEstimateReality&group=Dashboard&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize, "rw2");
                    i2 = 1;
                }

                oWnd3.setSize(winW, winH);
                oWnd3.moveTo(left + winW * 2, top);
                if (i3 == 0) {
                    radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBStatisticGender&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw3");
                    i3 = 1;
                }

                oWnd4.setSize(winW, winH);
                oWnd4.moveTo(left, top + winH);
                if (i4 == 0) {
                    radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBStatisticEducation&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw4");
                    i4 = 1;
                }

                oWnd5.setSize(winW, winH);
                oWnd5.moveTo(left + winW, top + winH);
                if (i5 == 0) {
                    radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBStatisticEducation&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw5");
                    i5 = 1;
                }

                oWnd6.setSize(winW, winH);
                oWnd6.moveTo(left + winW * 2, top + winH);
                if (i6 == 0) {
                    radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBStatisticEducation&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw6");
                    i6 = 1;
                }

            }, 100);
        }
        $(window).resize(function () {
            SizeToFit(1);
        });
    </script>
</tlk:RadCodeBlock>
