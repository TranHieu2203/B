<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_MA_SCP_QLCH.ascx.vb"
    Inherits="Payroll.ctrlPA_MA_SCP_QLCH" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <%--<tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>--%>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" AutoPostBack="true" runat="server" TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label5" Text="Tháng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lblTARGET_GROUP" Text="Nhóm Target"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTARGET_GROUP" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <%--<td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>--%>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID"
                        ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Nhóm Target %>" DataField="TARGET_GROUP_NAME"
                                SortExpression="TARGET_GROUP_NAME" UniqueName="TARGET_GROUP_NAME" HeaderStyle-Width="100px"/>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Tên cửa hàng%>" DataField="STORE_NAME"
                                SortExpression="STORE_NAME" UniqueName="STORE_NAME" HeaderStyle-Width="100px"/>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ %>" DataField="DTTD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD"
                                UniqueName="DTTD" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ_NG %>" DataField="DTTD_NG"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_NG"
                                UniqueName="DTTD_NG" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ_KNG1  %>" DataField="DTTD_KNG1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_KNG1"
                                UniqueName="DTTD_KNG1" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ_KNG2  %>" DataField="DTTD_KNG2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_KNG2"
                                UniqueName="DTTD_KNG2" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:UPT_TĐ  %>" DataField="UPT_TD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="UPT_TD"
                                UniqueName="UPT_TD" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:CON_TĐ %>" DataField="CON_TD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CON_TD"
                                UniqueName="CON_TD" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ RR12 %>" DataField="RATE_RR12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_RR12"
                                UniqueName="RATE_RR12" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ SPSG %>" DataField="RATE_SPSG"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_SPSG"
                                UniqueName="RATE_SPSG" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ CSS %>" DataField="RATE_CSS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_CSS"
                                UniqueName="RATE_CSS" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỉ lệ FSOM %>" DataField="RATE_FSOM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_FSOM"
                                UniqueName="RATE_FSOM" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ MRA %>" DataField="RATE_MRA"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_MRA"
                                UniqueName="RATE_MRA" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ CR %>" DataField="RATE_CR"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_CR"
                                UniqueName="RATE_CR" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ xin email KH %>" DataField="RATE_EMAILCUSTOMER"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_EMAILCUSTOMER"
                                UniqueName="RATE_EMAILCUSTOMER" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ MBS %>" DataField="RATE_MBS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_MBS"
                                UniqueName="RATE_MBS" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ 90D %>" DataField="RATE_90D"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_90D"
                                UniqueName="RATE_90D" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ MA %>" DataField="RATE_MA"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_MA"
                                UniqueName="RATE_MA" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tỉ lệ SCP %>" DataField="RATE_SCP"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RATE_SCP"
                                UniqueName="RATE_SCP" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:RR6_TD %>" DataField="RR6_TD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RR6_TD"
                                UniqueName="RR6_TD" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLBILL_TD %>" DataField="SLBILL_TD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLBILL_TD"
                                UniqueName="SLBILL_TD" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLTV %>" DataField="SLTV"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLTV"
                                UniqueName="SLTV" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLTV_6MONTH %>" DataField="SLTV_6MONTH"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLTV_6MONTH"
                                UniqueName="SLTV_6MONTH" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLBILL_NONMEMBER %>" DataField="SLBILL_NONMEMBER"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLBILL_NONMEMBER"
                                UniqueName="SLBILL_NONMEMBER" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLBILL_NEWMEMBER %>" DataField="SLBILL_NEWMEMBER"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLBILL_NEWMEMBER"
                                UniqueName="SLBILL_NEWMEMBER" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLKH_RETURN_YEAR %>" DataField="SLKH_RETURN_YEAR"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLKH_RETURN_YEAR"
                                UniqueName="SLKH_RETURN_YEAR" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SLKH_MEMBER_YEAR %>" DataField="SLKH_MEMBER_YEAR"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SLKH_MEMBER_YEAR"
                                UniqueName="SLKH_MEMBER_YEAR" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
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
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
