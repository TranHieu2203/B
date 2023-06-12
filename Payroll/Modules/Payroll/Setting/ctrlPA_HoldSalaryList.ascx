<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_HoldSalaryList.ascx.vb"
    Inherits="Payroll.ctrlPA_HoldSalaryList" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnmYear" runat="server" Width="50px" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcboPeriod" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ lương%>" DataField="PERIOD_NAME"
                                SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng thưởng %>" DataField="TOTAL_SALARY"
                                SortExpression="TOTAL_SALARY" UniqueName="TOTAL_SALARY" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<script type="text/javascript">

    function popupclose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);

        }

    }

    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>
