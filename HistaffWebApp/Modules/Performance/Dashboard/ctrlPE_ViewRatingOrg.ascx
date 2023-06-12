<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_ViewRatingOrg.ascx.vb"
    Inherits="Performance.ctrlPE_ViewRatingOrg" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<script src="../../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts-more.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/modules/exporting.js" type="text/javascript"></script>
<div class="chart-top">
    <table class="table-form">
        <tr>
            <td class="lb">
                <%# Translate("Năm")%>
            </td>
            <td>
                <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="2900" SkinID="Number" CausesValidation="false" AutoPostBack="true" Width="70px">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>
            </td>
            <td class="lb">
                <%# Translate("Kiểu đánh giá")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboTypeAss" CausesValidation="false" AutoPostBack="true" Width="100px">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Kỳ đánh giá")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboPeriod" CausesValidation="false" AutoPostBack="true" OnClientSelectedIndexChanged="OnClientClicked" Width="100px">
                </tlk:RadComboBox>
            </td>
        </tr>
    </table>
    <tlk:RadButton ID="btnMax" Style="margin-top: -2px" runat="server" OnClientClicked="MaxClick"
        Text="Maximize" AutoPostBack="false" SkinID="Demo" Visible="false">
    </tlk:RadButton>
</div>
<div class="box-headchart">
    <span class="headchart">
        <%# Translate("Xếp hạng nhân viên")%></span>
</div>
<div id="container" style="min-width: 220px; height: 220px; margin: 0 auto">
</div>
<script type="text/javascript">
    var enableAjax = true;
//    function MaxClick(sender, e) {
//        var btn = $find('<%= btnMax.ClientId %>');
//        var btnText = btn.get_text();
//        oWnd = getRadWindow();
//        switch (btnText) {
//            case "Maximize":
//                oWnd.maximize();
//                var winW = $("#WindowMainRegion", parent.document).width() - 16;
//                var winH = $("#WindowMainRegion", parent.document).height() + 91 - 10;
//                oWnd.setUrl('Dialog.aspx?mid=Performance&fid=ctrlPE_ViewRatingOrg&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
//                break;
//            case "Restore":
//                oWnd.restore();
//                var winW = ($("#WindowMainRegion", parent.document).width() - 16)/3 * 2 - 100;
//                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
//                oWnd.setUrl('Dialog.aspx?mid=Performance&fid=ctrlPE_ViewRatingOrg&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
//                break;
//            default:
//                break;

//        }
//    }

    function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    function OnClientAutoSizeEnd() {


    }
     function OnClientClicked(sender, args) {
            enableAjax = false;
     }
    //mandatory for the RadWindow dialogs functionality
    function getRadWindow() {
        if (window.radWindow) {
            return window.radWindow;
        }
        if (window.frameElement && window.frameElement.radWindow) {
            return window.frameElement.radWindow;
        }
        return null;
    }
     function InIEvent() {
        $(function () {
            // Create the chart
             $('#container').highcharts({
                credits: {
                    enabled: false
                },
                chart: {
                    type: 'column'
                },
                    title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
              xAxis: {
                 categories: [<%=categories %>],
                crosshair: true
            },
                yAxis: {
                    title: {
                        text: 'Số người'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: ''
                },
                legend: {
                    borderWidth: 0,
                    itemStyle: {
                        fontSize: '12px',
                        fontWeight: 'normal',
                        color: '#767676'
                        },
                },
                plotOptions: {
                column: {
                pointPadding: 0.2,
                borderWidth: 0
                },
                series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    style: { fontWeight: 'normal' }
                    }
                }
               },
                series: [<%=data %>]
            });
     });
    }
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
