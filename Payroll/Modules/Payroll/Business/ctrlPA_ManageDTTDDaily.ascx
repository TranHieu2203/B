<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_ManageDTTDDaily.ascx.vb"
    Inherits="Payroll.ctrlPA_ManageDTTDDaily" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" >
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server" >
                            </tlk:RadDatePicker>
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

                            <tlk:GridDateTimeColumn HeaderText="Ngày bán" DataField="SALE_DATE"
                                UniqueName="SALE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SALE_DATE">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ %>" DataField="DTTD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD"
                                UniqueName="DTTD">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ_NG %>" DataField="DTTD_NG"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_NG"
                                UniqueName="DTTD_NG">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ_KNG1  %>" DataField="DTTD_KNG1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_KNG1"
                                UniqueName="DTTD_KNG1">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:DTTĐ_KNG2  %>" DataField="DTTD_KNG2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DTTD_KNG2"
                                UniqueName="DTTD_KNG2">
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
            }else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
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
