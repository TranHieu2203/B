<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryTracker.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryTracker" %>
<%@ Import Namespace="Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidOrgName" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
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
                        <td class="lb">
                            <%# Translate("Tháng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtMonth" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" SkinID="ButtonFind"
                                OnClientClicked="OnClientClicked">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <div id="container" style="min-width: 250px; height: 250px; margin: 0 auto">
                </div>
                <%--                <tlk:RadChart ID="charData1" runat="server" DefaultType="Pie" AutoLayout="True" Width="600px">
                    <Series>
                        <telerik:ChartSeries Name="Series 1" Type="Pie" DataYColumn="VALUE">
                            <Appearance>
                                <FillStyle MainColor="90, 90, 235" SecondColor="237, 140, 20" FillType="ComplexGradient">
                                </FillStyle>
                                <Border Color="Transparent" />
                            </Appearance>
                        </telerik:ChartSeries>
                    </Series>
                    <Appearance>
                        <Border Visible="false" />
                    </Appearance>
                    <PlotArea>
                        <XAxis>
                            <Appearance Color="Silver" MajorTick-Color="Silver">
                                <MajorGridLines Color="Silver" />
                            </Appearance>
                        </XAxis>
                        <YAxis>
                            <Appearance Color="Black" MajorTick-Color="Black" MinorTick-Color="Black">
                                <MajorGridLines Color="Black" />
                                <MinorGridLines Color="224, 224, 224" />
                            </Appearance>
                        </YAxis>
                        <Appearance>
                            <FillStyle MainColor="White" SecondColor="Black" FillType="Solid">
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
                            <Border Visible="false" />
                        </Appearance>
                        <TextBlock>
                            <Appearance TextProperties-Color="Gray">
                            </Appearance>
                        </TextBlock>
                    </ChartTitle>
                </tlk:RadChart>--%>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="200px">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Nhân viên hiện tại %>" DataField="EMP_CUR"
                                SortExpression="EMP_CUR" UniqueName="EMP_CUR" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Định biên nhân sự %>" DataField="EMP_NUMBER"
                                SortExpression="EMP_NUMBER" UniqueName="EMP_NUMBER" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Quỹ lương theo định biên %>" DataField="TOTAL"
                                SortExpression="TOTAL" UniqueName="TOTAL" DataFormatString="{0:n0}" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
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
            var year = $find('<%# rntxtYear.ClientID %>').get_value();
            var org = $("#" + '<%# hidOrg.ClientID %>').val();
            var orgName = $("#" + '<%# hidOrgName.ClientID %>').val();
            var oWindow = radopen('Dialog.aspx?mid=Payroll&fid=ctrlPA_SalaryPlanNewEdit&group=Setting&noscroll=1&year=' + year + '&org_id=' + org + '&Org_Name=' + orgName, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(800, 400);
            oWindow.center();
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Payroll&fid=ctrlPA_SalaryPlanNewEdit&group=Setting&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(800, 400);
            oWindow.center();
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE' ||
            args.get_item().get_commandName() == 'APROVE' ||
            args.get_item().get_commandName() == 'REJECT') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
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
                    align: 'right',
                    verticalAlign: 'center',
                    y: 100,
                    x: -100,
                    padding: 0,
                    itemMarginTop: 0,
                    itemMarginBottom: 0,
                    itemStyle: {
                        fontSize: '12px',
                        fontWeight: 'normal',
                        color: '#767676'
                        },
                    layout: 'vertical'
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