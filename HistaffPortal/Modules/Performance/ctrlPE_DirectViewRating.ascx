<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_DirectViewRating.ascx.vb"
    Inherits="Performance.ctrlPE_DirectViewRating" %>
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <td class="lb">
                <%# Translate("Năm đánh giá")%>
            </td>
            <td>
                <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="2900" SkinID="Number" CausesValidation="false" AutoPostBack="true" Width="50%">
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
                <tlk:RadComboBox runat="server" ID="cboPeriod" CausesValidation="false">
                </tlk:RadComboBox>
            </td>
            <td colspan="2" align="center">
              <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false" AutoPostBack="true"
                        Text="<%$ Translate: Tìm kiếm %>" OnClientClicked="OnClientClicked">
                    </tlk:RadButton>
            </td>
        </tr>
    </table>
    <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto">
    </div>
</div>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
    var enableAjax = true;
        function clientButtonClicking(sender, args) {
        }
         function OnClientClicked(sender, args) {
            enableAjax = false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
