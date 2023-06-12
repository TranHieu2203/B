<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSeniorityProcess.ascx.vb"
    Inherits="Payroll.ctrlSeniorityProcess" %>
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
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 1 %>" DataField="D1" SortExpression="D1"
                                UniqueName="D1" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 2 %>" DataField="D2" SortExpression="D2"
                                UniqueName="D2" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 3 %>" DataField="D3" SortExpression="D3"
                                UniqueName="D3" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 4 %>" DataField="D4" SortExpression="D4"
                                UniqueName="D4" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 5 %>" DataField="D5" SortExpression="D5"
                                UniqueName="D5" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 6 %>" DataField="D6" SortExpression="D6"
                                UniqueName="D6" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 7 %>" DataField="D7" SortExpression="D7"
                                UniqueName="D7" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 8 %>" DataField="D8" SortExpression="D8"
                                UniqueName="D8" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 9 %>" DataField="D9" SortExpression="D9"
                                UniqueName="D9" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 10 %>" DataField="D10" SortExpression="D10"
                                UniqueName="D10" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 11 %>" DataField="D11" SortExpression="D11"
                                UniqueName="D11" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 12 %>" DataField="D12" SortExpression="D12"
                                UniqueName="D12" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 13 %>" DataField="D13" SortExpression="D13"
                                UniqueName="D13" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 14 %>" DataField="D14" SortExpression="D14"
                                UniqueName="D14" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 15 %>" DataField="D15" SortExpression="D15"
                                UniqueName="D15" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 16 %>" DataField="D16" SortExpression="D16"
                                UniqueName="D16" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 17 %>" DataField="D17" SortExpression="D17"
                                UniqueName="D17" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 18 %>" DataField="D18" SortExpression="D18"
                                UniqueName="D18" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 19 %>" DataField="D19" SortExpression="D19"
                                UniqueName="D19" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 20 %>" DataField="D20" SortExpression="D20"
                                UniqueName="D20" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 21 %>" DataField="D21" SortExpression="D21"
                                UniqueName="D21" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 22 %>" DataField="D22" SortExpression="D22"
                                UniqueName="D22" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 23 %>" DataField="D23" SortExpression="D23"
                                UniqueName="D23" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 24 %>" DataField="D24" SortExpression="D24"
                                UniqueName="D24" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 25 %>" DataField="D25" SortExpression="D25"
                                UniqueName="D25" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 26 %>" DataField="D26" SortExpression="D26"
                                UniqueName="D26" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 27 %>" DataField="D27" SortExpression="D27"
                                UniqueName="D27" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 28 %>" DataField="D28" SortExpression="D28"
                                UniqueName="D28" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 29 %>" DataField="D29" SortExpression="D29"
                                UniqueName="D29" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 30 %>" DataField="D30" SortExpression="D30"
                                UniqueName="D30" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 31 %>" DataField="D31" SortExpression="D31"
                                UniqueName="D31" DataFormatString="{0:n2}" HeaderStyle-Width="60px" AllowFiltering="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức thưởng tối đa %>" DataField="SALARY"
                                SortExpression="SALARY" UniqueName="SALARY" DataFormatString="{0:n0}" AllowFiltering="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng vi phạm trong tháng %>" DataField="DISCIPLINE_COUNT"
                                SortExpression="DISCIPLINE_COUNT" UniqueName="DISCIPLINE_COUNT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức kỷ luật %>" DataField="DISCIPLINE_TYPE_NAME"
                                SortExpression="DISCIPLINE_TYPE_NAME" UniqueName="DISCIPLINE_TYPE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: % Mức thưởng phê duyệt %>" DataField="PERCENT_SALARY"
                                SortExpression="PERCENT_SALARY" UniqueName="PERCENT_SALARY" AllowFiltering="false" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {

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
