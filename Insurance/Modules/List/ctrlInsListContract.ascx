<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsListContract.ascx.vb"
    Inherits="Insurance.ctrlInsListContract" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <%--<tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>--%>
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
                            <%# Translate("Số HĐ bảo hiểm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtSoBH" runat="server" >
                            </tlk:RadTextBox>
                        </td>
                        <td align="left">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNam" runat="server" >
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị bảo hiểm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboDVBaoHiem" runat="server">
                            </tlk:RadComboBox>
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
                    <MasterTableView DataKeyNames="ID,CONTRACT_INS_NO" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số HĐ bảo hiểm %>" DataField="CONTRACT_INS_NO"
                                SortExpression="CONTRACT_INS_NO" UniqueName="CONTRACT_INS_NO" HeaderStyle-Width="120px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR">
                                <HeaderStyle Width="60px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="ORG_INSURANCE_NAME"
                                SortExpression="ORG_INSURANCE_NAME" UniqueName="ORG_INSURANCE_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: HĐ từ ngày%>" DataField="START_DATE"
                               DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE" UniqueName="START_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: HĐ đến ngày%>" DataField="EXPIRE_DATE"
                               DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" CurrentFilterFunction="EqualTo" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị HĐ %>" DataField="VAL_CO"
                                SortExpression="VAL_CO" UniqueName="VAL_CO">
                                <HeaderStyle Width="120px" />
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày mua %>" DataField="BUY_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="BUY_DATE" UniqueName="BUY_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE"
                                SortExpression="NOTE" UniqueName="NOTE">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chương trình bảo hiểm %>" DataField="PROGRAM_MUL_NAME"
                                SortExpression="PROGRAM_MUL_NAME" UniqueName="PROGRAM_MUL_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
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
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsListContractNewEdit&group=List&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(900, 600);
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
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsListContractNewEdit&group=List&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.setSize(900, 600);
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
