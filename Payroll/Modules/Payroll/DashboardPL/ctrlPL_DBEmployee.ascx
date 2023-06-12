<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPL_DBEmployee.ascx.vb"
    Inherits="Payroll.ctrlPL_DBEmployee" %>
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
        </tr>
    </table>
    <tlk:RadButton ID="btnMax" Style="margin-top: -2px" runat="server" OnClientClicked="MaxClick"
        Text="Maximize" AutoPostBack="false" SkinID="Demo" Visible="false">
    </tlk:RadButton>
</div>
<div class="box-headchart">
    <span class="headchart"><%# Translate("Nhân sự")%></span>
</div>
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
//                oWnd.setUrl('Dialog.aspx?mid=Payroll&fid=ctrlPL_DBEmployee&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
//                break;
//            case "Restore":
//                oWnd.restore();
//                var winW = ($("#WindowMainRegion", parent.document).width() - 16)/3 * 2 - 100;
//                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
//                oWnd.setUrl('Dialog.aspx?mid=Payroll&fid=ctrlPL_DBEmployee&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
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
                 categories: [
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                '10',
                '11',
                '12'
                ],
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
