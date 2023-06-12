<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlChartGender.ascx.vb"
    Inherits="Recruitment.ctrlChartGender" %>
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
            <tlk:RadPane ID="RadPane4" runat="server"  Scrolling="None" Height="280px">
                <tlk:RadChart ID="charData1" runat="server" DefaultType="Pie" Width="680px" Height="300px">
             <Appearance>
                <Border Visible="false" />
            </Appearance>
            <Series>
                <telerik:ChartSeries DataYColumn="VALUE" Name="Series 1" Type="Pie">
                    <Appearance>
                        <FillStyle FillType="ComplexGradient" MainColor="150, 150, 150" SecondColor="194, 194, 194">
                        </FillStyle>
                        <LabelAppearance Dimensions-Paddings="0px,0px,0px,0px">
                        </LabelAppearance>
                        <TextAppearance Dimensions-Paddings="10px,10px,10px,10px">
                        </TextAppearance>
                        <Border Visible="false" />
                        <Border Visible="False" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Legend>
                <Appearance Dimensions-Margins="1%, 1%, 1%, 1%">
                </Appearance>
            </Legend>
            <PlotArea>
                <EmptySeriesMessage>
                    <TextBlock Text="<%$ Translate: Không tồn tại dữ liệu %>">
                    </TextBlock>
                </EmptySeriesMessage>
                <XAxis>
                    <Appearance Color="Silver" MajorTick-Color="Silver">
                        <MajorGridLines Color="Silver" />
                        <MajorGridLines Color="Silver" />
                    </Appearance>
                </XAxis>
                <YAxis>
                    <Appearance Color="Black" MajorTick-Color="Black" MinorTick-Color="Black">
                        <MajorGridLines Color="Black" />
                        <MinorGridLines Color="224, 224, 224" />
                        <MajorGridLines Color="Black" />
                        <MinorGridLines Color="224, 224, 224" />
                    </Appearance>
                </YAxis>
                <Appearance Dimensions-Margins="54px, 87px, 27px, 42px">
                    <Border Visible="false" />
                    <FillStyle FillType="Solid" MainColor="White" SecondColor="Black">
                    </FillStyle>
                    <Border Visible="False" />
                </Appearance>
            </PlotArea>
            <ChartTitle>
                <Appearance Dimensions-Margins="1%, 1%, 1%, 1%" Position-AlignedPosition="Top">
                    <Border Visible="false" />
                    <Border Visible="False" />
                </Appearance>
                <TextBlock>
                    <Appearance TextProperties-Color="Gray">
                    </Appearance>
                </TextBlock>
            </ChartTitle>
        </tlk:RadChart>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane6" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowMultiRowSelection="True">
                    <MasterTableView DataKeyNames="ORG_ID" ClientDataKeyNames="ORG_ID">
                        <Columns>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                HeaderStyle-Width="100px" UniqueName="TITLE_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhân viên hiện tại %>" DataField="EMP_COUNT" SortExpression="EMP_COUNT"
                                HeaderStyle-Width="100px" UniqueName="EMP_COUNT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Định biên nhân sự %>" DataField="MONTH_HRP_DETAIL" SortExpression="MONTH_HRP_DETAIL"
                                HeaderStyle-Width="100px" UniqueName="MONTH_HRP_DETAIL" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quỹ lương thực tế %>" DataField="ACTUAL_WAGE_FUND" SortExpression="ACTUAL_WAGE_FUND"
                                HeaderStyle-Width="100px" UniqueName="ACTUAL_WAGE_FUND" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Quỹ lương theo định biên %>" DataField="WAGE_FUNDS_PLAN" SortExpression="WAGE_FUNDS_PLAN"
                                HeaderStyle-Width="100px" UniqueName="WAGE_FUNDS_PLAN" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Quỹ lương còn lại %>" DataField="WAGE_FUNDS" SortExpression="WAGE_FUNDS"
                                HeaderStyle-Width="100px" UniqueName="WAGE_FUNDS" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                          
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>

        </tlk:RadSplitter>
    </tlk:RadPane>

</tlk:RadSplitter>



<%--
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">

    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
        <tlk:RadToolBar runat="server" ID="tbarMain" OnClientButtonClicking="ClientButtonClicking">
            </tlk:RadToolBar>
        <tlk:RadChart ID="charData1" runat="server" DefaultType="Pie" Width="680px" Height="400px">
             <Appearance>
                <Border Visible="false" />
            </Appearance>
            <Series>
                <telerik:ChartSeries DataYColumn="VALUE" Name="Series 1" Type="Pie">
                    <Appearance>
                        <FillStyle FillType="ComplexGradient" MainColor="150, 150, 150" SecondColor="194, 194, 194">
                        </FillStyle>
                        <LabelAppearance Dimensions-Paddings="0px,0px,0px,0px">
                        </LabelAppearance>
                        <TextAppearance Dimensions-Paddings="10px,10px,10px,10px">
                        </TextAppearance>
                        <Border Visible="false" />
                        <Border Visible="False" />
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <Legend>
                <Appearance Dimensions-Margins="1%, 1%, 1%, 1%">
                </Appearance>
            </Legend>
            <PlotArea>
                <EmptySeriesMessage>
                    <TextBlock Text="<%$ Translate: Không tồn tại dữ liệu %>">
                    </TextBlock>
                </EmptySeriesMessage>
                <XAxis>
                    <Appearance Color="Silver" MajorTick-Color="Silver">
                        <MajorGridLines Color="Silver" />
                        <MajorGridLines Color="Silver" />
                    </Appearance>
                </XAxis>
                <YAxis>
                    <Appearance Color="Black" MajorTick-Color="Black" MinorTick-Color="Black">
                        <MajorGridLines Color="Black" />
                        <MinorGridLines Color="224, 224, 224" />
                        <MajorGridLines Color="Black" />
                        <MinorGridLines Color="224, 224, 224" />
                    </Appearance>
                </YAxis>
                <Appearance Dimensions-Margins="54px, 87px, 27px, 42px">
                    <Border Visible="false" />
                    <FillStyle FillType="Solid" MainColor="White" SecondColor="Black">
                    </FillStyle>
                    <Border Visible="False" />
                </Appearance>
            </PlotArea>
            <ChartTitle>
                <Appearance Dimensions-Margins="1%, 1%, 1%, 1%" Position-AlignedPosition="Top">
                    <Border Visible="false" />
                    <Border Visible="False" />
                </Appearance>
                <TextBlock>
                    <Appearance TextProperties-Color="Gray">
                    </Appearance>
                </TextBlock>
            </ChartTitle>
        </tlk:RadChart>
</tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>--%>
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
    function rbtClicked(sender, eventArgs) {
        enableAjax = false;
    }
</script>
