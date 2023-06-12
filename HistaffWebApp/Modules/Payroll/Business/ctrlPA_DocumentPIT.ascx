<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_DocumentPIT.ascx.vb"
    Inherits="Payroll.ctrlPA_DocumentPIT" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" CheckBoxes="All" CheckChildNodes="true" AutoPostBack="true"/>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm chứng từ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label5" Text="Thời gian xuất liên 1 từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdL1FromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Thời gian xuất liên 1 đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdL1ToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeCode"  runat="server" >
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Thời gian xuất liên 2 từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdL2FromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label3" Text="Thời gian xuất liên 2 đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdL2ToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số(No)")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNo"  runat="server" >
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,LIEN1,LIEN2"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,LIEN1,LIEN2">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Mã số thuế" DataField="PIT_CODE"
                                SortExpression="PIT_CODE" UniqueName="PIT_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Số chứng từ" DataField="PIT_NO"
                                SortExpression="PIT_NO" UniqueName="PIT_NO" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Năm chứng từ" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Liên 1" DataField="LIEN1_STATUS"
                                SortExpression="LIEN1_STATUS" UniqueName="LIEN1_STATUS" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày ghi nhận Liên 1" DataField="LIEN1_DATE"
                                SortExpression="LIEN1_DATE" UniqueName="LIEN1_DATE" HeaderStyle-Width="130px"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Liên 2" DataField="LIEN2_STATUS"
                                SortExpression="LIEN2_STATUS" UniqueName="LIEN2_STATUS" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày ghi nhận Liên 2" DataField="LIEN2_DATE"
                                SortExpression="LIEN2_DATE" UniqueName="LIEN2_DATE" HeaderStyle-Width="130px"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Khoản thu nhập" DataField="TYPE_INCOME"
                                SortExpression="TYPE_INCOME" UniqueName="TYPE_INCOME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Thời điểm trả thu nhập" DataField="PERIOD_REPLY"
                                SortExpression="PERIOD_REPLY" UniqueName="PERIOD_REPLY" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng thu nhập chịu thuế đã trả %>" DataField="TAXABLE_INCOME"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="TAXABLE_INCOME"
                                UniqueName="TAXABLE_INCOME">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số thuế thu nhập cá nhân đã khấu trừ %>" DataField="MONEY_PIT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="MONEY_PIT"
                                UniqueName="MONEY_PIT">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số thu nhập cá nhân còn được nhận %>" DataField="REST_INCOME"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="REST_INCOME"
                                UniqueName="REST_INCOME">
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày sinh" DataField="BIRTH_DATE"
                                SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" HeaderStyle-Width="130px"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Giới tính" DataField="GENDER"
                                SortExpression="GENDER" UniqueName="GENDER" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" ReadOnly="true" />--%>

                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientClose="PopupClose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Tạo Phiếu PIT %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgData.ClientID%>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "PRINT_LIEN1" || args.get_item().get_commandName() == "PRINT_LIEN2") {
                var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
                if (items.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                } else if (items.length > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "CREATE") {
                var oWindow = radopen('Dialog.aspx?mid=Payroll&fid=ctrlPA_DocumentPITNewEdit&group=Business', "rwPopup");
                oWindow.setSize(900, 500);
                oWindow.center();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == "EDIT") {
                var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
                if (items.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                } else if (items.length > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                var id = empID = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_DocumentPITDetail&group=Business&strDT=' + btoa(unescape(encodeURIComponent(id))), "_self");
            } else if (args.get_item().get_commandName() == "PRINT_LIEN1" || args.get_item().get_commandName() == "PRINT_LIEN1") {
                var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
                if (items.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                } else if (items.length > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else if (args.get_item().get_commandName() == "CANCEL_PIT") {
                var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
                if (items.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OpenCreate() {
            var oWindow = radopen('Dialog.aspx?mid=Payroll&fid=ctrlPA_DocumentPITNewEdit&group=Business', "rwPopup");
            oWindow.setSize(600, 600);
            oWindow.center();
        }
        function PopupClose() {

        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
