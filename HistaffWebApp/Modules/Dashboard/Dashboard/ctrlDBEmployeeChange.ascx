<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBEmployeeChange.ascx.vb"
    Inherits="Profile.ctrlDBEmployeeChange" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<script src="../../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts-more.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/modules/exporting.js" type="text/javascript"></script>
<link href="../../../Styles/userCustom.css" rel="stylesheet" type="text/css">
<style>
    table#RAD_SPLITTER_RadSplitter1,td#RadPaneMain{
        border: none;
    }
    #ctrlDBEmployeeChange_cboType{
        width:250px !important;
    }
</style>
<div style="margin-top: 5px; text-align: left">
    <%# Translate("Thống kê theo: ")%>
    <tlk:RadComboBox ID="cboType" runat="server" AutoPostBack="true" OnClientSelectedIndexChanged="OnClientClicked">
    </tlk:RadComboBox>
    <tlk:RadButton ID="btnMax" Style="margin-top: -2px" runat="server" OnClientClicked="MaxClick"
        Text="Maximize" AutoPostBack="false" SkinID="Demo" Visible="false">
    </tlk:RadButton>
</div>
<%--<tlk:RadChart ID="charData1" runat="server" AutoLayout="True">
    <Appearance>
        <Border Visible="false" />
    </Appearance>
    <Series>
        <telerik:ChartSeries Name="Series 1">
            <Appearance>
                <FillStyle MainColor="150, 150, 150" SecondColor="194, 194, 194">
                </FillStyle>
                <Border Visible="false" />
            </Appearance>
        </telerik:ChartSeries>
    </Series>
    <Legend>
        <Appearance Dimensions-Margins="18%, 1%, 1px, 1px">
            <Border Visible="false" />
        </Appearance>
    </Legend>
    <PlotArea>
        <XAxis>
            <Appearance Color="Silver" MajorTick-Color="Silver">
                <MajorGridLines Color="Silver" />
            </Appearance>
        </XAxis>
        <YAxis>
            <Appearance Color="Silver" MajorTick-Color="Silver" MinorTick-Color="Silver">
                <MajorGridLines Color="Silver" />
                <MinorGridLines Color="224, 224, 224" />
            </Appearance>
        </YAxis>
        <Appearance Dimensions-Margins="18%, 22%, 12%, 10%">
            <FillStyle MainColor="White" SecondColor="White" FillType="Solid">
            </FillStyle>
            <Border Visible="false" />
        </Appearance>
        <EmptySeriesMessage>
            <TextBlock Text="<%$ Translate: Không tồn tại dữ liệu %>">
            </TextBlock>
        </EmptySeriesMessage>
    </PlotArea>
    <ChartTitle>
        <Appearance Position-AlignedPosition="Top">
        </Appearance>
        <TextBlock>
            <Appearance TextProperties-Color="Gray">
            </Appearance>
        </TextBlock>
    </ChartTitle>
</tlk:RadChart>
<tlk:RadChart ID="charData2" runat="server" AutoLayout="True" DefaultType="StackedLine"
    Height="100%" Width="100%" SeriesPalette="Colorful" Visible="False">
    <Appearance>
        <Border Visible="false" />
    </Appearance>
    <Series>
        <telerik:ChartSeries Name="<%$ Translate: Tuyển mới %>" Type="StackedLine">
            <Appearance>
                <FillStyle MainColor="21, 197, 22" SecondColor="241, 241, 241">
                </FillStyle>
                <Border Visible="false" />
            </Appearance>
        </telerik:ChartSeries>
    </Series>
    <Legend>
        <Appearance Dimensions-Margins="18%, 1%, 1px, 1px">
        </Appearance>
    </Legend>
    <PlotArea>
        <XAxis>
            <Appearance Color="Silver" MajorTick-Color="Silver">
                <MajorGridLines Color="Silver" />
            </Appearance>
        </XAxis>
        <YAxis>
            <Appearance Color="Silver" MajorTick-Color="Silver" MinorTick-Color="Silver">
                <MajorGridLines Color="Silver" />
                <MinorGridLines Color="224, 224, 224" />
            </Appearance>
        </YAxis>
        <Appearance Dimensions-Margins="18%, 22%, 12%, 10%" SeriesPalette="Colorful">
            <FillStyle MainColor="White" SecondColor="White" FillType="Solid">
            </FillStyle>
            <Border Visible="false" />
        </Appearance>
        <EmptySeriesMessage>
            <TextBlock Text="<%$ Translate: Không tồn tại dữ liệu %>">
            </TextBlock>
        </EmptySeriesMessage>
    </PlotArea>
    <ChartTitle Visible="False">
        <Appearance Position-AlignedPosition="Top" Visible="False">
        </Appearance>
        <TextBlock Text="">
            <Appearance TextProperties-Color="Gray">
            </Appearance>
        </TextBlock>
    </ChartTitle>
</tlk:RadChart>--%>
<div id="container" style="min-width: 250px; height: 250px; margin: 0 auto">
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
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeChange&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
                break;
            case "Restore":
                oWnd.restore();
                var winW = ($("#WindowMainRegion", parent.document).width() - 16)/3 * 2 - 100;
                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeChange&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
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
                        text: 'Số lượng nhân viên'
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
                    enabled: <%=enabled %>,
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    y: 15,
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
