<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_DBTotalWorking.ascx.vb"
    Inherits="Attendance.ctrlAT_DBTotalWorking" %>
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
                <tlk:RadComboBox runat="server" ID="cboYear" CausesValidation="false" AutoPostBack="true"
                    OnClientSelectedIndexChanged="OnClientClicked" Width="80px" >
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Tháng")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboMonth" CausesValidation="false" AutoPostBack="true"
                    OnClientSelectedIndexChanged="OnClientClicked" Width="80px">
                </tlk:RadComboBox>
            </td>
        </tr>
    </table>
    <tlk:RadButton ID="btnMax" Style="margin-top: -2px" runat="server" OnClientClicked="MaxClick"
        Text="Maximize" AutoPostBack="false" SkinID="Demo" Visible="false">
    </tlk:RadButton>
</div>
<%--<div class="box-headchart">
    <span class="headchart">
        <%# Translate("Tổng công nghỉ/Tổng công thực tế (ngày công)")%></span>
</div>--%>
<div id="container" style="min-width: 225px; height: 225px; margin: 0 auto">
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
//                oWnd.setUrl('Dialog.aspx?mid=Attendance&fid=ctrlAt_DBTotalWorking&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
//                break;
//            case "Restore":
//                oWnd.restore();
//                var winW = ($("#WindowMainRegion", parent.document).width() - 16)/3 * 2 - 100;
//                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
//                oWnd.setUrl('Dialog.aspx?mid=Attendance&fid=ctrlAt_DBTotalWorking&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
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
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.0f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: true
                    }
                },
                legend: {
                    enabled: true,
                    align: 'center',
                    verticalAlign: 'right',
                    y: 190,
                    x: -10,
                    padding: 0,
                    itemMarginTop: 0,
                    itemMarginBottom: 0,
                    itemStyle: {
                        fontSize: '12px',
                        fontWeight: 'normal',
                        color: '#767676'
                        },
                    layout: 'horizontal'
                },
                exporting: {
                    sourceWidth: 900,
                    sourceHeight: 300,
                    // scale: 2 (default)
                    chartOptions: {
                        subtitle: null
                    }
                },
                series: [{
                    name: '<%=name %>',
                    colorByPoint: true,
                    data: [<%=data %>]
                }],                  
                        title: {
                            text:'<span style="font-weight: bold;color: #767676;font-size:12px;">Tổng công nghỉ/Tổng công thực tế (ngày công)</span> '
                        }
            });
        });
    }
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
