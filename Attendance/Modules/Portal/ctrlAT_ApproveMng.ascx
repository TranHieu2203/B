<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_ApproveMng.ascx.vb"
    Inherits="Attendance.ctrlAT_ApproveMng" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="90%">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="26" Height="26px">
                <tlk:RadTabStrip runat="server" ID="rtabTab" Orientation="HorizontalTop" SelectedIndex="0">
                    <Tabs>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK Nghỉ %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK OT %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK ĐTVS %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK YCTD %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK YCĐT %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK KPI %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐG mục tiêu %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phê duyệt ĐK nhân viên %>">
                        </tlk:RadTab>
                    </Tabs>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <asp:PlaceHolder ID="TabView" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ResizeRadGrid();
        });
        $(document).resize(function () {
            ResizeRadGrid();
        });
        function ResizeRadGrid() {
            debugger;
            var MainPane = $find("<%= MainPane.ClientID%>");
            document.getElementById('WindowMainRegion').style.height = $(window).height() - 182 + 'px';
            if (MainPane != null) {
                MainPane.set_height($("#WindowMainRegion").height() - $(".panel_bar").height() - 10);
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            ResizeRadGrid();
        }
        function EndRequestHandler(sender, args) {
            ResizeRadGrid();
        }
    </script>
</tlk:RadScriptBlock>