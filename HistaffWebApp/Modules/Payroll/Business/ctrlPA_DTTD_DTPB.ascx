<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_DTTD_DTPB.ascx.vb"
    Inherits="Payroll.ctrlPA_DTTD_DTPB" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
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
                            <tlk:RadComboBox ID="cbYear" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ lương")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbPeriodName" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
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
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID"
                        ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã cửa hàng" DataField="STORE_CODE"
                                SortExpression="STORE_CODE" UniqueName="STORE_CODE" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR"
                                ItemStyle-HorizontalAlign="Center" SortExpression="YEAR"
                                UniqueName="YEAR">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="Tháng" DataField="MONTH"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MONTH"
                                UniqueName="MONTH">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTD %>" DataField="DTTD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD"
                                UniqueName="DTTD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTD_NG %>" DataField="DTTD_NG"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_NG"
                                UniqueName="DTTD_NG">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTD_KNG1  %>" DataField="DTTD_KNG1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_KNG1"
                                UniqueName="DTTD_KNG1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTD_KNG2  %>" DataField="DTTD_KNG2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_KNG2"
                                UniqueName="DTTD_KNG2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="UPT thực đạt" DataField="UPT_TD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="UPT_TD"
                                UniqueName="UPT_TD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="CON thực đạt" DataField="CON_TD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CON_TD"
                                UniqueName="CON_TD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Khiếu nại" DataField="COMPLAIN"
                                ItemStyle-HorizontalAlign="Center" SortExpression="COMPLAIN"
                                UniqueName="COMPLAIN">
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
