<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSalaryFuncMapping.ascx.vb"
    Inherits="Payroll.ctrlSalaryFuncMapping" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .RadGrid_Metro .rgRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgAltRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgHeader
    {
        padding :0 !important;
    }
</style>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="88px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
                <table class="table-form" style="padding-top:5px">
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
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("SALTYPE")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSalaryType" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSeach" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
                    <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
                    </tlk:RadSplitBar>
                    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,COL_NAME,NAME_VN">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="8px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderStyle-Width="50px" HeaderText="<%$ Translate: Mã mục lương %>"
                                        DataField="COL_NAME" SortExpression="COL_NAME" UniqueName="COL_NAME" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="80px" HeaderText="<%$ Translate: Tên mục lương %>"
                                        DataField="NAME_VN" SortExpression="NAME_VN" UniqueName="NAME_VN" />
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
