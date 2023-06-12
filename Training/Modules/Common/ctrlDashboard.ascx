<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboard.ascx.vb"
    Inherits="Common.ctrlDashboard" %>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="Maximize" runat="server"
    Width="200px" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;" ShowOnTopWhenMaximized="true">
    <windows>
        <tlk:RadWindow runat="server" ID="rw1" OnClientCommand="rw1Command">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw2" OnClientCommand="rw2Command">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw3" OnClientCommand="rw3Command">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw4" OnClientCommand="rw4Command">
        </tlk:RadWindow>
    </windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd1, oWnd2, oWnd3, oWnd4;
        function rw1Command(sender, eventArgs) {
            if (eventArgs.get_commandName() == 'Maximize') {
                var w = $(document).width();
                var h = $(document).height();
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeStatistic&group=Dashboard&width=' + (w - 20) + '&height=' + (h - 80) + "&noscroll=1&resize=1", "rw1");
            } else if (eventArgs.get_commandName() == 'Restore') {
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeStatistic&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=1", "rw1");
            }
        }

        function rw2Command(sender, eventArgs) {
            if (eventArgs.get_commandName() == 'Maximize') {
                var w = $(document).width();
                var h = $(document).height();
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeChange&group=Dashboard&width=' + (w - 20) + '&height=' + (h - 80) + "&noscroll=1&resize=1", "rw2");
            } else if (eventArgs.get_commandName() == 'Restore') {
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeChange&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=1", "rw2");
            }
        }

        function rw3Command(sender, eventArgs) {
            if (eventArgs.get_commandName() == 'Maximize') {
                var w = $(document).width();
                var h = $(document).height();
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBReminder&group=Dashboard&width=' + (w - 20) + '&height=' + (h - 80) + "&noscroll=1&resize=1", "rw3");
            } else if (eventArgs.get_commandName() == 'Restore') {
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBReminder&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=1", "rw3");
            }
        }
        function rw4Command(sender, eventArgs) {
            if (eventArgs.get_commandName() == 'Maximize') {
                var w = $(document).width();
                var h = $(document).height();
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBInfo&group=Dashboard&width=' + (w - 20) + '&height=' + (h - 80) + "&noscroll=1&resize=1", "rw4");
            } else if (eventArgs.get_commandName() == 'Restore') {
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBInfo&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=1", "rw4");
            }
        }
        function SizeToFit(resize) {
            window.setTimeout(function () {
                winW = $(".PanelMain").width() / 3;
                winH = $(".PanelMain").height();
                pos = $(".PanelMain").offset();
                var left = pos.left;
                var top = pos.top;

                oWnd1 = $find("<%=rw1.ClientID %>");
                oWnd2 = $find("<%=rw2.ClientID %>");
                oWnd3 = $find("<%=rw3.ClientID %>");
                oWnd4 = $find("<%=rw4.ClientID %>");

                oWnd1.setSize(winW, winH / 2);
                oWnd1.moveTo(left, top);
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeStatistic&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw1");

                oWnd2.setSize(winW, winH / 2);
                oWnd2.moveTo(left + winW, top);
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeChange&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw2");

                oWnd3.setSize(winW, winH);
                oWnd3.moveTo(left + winW * 2, top);
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBReminder&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw3");

                oWnd4.setSize(winW * 2, winH / 2);
                oWnd4.moveTo(left, top + winH / 2);
                //radopen('Dialog.aspx?mid=Profile&fid=ctrlDBInfo&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw4");
            }, 400);
        }
        $(document).ready(function () {
            SizeToFit(0);
        });
        $(window).resize(function () {
            SizeToFit(1);
        });
    </script>
</tlk:RadCodeBlock>
