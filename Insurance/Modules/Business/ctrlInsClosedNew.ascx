<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsClosedNew.ascx.vb"
    Inherits="Insurance.ctrlInsClosedNew" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="BottomPanel" runat="server" MinWidth="200" Width="250px" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="40px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tháng biến động")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdEFFECTDATE" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                                DateInput-DateFormat="dd/MMM/yyyy">
                                <DateInput Enabled="false">
                                </DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgAgrising" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
                    AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID nhân viên%>" DataField="EMPLOYEE_ID"
                                UniqueName="EMPLOYEE_ID" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_ID"
                                Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" HeaderStyle-Width="100px" SortExpression="VN_FULLNAME">
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh chính %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="100px" SortExpression="TITLE_NAME">
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" HeaderStyle-Width="100px" SortExpression="STAFF_RANK_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương cơ bản %>" DataField="SAL_BASIC"
                                UniqueName="SAL_BASIC" SortExpression="SAL_BASIC" DataFormatString="{0:n0}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTDATE"
                                UniqueName="EFFECTDATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTDATE">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACT_TYPE_NAME"
                                UniqueName="CONTRACT_TYPE_NAME" SortExpression="CONTRACT_TYPE_NAME">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <%-- <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tháng biến động %>" DataField="DATE_CHANGE"
                                UniqueName="DATE_CHANGE" DataFormatString="{0:MM/yyyy}" SortExpression="DATE_CHANGE">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>--%>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="ISBHXH" ItemStyle-HorizontalAlign="Center"
                                UniqueName="ISBHXH" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="ISBHYT" ItemStyle-HorizontalAlign="Center"
                                UniqueName="ISBHYT" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="ISBHTN" ItemStyle-HorizontalAlign="Center"
                                UniqueName="ISBHTN" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }
        function OnClientClose(oWnd, args) {
            postBack(oWnd.get_navigateUrl());
        }

        function postBack(url) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>
