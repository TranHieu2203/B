<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_DBEstimateReality.ascx.vb"
    Inherits="Recruitment.ctrlRC_DBEstimateReality" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<script src="../../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts-more.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/modules/exporting.js" type="text/javascript"></script>
<div class="chart-top">
    <%# Translate("Năm: ")%>
    <%=_year %>
    <tlk:RadComboBox ID="cboType" SkinID="dDropdownList" runat="server" AutoPostBack="true"
        Width="80px" OnClientSelectedIndexChanged="OnClientClicked" Visible="false">
    </tlk:RadComboBox>
    <tlk:RadButton ID="btnMax" Style="margin-top: -2px" runat="server" OnClientClicked="MaxClick"
        Text="Maximize" AutoPostBack="false" SkinID="Demo" Visible="false">
    </tlk:RadButton>
</div>
<div class="box-headchart">
    <span class="headchart">
        <%# Translate("Báo cáo so sánh chi phí tuyển dụng kế hoạch và thực tế")%></span>
</div>
<div id="container" style="min-width: 225px; height: 225px; margin: 0 auto">
</div>
<script type="text/javascript">
    var enableAjax = true;
    function MaxClick(sender, e) {
        var btn = $find('<%= btnMax.ClientId %>');
        var btnText = btn.get_text();
        oWnd = getRadWindow();
        switch (btnText) {
            case "Maximize":
                oWnd.maximize();
                var winW = $("#WindowMainRegion", parent.document).width() - 16;
                var winH = $("#WindowMainRegion", parent.document).height() + 91 - 10;
                oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBEstimateReality&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
                break;
            case "Restore":
                oWnd.restore();
                var winW = ($("#WindowMainRegion", parent.document).width() - 16)/3 * 2 - 100;
                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
                oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlRC_DBEstimateReality&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
                break;
            default:
                break;

        }
    }

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
                    zoomType: 'xy'
                },
                    title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                <%=categories %>
                yAxis: [{ // Primary yAxis Chi phí tuyển dụng thực tế
                    labels: {
                    format: '{value} triệu đồng',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                    },
                    title: {
                        text: 'Chi phí tuyển dụng thực tế',
                        style: {
                    color: Highcharts.getOptions().colors[1]
                    }
                    }
                },
                { // Secondary yAxis
                    title: {
                    text: 'Chi phí tuyển dụng kế hoạch',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                    },
                    labels: {
                    format: '{value} triệu đồng',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
            opposite: true
            }],

                tooltip: {
                        shared: true
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
                series: [<%=data %>]
            });
        });
    }
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
