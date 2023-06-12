<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsChange.ascx.vb"
    Inherits="Insurance.ctrlInsChange" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <common:ctrlorganization id="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="40px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Biến động từ tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdFrom" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                                DateInput-DateFormat="dd/MMM/yyyy">
                                <DateInput Enabled="false">
                                </DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td align="left">
                            <%# Translate("Biến động tới tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdTo" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                                DateInput-DateFormat="dd/MMM/yyyy">
                                <DateInput Enabled="false">
                                </DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgInsChange" runat="server" Height="100%" AllowSorting="True"
                    AllowMultiRowSelection="true" AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="ISBHXH" ItemStyle-HorizontalAlign="Center"
                                UniqueName="ISBHXH" AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="ISBHYT" ItemStyle-HorizontalAlign="Center"
                                UniqueName="ISBHYT" AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="ISBHTN" ItemStyle-HorizontalAlign="Center"
                                UniqueName="ISBHTN" AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại biến động %>" DataField="CHANGE_TYPE_NAME"
                                ItemStyle-HorizontalAlign="Left" SortExpression="CHANGE_TYPE_NAME" UniqueName="CHANGE_TYPE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>                                         
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tháng biến động %>" DataField="CHANGE_MONTH"
                                SortExpression="CHANGE_MONTH" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/yyyy}"
                                UniqueName="CHANGE_MONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số kỳ trước %>" DataField="OLDSALARY"
                                SortExpression="OLDSALARY" UniqueName="OLDSALARY" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số kỳ này %>" DataField="NEWSALARY"
                                ItemStyle-HorizontalAlign="Center" SortExpression="NEWSALARY" UniqueName="NEWSALARY"
                                DataFormatString="{0:N0}" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày trả sổ BHXH %>" DataField="RETURN_DATEBHXH"
                                ItemStyle-HorizontalAlign="Center" SortExpression="RETURN_DATEBHXH" UniqueName="RETURN_DATEBHXH"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày trả thẻ BHYT %>" DataField="RETURN_DATEBHYT"
                                SortExpression="RETURN_DATEBHYT" UniqueName="RETURN_DATEBHYT" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" ItemStyle-HorizontalAlign="Left"
                                SortExpression="NOTE" UniqueName="NOTE">
                                <HeaderStyle Width="250px" />
                                <ItemStyle Width="250px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Truy thu từ ngày %>" DataField="CLTFRMMONTH"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="CLTFRMMONTH"
                                UniqueName="CLTFRMMONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Truy thu đến ngày %>" DataField="CLTTOMONTH"
                                SortExpression="CLTTOMONTH" UniqueName="CLTTOMONTH" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Truy thu BHXH %>" DataField="CLTBHXH"
                                SortExpression="CLTBHXH" UniqueName="CLTBHXH" ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Truy thu BHYT %>" DataField="CLTBHYT"
                                SortExpression="CLTBHYT" UniqueName="CLTBHYT" ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Truy thu BHTN %>" DataField="CLTBHTN"
                                SortExpression="CLTBHTN" UniqueName="CLTBHTN" ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thoái thu từ ngày %>" DataField="REPFRMMONTH"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="REPFRMMONTH"
                                UniqueName="REPFRMMONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thoái thu tới ngày %>" DataField="REPTOMONTH"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="REPTOMONTH"
                                UniqueName="REPTOMONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thoái thu BHXH %>" DataField="REPBHXH"
                                SortExpression="REPBHXH" UniqueName="REPBHXH" ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thoái thu BHYT %>" DataField="REPBHYT"
                                SortExpression="REPBHYT" UniqueName="REPBHYT" ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thoái thu BHTN %>" DataField="REPBHTN"
                                SortExpression="REPBHTN" UniqueName="REPBHTN" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
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
        var splitterID = 'ctl00_MainContent_ctrlInsChange_RadSplitter3';

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Insurance&fid=ctrlInsChangeNewEdit&group=Business&FormType=0', "_self"); /*
            oWindow.setSize(800, 480);
            oWindow.center(); */
        }

        function OpenEditWindow() {
            var grid = $find('<%# rgInsChange.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                window.open('/Default.aspx?mid=Insurance&fid=ctrlInsChangeNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '', "_self"); /*
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
                var bCheck = $find('<%= rgInsChange.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                } else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%= rgInsChange.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
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
                $find("<%= rgInsChange.ClientID %>").get_masterTableView().rebind();
            }
            $get("<%# btnSearch.ClientId %>").click();
        }
    </script>
</tlk:RadScriptBlock>
