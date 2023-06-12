<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlManGroupSunCare.ascx.vb"
    Inherits="Insurance.ctrlManGroupSunCare" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="40px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Ngày hết hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
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
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgManagerInfo" runat="server" Height="100%" AllowSorting="True"
                    AllowMultiRowSelection="true" AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_ID"
                                Visible="false" SortExpression="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh cũ %>" DataField="TITLE_NAME_OLD"
                                SortExpression="TITLE_NAME_OLD" UniqueName="TITLE_NAME_OLD">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh mới %>" DataField="TITLE_NAME_NEW"
                                SortExpression="TITLE_NAME_NEW" UniqueName="TITLE_NAME_NEW">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự cũ %>" DataField="STAFF_RANK_NAME_OLD"
                                UniqueName="STAFF_RANK_NAME_OLD" HeaderStyle-Width="100px" SortExpression="STAFF_RANK_NAME_OLD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự mới %>" DataField="STAFF_RANK_NAME_NEW"
                                UniqueName="STAFF_RANK_NAME_NEW" HeaderStyle-Width="100px" SortExpression="STAFF_RANK_NAME_NEW" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm hưởng bảo hiểm cũ %>" DataField="COST_LEVER_NAME_OLD"
                                UniqueName="COST_LEVER_NAME_OLD" HeaderStyle-Width="100px" SortExpression="COST_LEVER_NAME_OLD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm hưởng bảo hiểm mới %>" DataField="COST_LEVER_NAME_NEW"
                                UniqueName="COST_LEVER_NAME_NEW" HeaderStyle-Width="100px" SortExpression="COST_LEVER_NAME_NEW" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức hưởng chi phí %>" DataField="COST_MONEY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" SortExpression="COST_MONEY"
                                UniqueName="COST_MONEY" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực của nhóm hưởng BH mới %>"
                                DataField="EFFECTDATE_COST_NEW" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                                SortExpression="EFFECTDATE_COST_NEW" UniqueName="NGAYSINH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi Chú %>" DataField="NOTE" SortExpression="NOTE"
                                UniqueName="NOTE">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        var splitterID = 'ctl00_MainContent_ctrlManGroupSunCare_RadSplitter3';

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
            OpenEditWindow();
        }

        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Insurance&fid=ctrlManGroupSunCareNewEdit&group=Business&FormType=0&noscroll=1', "_self"); /*
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlManGroupSunCareNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(850, 530);
            oWindow.center(); */
        }

        function OpenEditWindow() {
            var grid = $find('<%# rgManagerInfo.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                window.open('/Default.aspx?mid=Insurance&fid=ctrlManGroupSunCareNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "_self"); /*
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlManGroupSunCareNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup"); 
                oWindow.setSize(850, 530);
                oWindow.center(); */
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgManagerInfo.ClientID %>").get_masterTableView().rebind();
            }
            $get("<%# btnSearch.ClientId %>").click();
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%= rgManagerInfo.ClientID %>");
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
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'IMPORT') {
                OpenImportWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgManagerInfo.ClientID %>').get_masterTableView().get_selectedItems().length;
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
            }
        }
    </script>
</tlk:RadScriptBlock>
