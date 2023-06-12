<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_LdapConfiguration.ascx.vb"
    Inherits="Common.ctrlSE_LdapConfiguration" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="102px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đường dẫn LDAP")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLDAP" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td>
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
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtDomainName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Domain %>" ToolTip="<%$ Translate: Bạn phải nhập Domain %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Base DN")%><span class="lbReq">*</span>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtBaseDN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td>
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
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="LDAP_NAME,DOMAIN_NAME,BASE_DN">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
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

        var splitterID = 'ctl00_MainContent_ctrlSE_LdapConfiguration_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSE_LdapConfiguration_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSE_LdapConfiguration_RadPane2';
        var validateID = 'MainContent_ctrlSE_LdapConfiguration_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid');
                } else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
