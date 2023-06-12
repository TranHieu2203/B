<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_DBTimeOtByOrg.ascx.vb"
    Inherits="Attendance.ctrlAT_DBTimeOtByOrg" %>
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
    <%# Translate("Số giờ làm thêm (giờ)")%></span>
</div>--%>
<div id="container" style="min-width: 200px; height: 220px; margin: 0 auto">
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
//                oWnd.setUrl('Dialog.aspx?mid=Attendance&fid=ctrlAT_DBTimeOtByOrg&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
//                break;
//            case "Restore":
//                oWnd.restore();
//                var winW = ($("#WindowMainRegion", parent.document).width() - 16)/3 * 2 - 100;
//                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
//                oWnd.setUrl('Dialog.aspx?mid=Attendance&fid=ctrlAT_DBTimeOtByOrg&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
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
                    type: '<%=type %>'
                },
                    title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                <%=categories %>
                yAxis: {
                    title: {
                        text: ''
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
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.0f}',
                            style: { fontWeight: 'normal' }
                        }
                    }
                },
                legend: {
                    enabled: <%=enabled %>,
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0,
                    itemStyle: {
                        fontSize: '12px',
                        fontWeight: 'normal',
                        color: '#767676'
                        },
                },
                exporting: {
                    sourceWidth: 900,
                    sourceHeight: 300,
                    chartOptions: {
                        subtitle: null
                    }
                },
                series: [<%=data %>],                  
                 title: {
                            text:'<span style="font-weight: bold;color: #767676;font-size:12px;">Số giờ làm thêm (giờ)</span>'
                        }
            });
        });
    }
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
