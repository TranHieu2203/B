<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlChartDegree.ascx.vb"
    Inherits="Recruitment.ctrlChartDegree" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter4" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar runat="server" ID="tbarMain" OnClientButtonClicking="ClientButtonClicking">
            </tlk:RadToolBar>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane5" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tháng/ Năm")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdTuThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                                DateInput-DateFormat="dd/MM/yyyy">
                            </tlk:RadMonthYearPicker>
                        </td>

                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Xem %>">
                            </tlk:RadButton>
                        </td>
                    </tr>

                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server"  Scrolling="None" Height="400px">
               <tlk:RadChart ID="charData1" runat="server" Width="780px" Height="400px">
            <Appearance>
                <Border Visible="false" />
            </Appearance>
            <Series>
                <telerik:ChartSeries Name="Series 1">
                    <Appearance Shadow-Position="Top">
                        <%-- <FillStyle MainColor="150, 150, 150" SecondColor="194, 194, 194">
                        </FillStyle>--%>
                        <Border Visible="false" />
                    </Appearance>
                    
                </telerik:ChartSeries>
                
            </Series>
            <Series>
                <telerik:ChartSeries Name="Series 2">
                    <Appearance Shadow-Position="Top">
                       
                        <Border Visible="false" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Legend>
                <Appearance>
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
                <Appearance>
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
            </tlk:RadPane>
            
            <tlk:RadPane ID="RadPane6" runat="server"  Scrolling="None" Height="280px">
               <tlk:RadChart ID="RadChart1" runat="server" Width="780px" Height="400px">
            <Appearance>
                <Border Visible="false" />
            </Appearance>
            <Series>
                <telerik:ChartSeries Name="Series 1">
                    <Appearance Shadow-Position="Top">
                        <%-- <FillStyle MainColor="150, 150, 150" SecondColor="194, 194, 194">
                        </FillStyle>--%>
                        <Border Visible="false" />
                    </Appearance>
                    
                </telerik:ChartSeries>
                
            </Series>
            <Series>
                <telerik:ChartSeries Name="Series 2">
                    <Appearance Shadow-Position="Top">
                       
                        <Border Visible="false" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Legend>
                <Appearance>
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
                <Appearance>
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
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>

</tlk:RadSplitter>
<%--<style>
    .container
    {
        height: auto;
        overflow: hidden;
    }
    
    .left-column
    {
        border-right: 1px solid;
        float: left;
        width: 235px;
        height: auto;
    }
    .right-column
    {
        margin-left: 30px;
        overflow: hidden;
        height: auto;
    }
</style>
<div class="container" style="border: 1px solid">
    <div class="left-column">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" BorderStyle="None">
            <tlk:RadPane ID="RadPane1" runat="server" MinWidth="235" Width="235px" Height="440px"
                Scrolling="None" BorderStyle="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </div>
    <div class="right-column">
        <tlk:RadToolBar runat="server" ID="tbarMain" OnClientButtonClicking="ClientButtonClicking">
        </tlk:RadToolBar>
        <tlk:RadChart ID="charData1" runat="server" Width="780px" Height="400px">
            <Appearance>
                <Border Visible="false" />
            </Appearance>
            <Series>
                <telerik:ChartSeries Name="Series 1">
                    <Appearance Shadow-Position="Top">
                        
                        <Border Visible="false" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Series>
                <telerik:ChartSeries Name="Series 2">
                    <Appearance Shadow-Position="Top">
                       
                        <Border Visible="false" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Legend>
                <Appearance>
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
                <Appearance>
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
    </div>
</div>--%>
<script type="text/javascript">

    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        }
    }

    function OnClientAutoSizeEnd() {


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
</script>
