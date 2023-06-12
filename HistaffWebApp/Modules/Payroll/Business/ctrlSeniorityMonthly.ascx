<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSeniorityMonthly.ascx.vb"
    Inherits="Payroll.ctrlSeniorityMonthly" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarTerminates" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="70px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server" SkinID="dDropdownList" AutoPostBack="true"
                                Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server" SkinID="dDropdownList" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneBotton" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp bậc nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số năm thâm niên %>" DataField="SENIORITY"
                                SortExpression="SENIORITY" UniqueName="SENIORITY" DataFormatString="{0:n2}" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ mức thưởng phê duyệt %>" DataField="PERCENT_SALARY"
                                SortExpression="PERCENT_SALARY" UniqueName="PERCENT_SALARY" AllowFiltering=false />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày công hưởng 1.000.000 %>" DataField="DAY_COUNT1"
                                SortExpression="DAY_COUNT1" UniqueName="DAY_COUNT1" DataFormatString="{0:n0}"
                                HeaderStyle-Width="110px" AllowFiltering=false />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày công hưởng 2.000.000 %>" DataField="DAY_COUNT2"
                                SortExpression="DAY_COUNT2" UniqueName="DAY_COUNT2" DataFormatString="{0:n0}"
                                HeaderStyle-Width="110px" AllowFiltering=false />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày công hưởng 3.000.000 %>" DataField="DAY_COUNT3"
                                SortExpression="DAY_COUNT3" UniqueName="DAY_COUNT3" DataFormatString="{0:n0}"
                                HeaderStyle-Width="110px" AllowFiltering=false />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày công hưởng 5.000.000 %>" DataField="DAY_COUNT5"
                                SortExpression="DAY_COUNT5" UniqueName="DAY_COUNT5" DataFormatString="{0:n0}"
                                HeaderStyle-Width="110px" AllowFiltering=false />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng mức thưởng %>" DataField="SALARY_TOTAL"
                                SortExpression="SALARY_TOTAL" UniqueName="SALARY_TOTAL" DataFormatString="{0:n0}" AllowFiltering=false />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }

        }
        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }

        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function UserConfirmation(value) {
            return confirm(value);
        }
    </script>
</tlk:RadCodeBlock>
