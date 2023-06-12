<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBInfoProfile.ascx.vb"
    Inherits="Profile.ctrlDBInfoProfile" %>
<script src="../../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts-more.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/modules/exporting.js" type="text/javascript"></script>
<link href="../../../Styles/userCustom.css" rel="stylesheet" type="text/css">
<style type="text/css">
    .lblInfo {
        font-weight: bold;
        color: #2196f3;
    }

    .item-head {
        color: #6a6a6a !important;
    }
</style>
<link href="../../../Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="95%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <table class="table-form">
            <tr>
                <td colspan="5">
                    <span class="item-head"><%--<i class="fa fa-bar-chart"></i>--%>
                        <%# Translate("Thống kê nhanh")%></span>
                </td>
            </tr>
            <tr class="crum-top">
                <td>
                    <asp:Label runat="server" ID="lbEmpCount" Text="Tổng số nhân viên hiện tại:"></asp:Label>
                </td>
                <td style="width: 50px" class="lb">
                    <asp:Label ID="lbtnEmpCount" runat="server" CssClass="lblInfo" />
                </td>
                <td style="width: 20px" class="lb"></td>
                <td>
                    <asp:Label runat="server" ID="lbEmpNew" Text="Nhân viên tuyển mới trong tháng:"></asp:Label>
                </td>
                <td style="width: 50px" class="lb">
                    <asp:Label ID="lbtnEmpNew" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lbEmpTer" Text="Nhân viên nghỉ việc trong tháng:"></asp:Label>
                </td>
                <td class="lb">
                        <asp:Label ID="lbtnEmpTer" runat="server" CssClass="lblInfo" />
                </td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbAgeAvg" Text="Tuổi bình quân:"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnAgeAvg" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lbTransferNew" Text="Lao động chuyển đi:"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnTransferNew" runat="server" CssClass="lblInfo" />
                </td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbTransferMove" Text="Lao động chuyển đến:"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnTransferMove" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr class="crum-bottom">
                <td>
                    <asp:Label runat="server" ID="lbContractNew" Text="Hợp đồng tạo mới trong tháng:"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnContractNew" runat="server" CssClass="lblInfo" />
                </td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbSeniority" Text="Thâm niên bình quân (năm):"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnSeniority" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td colspan="5" style="padding-top: 20px;">
                    <span class="item-head"><%--<i class="fa fa-exclamation-circle"></i>--%>
                        <%# Translate("Nhắc nhở")%></span>
                </td>
            </tr>
            <tr class="crum-top">
                <td id="td16" runat="server">
                    <asp:Label runat="server" ID="lbReminderPage" Text="Chưa nộp đủ giấy tờ khi tiếp nhận:"></asp:Label>
                </td>
                <td class="lb" id="td16_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=16" target="_blank">
                         <asp:Label ID="lbReminder16" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
                <td id="td16_3" runat="server"></td>
                <td id="td13" runat="server">
                    <asp:Label runat="server" ID="lbToTrinh" Text="Hết hạn tờ trình:"></asp:Label>
                </td>
                <td class="lb" id="td13_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=13" target="_blank">
                        <asp:Label ID="lbReminder13" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
            </tr>
            <tr >
                <td id="td38" runat="server">
                    <asp:Label runat="server" ID="lbExpAuthority" Text="Ủy quyền sắp hết hạn:"></asp:Label>
                </td>
                <td class="lb" id="td38_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=38" target="_blank">
                        <asp:Label ID="lbnExpAuthority" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
                <td id="td38_3" runat="server"></td>
                <td id="td10" runat="server">
                    <asp:Label runat="server" ID="lbVisa" Text="Hết hạn Visa:"></asp:Label>
                </td>
                <td class="lb" id="td10_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=10" target="_blank">
                        <asp:Label ID="lbReminderVisa" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
            </tr>
            <tr>
                <td id="td1" runat="server">
                    <asp:Label runat="server" ID="lbReminder" Text="Hết hạn hợp đồng:"></asp:Label>
                </td>
                <td class="lb" id="td1_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=1" target="_blank">
                         <asp:Label ID="lbReminder1" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
                <td id="td1_3" runat="server"></td>
                <td id="td20" runat="server">
                    <asp:Label runat="server" ID="lbProbation" Text="Sắp hết hạn thử việc:"></asp:Label>
                </td>
                <td class="lb" id="td20_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=20" target="_blank">
                         <asp:Label ID="lbnProbation" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
            </tr>
            <tr>
                <td id="td2" runat="server">
                    <asp:Label runat="server" ID="lbReminderDay" Text="Sắp đến sinh nhật:"></asp:Label>
                </td>
                <td class="lb" id="td2_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=2" target="_blank">
                         <asp:Label ID="lbReminder2" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
                <td id="td2_3" runat="server"></td>
                <td id="td19" runat="server">
                    <asp:Label runat="server" ID="lbGPLD" Text="Giấy phép lao động:"></asp:Label>
                </td>
                <td class="lb" id="td19_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=19" target="_blank">
                    <asp:Label ID="lbReminder19" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
            </tr>
            <tr>
                <td id="td24" runat="server">
                    <asp:Label runat="server" ID="lbMaterniti" Text="Sắp hết kỳ nghỉ thai sản:"></asp:Label>
                </td>
                <td class="lb" id="td24_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=24" target="_blank">

                    </a>
                    <asp:Label ID="lbnMaterniti" runat="server" CssClass="lblInfo" />
                </td>
                <td id="td24_3" runat="server"></td>
                <td id="td25" runat="server">
                    <asp:Label runat="server" ID="lbRetirement" Text="Sắp đến kỳ nghỉ hưu:"></asp:Label>
                </td>
                <td class="lb" id="td25_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=25" target="_blank">
                    <asp:Label ID="lbnRetirement" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
            </tr>
            <tr>
                <td id="td39" runat="server">
                    <asp:Label runat="server" ID="lbNoticePersonalIncomeTax" Text="Thông báo quyết toán thuế thu nhập cá nhân:"></asp:Label>
                </td>
                <td class="lb" id="td39_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=39" target="_blank">
                       <asp:Label ID="lbnNoticePersonalIncomeTax" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
                <td id="td39_3" runat="server"></td>
                <td id="td14" runat="server">
                    <asp:Label runat="server" ID="lbNVTT" Text="Nghỉ việc trong tháng:"></asp:Label>
                </td>
                <td class="lb" id="td14_2" runat="server">
                    <a href="../../Default.aspx?mid=Profile&fid=ctrlHomeDashboard&group=Shared&kindRemind=14" target="_blank">
                        <asp:Label ID="lbReminder14" runat="server" CssClass="lblInfo" />
                    </a>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function POPUP(value) {
            var RadWinManager = GetRadWindow().get_windowManager();
            RadWinManager.set_visibleTitlebar(true);
            var oWindow = RadWinManager.open(value);
            var pos = $("html").offset();
            oWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close); //set the desired behavioursvar pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(parent.document.body.clientWidth, parent.document.body.clientHeight);
            setTimeout(function () { oWindow.setActive(true); }, 0);
            return false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }

    </script>
</tlk:RadScriptBlock>
