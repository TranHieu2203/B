<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_LdapConfiguration.ascx.vb"
    Inherits="Common.ctrlSE_LdapConfiguration" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đường dẫn LDAP")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLDAP" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtLDAP" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Đường dẫn LDAP %>" ToolTip="<%$ Translate: Bạn phải nhập Đường dẫn LDAP %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Domain")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDomainName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtDomainName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Domain %>" ToolTip="<%$ Translate: Bạn phải nhập Domain %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Base DN")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtBaseDN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtBaseDN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Base DN %>" ToolTip="<%$ Translate: Bạn phải nhập Base DN %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,LDAP_NAME,DOMAIN_NAME,BASE_DN" ClientDataKeyNames="LDAP_NAME,DOMAIN_NAME,BASE_DN">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đường dẫn LDAP %>" DataField="LDAP_NAME"
                        UniqueName="LDAP_NAME" SortExpression="LDAP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Domain %>" DataField="DOMAIN_NAME"
                        UniqueName="DOMAIN_NAME" SortExpression="DOMAIN_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Base DN %>" DataField="BASE_DN" UniqueName="BASE_DN"
                        SortExpression="BASE_DN" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
