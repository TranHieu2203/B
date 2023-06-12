<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_CBNSViewRating.ascx.vb"
    Inherits="Performance.ctrlPE_CBNSViewRating" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None" Visible="False">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="70px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                                MaxValue="2900" SkinID="Number" CausesValidation="false" AutoPostBack="true"
                                Width="50%">
                                <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kiểu đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboTypeAss" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPeriod" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2" align="center">
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                AutoPostBack="true" Text="<%$ Translate: Tìm kiếm %>" OnClientClicked="OnClientClicked">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto">
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
         function OnClientClicked(sender, args) {
            enableAjax = false;
        }
         function InIEvent() {
            $(function () {
                // Create the chart
                $('#container').highcharts({
                    credits: {
                        enabled: false
                    },
                    chart: {
                        type: 'column',
                         style: {
                            fontFamily: 'Arial'
                               }
                    },
                    title: {
                        text: '<%=title %>'
                    },
                    xAxis: {
                        type: '<%=titleX %>'
                    },
                    yAxis: {
                        title: {
                            text: '<%=titleY %>'
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    plotOptions: {
                        series: {
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true,
                                format: '{point.y:.0f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:12px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.0f}</b><br/>'
                    },

                    series: [{
                        name: '<%=name %>',
                        colorByPoint: true,
                        data: [<%=data %>]
                    }]
                });
            });
        }
        $(document).ready(InIEvent);
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
