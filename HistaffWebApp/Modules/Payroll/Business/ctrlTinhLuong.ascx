<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTinhLuong.ascx.vb"
    Inherits="Payroll.ctrlTinhLuong" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane4" runat="server" Height="100%" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarTerminates" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="70px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnmYear" runat="server" SkinID="Number" ShowSpinButtons="true"
                                AutoPostBack="true">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcboPeriod" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Danh sách")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcboDsach" runat="server" Width="150px" AutoPostBack="true">
                                <Items>
                                    <tlk:RadComboBoxItem runat="server" Text="Dữ liệu chưa tải" Value="0" />
                                    <tlk:RadComboBoxItem runat="server" Text="Dữ liệu đã tải" Value="1" />
                                </Items>
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkNghiViec" runat="server" Text="Lọc theo nhân viên nghỉ việc" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneTop" runat="server" Height="25px" Scrolling="None">
                <tlk:RadTabStrip ID="rtabSalary" runat="server" AutoPostBack="false" CausesValidation="false"
                    MultiPageID="RadMultiPage1">
                    <Tabs>
                        <tlk:RadTab runat="server" PageViewID="rpTrongKy" Text="<%$ Translate: Dữ liệu lương trong kỳ %>"
                            Selected="True">
                        </tlk:RadTab>
                        <tlk:RadTab runat="server" PageViewID="rpTongHop" Text="<%$ Translate: Lương tổng hợp %>">
                        </tlk:RadTab>
                    </Tabs>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneBotton" runat="server" Scrolling="None">
                <tlk:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%"
                    Height="100%">
                    <tlk:RadPageView ID="rpTrongKy" runat="server" Width="100%" Height="100%">
                        <tlk:RadGrid ID="rgDataTK" runat="server" SkinID="GridSingleSelect" Height="100%" PageSize="50">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_NAME">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                        DataField="FULLNAME_VN" SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="200px" HeaderText="<%$ Translate: Đơn vị %>"
                                        DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPageView>
                    <tlk:RadPageView ID="rpTongHop" runat="server" Width="100%" Height="100%">
                        <tlk:RadGrid ID="rgDataTH" runat="server" SkinID="GridSingleSelect" Height="100%">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_NAME">
                                <Columns>
                                     <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                        DataField="FULLNAME_VN" SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="200px" HeaderText="<%$ Translate: Đơn vị %>"
                                        DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPageView>
                </tlk:RadMultiPage>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
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
