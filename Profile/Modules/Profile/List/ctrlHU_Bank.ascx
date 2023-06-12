<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Bank.ascx.vb"
    Inherits="Profile.ctrlHU_Bank" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="130px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server">
        </tlk:RadToolBar>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ngân hàng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" Width="160px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã ngân hàng %>" ToolTip="<%$ Translate:  Bạn phải nhập mã ngân hàng %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã ngân hàng đã tồn tại %>"
                        ToolTip="<%$ Translate: Mã ngân hàng đã tồn tại %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên ngân hàng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server" Width="160px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên ngân hàng %>" ToolTip="<%$ Translate:  Bạn phải nhập tên ngân hàng %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên viết tắt")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtShortName" MaxLength="255" runat="server" Width="160px">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="reqShortName" ControlToValidate="txtShortName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên viết tắt %>" ToolTip="<%$ Translate: Bạn phải nhập tên viết tắt %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgBank" runat="server" Height="100%" OnPreRender="rgBanks_PreRender" >
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,SHORT_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>" UniqueName="ID"
                        Visible="false" />
                    <tlk:GridBoundColumn DataField="CODE" HeaderText="<%$ Translate: Mã ngân hàng %>"
                        UniqueName="CODE" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME" HeaderText="<%$ Translate: Tên ngân hàng %>"
                        UniqueName="NAME" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SHORT_NAME" HeaderText="<%$ Translate: Tên viết tắt %>"
                        UniqueName="SHORT_NAME" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" DataType="System.String" HeaderText="<%$ Translate: Trạng thái %>">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
             </ClientSettings>
            <HeaderStyle HorizontalAlign="Center" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var val = eventArgs.get_commandArgument().split("|")[1];
            if (validateHTMLText(val) || validateSQLText(val)) {
                eventArgs.set_cancel(true);
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_Bank_RadSplitter3');
        }

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
                var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }
        // Resize when save success
        function ResizeSplitterSaveSuccess() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - 130);
                pane.set_height(130);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane1.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - 130);
                pane.set_height(130);
                pane2.set_height(splitter.get_height() - 130 - 1);
            }
        }
      
    </script>
</tlk:RadCodeBlock>
