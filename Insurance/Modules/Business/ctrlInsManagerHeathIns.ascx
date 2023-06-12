<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsManagerHeathIns.ascx.vb"
    Inherits="Insurance.ctrlInsManagerHeathIns" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
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
            <tlk:RadPane ID="RadPane1" runat="server" Height="140px" Scrolling="None">
                <asp:HiddenField ID="hidID" runat="server" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày tham gia")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdJoinFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdJoinTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày báo giảm")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdReduceFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdReduceTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chkClaim" runat="server" Text="<%$ Translate: Đã claim %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="True">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ORG_DESC,PHONG_BAN">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                                SortExpression="FULLNAME" UniqueName="FULLNAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO"
                                SortExpression="ID_NO" UniqueName="ID_NO">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ban/Phòng %>" DataField="PHONG_BAN" UniqueName="PHONG_BAN" SortExpression="PHONG_BAN"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="170px"/>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="FULLNAME"
                                SortExpression="FULLNAME" UniqueName="FULLNAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>--%>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR" >
                                <HeaderStyle Width="110px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số HĐ bảo hiểm %>" DataField="CONTRACT_INS_NO"
                                SortExpression="CONTRACT_INS_NO" UniqueName="CONTRACT_INS_NO">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="ORG_INSURANCE"
                                SortExpression="ORG_INSURANCE" UniqueName="ORG_INSURANCE">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị HĐ %>" DataField="VAL_CO"
                                SortExpression="VAL_CO" UniqueName="VAL_CO">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: HĐ từ ngày %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: HĐ đến ngày %>" DataField="EXPIRE_DATE"
                                SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chương trình bảo hiểm %>" DataField="NAME_PROGRAM"
                                SortExpression="NAME" UniqueName="NAME_PROGRAM">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="SOTIEN"
                                SortExpression="SOTIEN" UniqueName="SOTIEN" DataFormatString="{0:n0}">
                                <HeaderStyle Width="150px" />
                            </tlk:GridNumericColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên người thân %>" DataField="HOTENNGUOITHAN"
                                SortExpression="HOTENNGUOITHAN" UniqueName="HOTENNGUOITHAN">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mối quan hệ %>" DataField="MOIQUANHE"
                                SortExpression="MOIQUANHE" UniqueName="MOIQUANHE">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="NGAYSINHTN"
                                SortExpression="NGAYSINHTN" UniqueName="NGAYSINHTN" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="CMNDTN"
                                SortExpression="CMNDTN" UniqueName="CMNDTN">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng chi trả %>" DataField="DT_CHITRA_NAME"
                                SortExpression="DT_CHITRA_NAME" UniqueName="DT_CHITRA_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày tham gia %>" DataField="JOIN_DATE"
                                SortExpression="JOIN_DATE" UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền bảo hiểm %>" DataField="MONEY_INS"
                                SortExpression="MONEY_INS" UniqueName="MONEY_INS" DataFormatString="{0:n0}">
                                <HeaderStyle Width="150px" />
                            </tlk:GridNumericColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày báo giảm %>" DataField="REDUCE_DATE"
                                SortExpression="REDUCE_DATE" UniqueName="REDUCE_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền hoàn lại %>" DataField="REFUND"
                                SortExpression="REFUND" UniqueName="REFUND" DataFormatString="{0:n0}">
                                <HeaderStyle Width="150px" />
                            </tlk:GridNumericColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày nhận tiền %>" DataField="DATE_RECEIVE_MONEY"
                                SortExpression="DATE_RECEIVE_MONEY" UniqueName="DATE_RECEIVE_MONEY" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người nhận tiền %>" DataField="EMP_RECEIVE_MONEY"
                                SortExpression="EMP_RECEIVE_MONEY" UniqueName="EMP_RECEIVE_MONEY">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú%>" DataField="NOTES"
                                SortExpression="NOTES" UniqueName="NOTES">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                                

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
        $(document).ready(function () {
            $(window).on('load', searchDara());
        });
        function searchDara() {
            $get("<%# btnSearch.ClientId %>").click();

        }       
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerHeathInsNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.maximize(true);
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
                oWindow.maximize(true);
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
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CHECK') {
                enableAjax = false;
            }
        }
        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadScriptBlock>
