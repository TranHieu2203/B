<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBInfo.ascx.vb"
    Inherits="Profile.ctrlDBInfo" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="95%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="LeftPane" runat="server" Scrolling="None">
        <table class="table-form">
            <tr>
                <td colspan="5">
                    <span class="item-head"><i class="fa fa-bar-chart"></i>
                        <%# Translate("Thống kê nhanh")%></span>
                </td>
            </tr>
            <tr class="crum-top">
                <td>
                    <%# Translate("Tổng số nhân viên hiện tại:")%>
                </td>
                <td style="width: 50px" class="lb">
                    <asp:Label ID="lbtnEmpCount" runat="server" CssClass="lblInfo" />
                </td>
                <td style="width: 20px" class="lb">
                </td>
                <td>
                    <%# Translate("Nhân viên tuyển mới trong tháng:")%>
                </td>
                <td style="width: 50px" class="lb">
                    <asp:Label ID="lbtnEmpNew" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Nhân viên nghỉ việc trong tháng:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnEmpTer" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Hợp đồng tạo mới trong tháng:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnContractNew" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Lao động chuyển đi:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnTransferNew" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Lao động chuyển đến:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnTransferMove" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr class="crum-bottom">
                <td>
                    <%# Translate("Tuổi bình quân:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnAgeAvg" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Thâm niên bình quân (năm):")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbtnSeniority" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td colspan="5" style="padding-top: 20px;">
                    <span class="item-head"><i class="fa fa-exclamation-circle"></i>
                        <%# Translate("Nhắc nhở")%></span>
                </td>
            </tr>
            <tr class="crum-top">
                <td>
                    <%# Translate("Hết hạn hợp đồng:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder1" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Sắp đến sinh nhật:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder2" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Chưa nộp đủ giấy tờ khi tiếp nhận:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder16" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Hết hạn Visa:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder5" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Hết hạn tờ trình:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder13" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Giấy phép lao động:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder19" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Chứng chỉ lao động:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder20" runat="server" CssClass="lblInfo" />
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Nghỉ việc trong tháng:")%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbReminder14" runat="server" CssClass="lblInfo" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgContract" runat="server" Height="100%" AllowFilteringByColumn="true"
            SkinID="GridSingleSelect">
            <MasterTableView CommandItemDisplay="Top" ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,LINK_POPUP">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true">
                </CommandItemSettings>
                <GroupByExpressions>
                    <tlk:GridGroupByExpression>
                        <SelectFields>
                            <tlk:GridGroupByField FieldAlias="Nhóm" FieldName="REMIND_NAME"></tlk:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <tlk:GridGroupByField FieldName="REMIND_NAME"></tlk:GridGroupByField>
                        </GroupByFields>
                    </tlk:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <tlk:GridTemplateColumn UniqueName="LINK_POPUP" AllowFiltering="false" HeaderText="<%$ Translate: Liên kết nhanh %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnLink" runat="server" Text="View">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="90px" />
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nhắc nhở %>" DataField="REMIND_NAME"
                        UniqueName="REMIND_NAME" SortExpression="REMIND_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnCommand="RaiseCommand" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function POPUP(value) {
            var RadWinManager = GetRadWindow().get_windowManager();
            RadWinManager.set_visibleTitlebar(true);
            var oWindow = RadWinManager.open(value);
            var pos = $("html").offset();
            oWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close); //set the desired behavioursvar pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(parent.document.body.clientWidth, parent.document.body.clientHeight);
            setTimeout(function () { oWindow.setActive(true); }, 0);
            return false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }

    </script>
</tlk:RadScriptBlock>
