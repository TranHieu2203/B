<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsManagerChange.ascx.vb"
    Inherits="Insurance.ctrlInsManagerChange" %>
<%@ Import Namespace="Common" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
                <asp:HiddenField ID="hidID" runat="server" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày thay đổi từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>

                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbJoinDate" Text="Tháng báo BH từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdMonthFrom" runat="server" DateInput-DisplayDateFormat="MM/yyyy" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdMonthTo" runat="server" DateInput-DisplayDateFormat="MM/yyyy" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
                    AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban%>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại thay đổi%>" DataField="TYPE_CHANGE_NAME"
                                SortExpression="TYPE_CHANGE_NAME" UniqueName="TYPE_CHANGE_NAME">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung thay đổi%>" DataField="CONTENT_CHANGE"
                                SortExpression="CONTENT_CHANGE" UniqueName="CONTENT_CHANGE">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thông tin cũ%>" DataField="OLD_INFO"
                                SortExpression="OLD_INFO" UniqueName="OLD_INFO">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thông tin mới%>" DataField="NEW_INFO"
                                SortExpression="NEW_INFO" UniqueName="NEW_INFO">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thay đổi %>" DataField="DATE_CHANGE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="DATE_CHANGE" UniqueName="DATE_CHANGE">
                                <HeaderStyle Width="90px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tháng báo BH %>" DataField="MONTH_DECLARE1"
                                SortExpression="MONTH_DECLARE1" UniqueName="MONTH_DECLARE1">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tháng báo BH %>" DataField="MONTH_DECLARE1"
                                DataFormatString="{0:MM/yyyy}" SortExpression="MONTH_DECLARE1" UniqueName="MONTH_DECLARE1">
                                <HeaderStyle Width="90px" />
                            </tlk:GridDateTimeColumn>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do%>" DataField="REASON_CHANGE"
                                SortExpression="REASON_CHANGE" UniqueName="REASON_CHANGE">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                           
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerChangeNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(800, 500);
            oWindow.center();
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
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerChangeNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.setSize(800, 500);
                oWindow.center();
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
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
            $get("<%# btnSearch.ClientId %>").click();
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
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
    }
    function OpenInNewTab(url) {
        var win = window.open(url, '_blank');
        win.focus();
    }
    </script>
</tlk:RadScriptBlock>
