<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlImportQuyThuongHQCV.ascx.vb"
    Inherits="Payroll.ctrlImportQuyThuongHQCV" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane4" runat="server" Width="250px" Scrolling="None" Visible="false">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPaneTop" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server" SkinID="dDropdownList" AutoPostBack="true"
                                Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ lương")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("THƯỞNG HQCV")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSalaryType" runat="server" SkinID="dDropdownList" Width="350px">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSeach" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneBotton" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
                    <tlk:RadPane ID="RadPane1" runat="server" Width="250px" Scrolling="None">
                        <tlk:RadTreeView runat="server" CausesValidation="false" ID="ctrlListSalary" CheckBoxes="true"
                            CheckChildNodes="true" Height="100%">
                        </tlk:RadTreeView>
                    </tlk:RadPane>
                    <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
                    </tlk:RadSplitBar>
                    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="PAYMENTSOURCES_ID" ClientDataKeyNames="PAYMENTSOURCES_ID,NAME,MONEY">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="X" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                                        ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã đơn vị quỹ thưởng %>"
                                        DataField="PAYMENTSOURCES_ID" SortExpression="PAYMENTSOURCES_ID" UniqueName="PAYMENTSOURCES_ID" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="250px" HeaderText="<%$ Translate: Tên đơn vị quỹ thưởng %>"
                                        DataField="NAME" SortExpression="NAME" UniqueName="NAME" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Số tiền %>"
                                        DataField="MONEY" SortExpression="MONEY" UniqueName="MONEY" DataFormatString="{0:#,##0.##}" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<script type="text/javascript">

    function clientButtonClicking(sender, args) {
        if (args.get_item().get_commandName() == 'EXPORT') {
            enableAjax = false;
        }
    }
    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>
