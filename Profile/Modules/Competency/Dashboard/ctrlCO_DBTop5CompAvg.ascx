<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCO_DBTop5CompAvg.ascx.vb"
    Inherits="Profile.ctrlCO_DBTop5CompAvg" %>
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
    <span class="headchart"><%# Translate("Trung bình năng lực cá nhân top 5 năng lực")%></span>
</div>
<div id="container" style="min-width: 225px; height: 225px; margin: 0 auto">
</div>
<script type="text/javascript">
    var enableAjax = true;

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
                
                $('#container').highcharts({
                    credits: {
                        enabled: false
                    },
                    chart: {
                        polar: true,
                        type: 'line',
                        style: {
                            fontFamily: 'Arial'
                               },
                    },
                    title: {
                        text: ''
                    },

                    pane: {
                        size: '90%'
                    },

                    xAxis: {
                        categories: [<%=categories %>],
                        tickmarkPlacement: 'on',
                        lineWidth: 0
                    },

                    yAxis: {
                        gridLineInterpolation: 'polygon',
                        lineWidth: 0,
                        min: 0
                    },

                    tooltip: {
                        shared: true,
                        pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.1f}</b><br/>'
                    },
                    legend: {
                        align: 'center',
                        verticalAlign: 'top',
                        y: 30,
                        layout: 'horizontal'
                    },

                    series: [{
                        name: '<%=name1 %>',
                        data: [<%=dataset1 %>],
                        color: '#33FF99',
                        pointPlacement: 'on'
                    }, {
                        name: '<%=name2 %>',
                        data: [<%=dataset2 %>],
                        color: '#FF6666',
                        pointPlacement: 'on'
                    }]
                });
            });
            }  
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
