<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsRemigeManager.ascx.vb"
    Inherits="Insurance.ctrlInsRemigeManager" %>
<%@ Import Namespace="Common" %>
      <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="800px" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane5" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <asp:HiddenField ID="hidID" runat="server" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Tới ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
                    AllowPaging="True">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="70px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban%>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ BHXH%>" DataField="INSURRANCE_NUM"
                                SortExpression="INSURRANCE_NUM" UniqueName="INSURRANCE_NUM" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số thẻ BHYT%>" DataField="BOOK_NUM"
                                SortExpression="BOOK_NUM" UniqueName="BOOK_NUM" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại chế độ%>" DataField="ENTITLED_NAME"
                                SortExpression="ENTITLED_NAME" UniqueName="ENTITLED_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn UniqueName="IS_ATHOME" DataField="IS_ATHOME" HeaderText="<%$ Translate: Nghỉ tại nhà %>"
                                SortExpression="IS_ATHOME" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn UniqueName="is_FOCUSED" DataField="is_FOCUSED" HeaderText="<%$ Translate: Nghỉ tập trung %>"
                                SortExpression="is_FOCUSED" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE" UniqueName="START_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE" UniqueName="END_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                           <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tháng biến động %>" DataField="CHANGE_MONTH"
                                SortExpression="CHANGE_MONTH" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/yyyy}"
                                PickerType ="YearPicker" UniqueName="CHANGE_MONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>--%>
                            <tlk:GridTemplateColumn DataField="CHANGE_MONTH"
                                 SortExpression="CHANGE_MONTH"
                                UniqueName="CHANGE_MONTH" HeaderText="<%$ Translate: Tháng biến động %>" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <%# Eval("CHANGE_MONTH", "{0:MM/yyyy}") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadMonthYearPicker ID="OkMonthYearPicker" Runat="server" DbSelectedDate='<%#Bind("CHANGE_MONTH") %>' 
                                            DateInput-Width="120px">
                                    </tlk:RadMonthYearPicker>
                                </EditItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày %>" DataField="NUM_DATE"
                                SortExpression="NUM_DATE" UniqueName="NUM_DATE" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh con %>" DataField="DATE_OFF_BIRT"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="DATE_OFF_BIRT" UniqueName="DATE_OFF_BIRT">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên con %>" DataField="BABY_NAME"
                                SortExpression="BABY_NAME" UniqueName="BABY_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Con thứ mấy %>" DataField="NUM_BABY"
                                SortExpression="NUM_BABY" UniqueName="NUM_BABY" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày lũy kế %>" DataField="ACCUMULATED_DATE"
                                SortExpression="ACCUMULATED_DATE" UniqueName="ACCUMULATED_DATE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền lương tính trợ cấp %>" DataField="ALLOWANCE_SAL"
                                DataFormatString="{0:N0}" SortExpression="ALLOWANCE_SAL" UniqueName="ALLOWANCE_SAL" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền trợ cấp %>" DataField="ALLOWANCE_MONEY"
                                DataFormatString="{0:N0}" SortExpression="ALLOWANCE_MONEY" UniqueName="ALLOWANCE_MONEY" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền trợ cấp chỉnh sửa %>" DataField="ALLOWANCE_MONEY_EDIT"
                                DataFormatString="{0:N0}" SortExpression="ALLOWANCE_MONEY_EDIT" UniqueName="ALLOWANCE_MONEY_EDIT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời điểm tính trợ cấp %>" DataField="TIME_ALLOWANCE"
                                SortExpression="TIME_ALLOWANCE" UniqueName="TIME_ALLOWANCE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều kiện tính trợ cấp %>" DataField="CONDITION_ALLOWANCE"
                                SortExpression="CONDITION_ALLOWANCE" UniqueName="CONDITION_ALLOWANCE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày bị xuất toán  %>" DataField="APPROVAL_NUM"
                                SortExpression="APPROVAL_NUM" UniqueName="APPROVAL_NUM" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="SDESC" SortExpression="SDESC"
                                UniqueName="SDESC" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }
        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Insurance&fid=ctrlInsRemigeManagerNewEdit&group=Business&FormType=0', "_self"); /*
            oWindow.setSize(800, 480);
            oWindow.center(); */
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgData.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                window.open('/Default.aspx?mid=Insurance&fid=ctrlInsRemigeManagerNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '', "_self"); /*
                oWindow.setSize(800, 480);
                oWindow.center(); */
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                } else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgData.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
        }
        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
            $get("<%# btnSearch.ClientId %>").click();
        }
    </script>
</tlk:RadScriptBlock>
