<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlImport_ManagerInfo.ascx.vb"
    Inherits="Insurance.ctrlImport_ManagerInfo" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="BottomPanel" runat="server" MinWidth="200" Width="250px" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
                <tlk:RadGrid PageSize=50 ID="rgImport_ManagerInfo" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="STT" ClientDataKeyNames="STT">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID nhân viên %>" DataField="EMPLOYEE_ID"
                                UniqueName="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME" UniqueName="FULLNAME"
                                SortExpression="FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:  Số sổ %>" DataField="SOBOOKNO" UniqueName="SOBOOKNO"
                                SortExpression="SOBOOKNO" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: ngày cấp %>" DataField="SOPRVDBOOKDAY"
                                UniqueName="SOPRVDBOOKDAY" SortExpression="SOPRVDBOOKDAY" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nộp sổ %>" DataField="DAYPAYMENTCOMPANY"
                                UniqueName="DAYPAYMENTCOMPANY" SortExpression="DAYPAYMENTCOMPANY" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:  Số thẻ %>" DataField="HECARDNO"
                                UniqueName="HECARDNO" SortExpression="HECARDNO" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: ngày hiệu lực thẻ %>" DataField="HECARDEFFFROM"
                                UniqueName="HECARDEFFFROM" SortExpression="HECARDEFFFROM" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực thẻ %>" DataField="HECARDEFFTO"
                                UniqueName="HECARDEFFTO" SortExpression="HECARDEFFTO" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đăng ký khám chữa bệnh %>" DataField="HEWHRREGISKEY_NAME"
                                UniqueName="HEWHRREGISKEY_NAME" SortExpression="HEWHRREGISKEY_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nơi đăng ký khám chữa bệnh %>"
                                DataField="HEWHRREGISKEY" UniqueName="HEWHRREGISKEY" SortExpression="HEWHRREGISKEY"
                                Visible="false" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadScriptBlock>
