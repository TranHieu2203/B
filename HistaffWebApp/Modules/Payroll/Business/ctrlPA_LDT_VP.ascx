<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_LDT_VP.ascx.vb"
    Inherits="Payroll.ctrlPA_LDT_VP" %>
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
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:MR %>" DataField="MR"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="MR"
                                UniqueName="MR">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:AVBA %>" DataField="AVBA"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="AVBA"
                                UniqueName="AVBA">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:RR6_A  %>" DataField="RR6_A"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="RR6_A"
                                UniqueName="RR6_A">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:BS  %>" DataField="BS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BS"
                                UniqueName="BS">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:SPSG  %>" DataField="SPSG"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SPSG"
                                UniqueName="SPSG">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:90D %>" DataField="D90"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="D90"
                                UniqueName="D90">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:TA  %>" DataField="TA"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="TA"
                                UniqueName="TA">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:BILL  %>" DataField="BILL"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BILL"
                                UniqueName="BILL">
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
