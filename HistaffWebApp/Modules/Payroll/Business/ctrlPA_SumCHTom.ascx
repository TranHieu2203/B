<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SumCHTom.ascx.vb"
    Inherits="Payroll.ctrlPA_SumCHTom" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                     <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
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
                <tlk:RadGrid PageSize="50" ID="rgWorkschedule" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Tên đơn vị/phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                    </asp:Label>
                                    <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                        RelativeTo="Element" Position="BottomCenter">
                                        <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                    </tlk:RadToolTip>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Sales Target %>" DataField="Target_DT"
                                UniqueName="Target_DT" SortExpression="Target_DT">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Doanh thu thực đạt %>" DataField="DTTD_CH"
                                UniqueName="DTTD_CH" SortExpression="DTTD_CH">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉ lệ doanh thu %>" DataField="TLDT_CH"
                                UniqueName="TLDT_CH" SortExpression="TLDT_CH">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉ lệ 90D %>" DataField="RATE_90D"
                                UniqueName="RATE_90D" SortExpression="RATE_90D">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                             <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên %>" DataField="OBJECT_EMPLOYEE_NAME"
                                UniqueName="OBJECT_EMPLOYEE_NAME" SortExpression="OBJECT_EMPLOYEE_NAME">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉ lệ SPSG %>" DataField="RATE_SPSG"
                                UniqueName="RATE_SPSG" SortExpression="RATE_SPSG">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bill Non-Member %>" DataField="SLBILL_NONMEMBER"
                                UniqueName="SLBILL_NONMEMBER" SortExpression="SLBILL_NONMEMBER">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bill New-Member %>" DataField="SLBILL_NEWMEMBER"
                                UniqueName="SLBILL_NEWMEMBER" SortExpression="SLBILL_NEWMEMBER">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Bill target %>" DataField="SLBill_Target"
                                UniqueName="SLBill_Target" SortExpression="SLBill_Target">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                              <tlk:GridBoundColumn HeaderText="<%$ Translate: Bill thực đạt %>" DataField="SLBILL_TD"
                                UniqueName="SLBILL_TD" SortExpression="SLBILL_TD">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: RR6_TARGET %>" DataField="RR6_TARGET"
                                UniqueName="RR6_TARGET" SortExpression="RR6_TARGET">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: SLTV %>" DataField="SLTV"
                                UniqueName="SLTV" SortExpression="SLTV">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: SLTV_6MONTH %>" DataField="SLTV_6MONTH"
                                UniqueName="SLTV_6MONTH" SortExpression="SLTV_6MONTH">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: % achieve (RRA6) %>" DataField="RR6_SLTV"
                                UniqueName="RR6_SLTV" SortExpression="RR6_SLTV">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                              <tlk:GridBoundColumn HeaderText="<%$ Translate: % achieve (RRA6) /RR6_Target %>" DataField="TiLe_SLTV"
                                UniqueName="TiLe_SLTV" SortExpression="TiLe_SLTV">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Chỉ tiêu khác hàng mới %>" DataField="MBS_Target"
                                UniqueName="MBS_Target" SortExpression="MBS_Target">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Khách hàng mới TD %>" DataField="MBS_TD"
                                UniqueName="MBS_TD" SortExpression="MBS_TD">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: SLKH_RETURN_YEAR %>" DataField="SLKH_RETURN_YEAR"
                                UniqueName="SLKH_RETURN_YEAR" SortExpression="SLKH_RETURN_YEAR">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: SLKH_MEMBER_YEAR %>" DataField="SLKH_MEMBER_YEAR"
                                UniqueName="SLKH_MEMBER_YEAR" SortExpression="SLKH_MEMBER_YEAR">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

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

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();

            //if (args.get_item().get_commandName() == 'EXPORT_TEMP') {
            //    enableAjax = false;
            //}

            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    $('#' + txtBoxName + ',' + '#' + txtBoxTitle).css("width", "100%");
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgWorkschedule.ClientID %>");
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
        }

        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
