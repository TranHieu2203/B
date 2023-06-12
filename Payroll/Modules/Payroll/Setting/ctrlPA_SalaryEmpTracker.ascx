<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryEmpTracker.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryEmpTracker" %>
<%@ Import Namespace="Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidOrgName" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="350px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" AutoPostBack="false"/>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="none">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None" Visible="false">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="40px" Scrolling="None">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtYear" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" SkinID="ButtonFind" OnClientClicked="OnClientClicked">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server">
                <div style="text-align: center">
                  <%--  <tlk:RadChart ID="charData1" runat="server" AutoLayout="True" DefaultType="Bar" Width="800px"
                        SeriesPalette="Colorful">
                        <Appearance>
                            <Border Visible="false" />
                        </Appearance>
                        <Series>
                            <telerik:ChartSeries Name="<%$ Translate: Tổng quỹ lương theo định biên %>" Type="Bar">
                                <Appearance>
                                    <FillStyle MainColor="21, 197, 22" SecondColor="241, 241, 241">
                                    </FillStyle>
                                    <Border Visible="false" />
                                </Appearance>
                            </telerik:ChartSeries>
                            <telerik:ChartSeries Name="<%$ Translate: Tổng chi phí lương thực tế %>" Type="Bar">
                                <Appearance>
                                    <FillStyle MainColor="255, 186, 74" SecondColor="194, 194, 194">
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
                        <ChartTitle>
                            <Appearance Position-AlignedPosition="Top">
                            </Appearance>
                            <TextBlock Text="Quỹ lương">
                                <Appearance TextProperties-Color="Gray">
                                </Appearance>
                            </TextBlock>
                        </ChartTitle>
                    </tlk:RadChart>--%>
                    <div id="container" style="min-width: 250px; height: 250px; margin: 0 auto">
                    </div>
                    <hr />
                   <%-- <tlk:RadChart ID="charData2" runat="server" AutoLayout="True" DefaultType="Bar" Width="800px"
                        SeriesPalette="Colorful">
                        <Appearance>
                            <Border Visible="false" />
                        </Appearance>
                        <Series>
                            <telerik:ChartSeries Name="<%$ Translate: Tổng nhân sự định biên %>" Type="Bar">
                                <Appearance>
                                    <FillStyle MainColor="21, 197, 22" SecondColor="241, 241, 241">
                                    </FillStyle>
                                    <Border Visible="false" />
                                </Appearance>
                            </telerik:ChartSeries>
                            <telerik:ChartSeries Name="<%$ Translate: Tổng nhân sự thực tế %>" Type="Bar">
                                <Appearance>
                                    <FillStyle MainColor="255, 186, 74" SecondColor="194, 194, 194">
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
                        <ChartTitle>
                            <Appearance Position-AlignedPosition="Top">
                            </Appearance>
                            <TextBlock Text="Nhân sự">
                                <Appearance TextProperties-Color="Gray">
                                </Appearance>
                            </TextBlock>
                        </ChartTitle>
                    </tlk:RadChart>--%>
                    <div id="container1" style="min-width: 250px; height: 250px; margin: 0 auto">
                    </div>
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {

        }

        function OpenEdit() {

        }

        function clientButtonClicking(sender, args) {

        }

        function OnClientClose(oWnd, args) {

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
                    type: 'column'
                },
                    title: {
                    text: 'Quỹ lương'
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
                        text: 'Số tiền'
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
            }
            },
                series: [<%=data %>]
            });


             $('#container1').highcharts({
                credits: {
                    enabled: false
                },
                chart: {
                    type: 'column'
                },
                    title: {
                    text: 'Nhân sự'
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
                series: [<%=data1 %>]
            });
        });
    }
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
</tlk:RadCodeBlock>
