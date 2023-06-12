<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsManagerSunCare.ascx.vb"
    Inherits="Insurance.ctrlInsManagerSunCare" %>
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
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <asp:HiddenField ID="hidID" runat="server" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày cấp từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Ngày cấp tới")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
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
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự%>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="THOIDIEMHUONG"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="THOIDIEMHUONG" UniqueName="THOIDIEMHUONG">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE" UniqueName="START_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="END_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE" UniqueName="END_DATE">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị cung cấp BH %>" DataField="LEVEL_NAME"
                                SortExpression="LEVEL_NAME" UniqueName="LEVEL_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí mua %>" DataField="COST"
                                SortExpression="COST" DataFormatString="{0:n0}" UniqueName="COST" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức bồi thường %>" DataField="COST_SAL"
                                SortExpression="COST_SAL" DataFormatString="{0:n0}" UniqueName="COST_SAL" />
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" SortExpression="NOTE"
                                UniqueName="NOTE" />--%>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
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
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerHeathInsNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
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
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerHeathInsNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
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
